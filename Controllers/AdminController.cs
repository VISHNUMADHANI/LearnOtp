using AutoMapper;
using Homecare_Dotnet.Models.DTOs;
using Homecare_Dotnet.Models.Entities;
// using Homecare_Dotnet.Repositories.Interfaces;
using Homecare_Dotnet.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



[ApiController]
[Route("api/admin")]
[Authorize]
public class AdminController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly IAdminRepository _repository;
    private readonly IPasswordService _passwordService;

    public AdminController(
        IAdminRepository repository,
        IPasswordService passwordService,
        ITokenService tokenService)
    {
        _repository = repository;
        _passwordService = passwordService;
        _tokenService = tokenService;
    }


    [HttpPost("add")]

    public async Task<IActionResult> AddAdmin(CreateAdminDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existing = await _repository.GetByEmailAsync(dto.Email.ToLowerInvariant());

        if (existing != null)
            return BadRequest("Email already exists.");

        _passwordService.CreatePasswordHash(
            dto.Password,
            out string hash,
            out string salt);

        var admin = new Admin
        {
            Name = dto.Name,
            Email = dto.Email.ToLowerInvariant(),
            PasswordHash = hash,
            PasswordSalt = salt,
            IsActive = true,
            IsDeleted = false,
        };

        await _repository.AddAsync(admin);
        await _repository.SaveChangesAsync();

        return Ok("Admin added successfully.");
    }
    [HttpGet("getall")]
    public async Task<IActionResult> GetAll()
    {
        var admins = await _repository.GetAllAsync();
        return Ok(admins);
    }



    [HttpGet("getbyemail")]
    public async Task<IActionResult> GetByEmail(string email)
    {
        var admin = await _repository.GetByEmailAsync(email);

        if (admin == null)
            return NotFound("Admin not found.");

        return Ok(admin);
    }


    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginAdminDto dto)
    {
        var admin = await _repository.GetByEmailAsync(dto.Email.ToLowerInvariant());

        if (admin == null || !admin.IsActive)
            return BadRequest("Invalid credentials.");

        bool isValid = _passwordService.VerifyPassword(
            dto.Password,
            admin.PasswordHash!,
            admin.PasswordSalt!
        );

        if (!isValid)
            return BadRequest("Invalid Password.");

        var token = _tokenService.CreateToken(admin.Email, "Admin");

        return Ok(new { token });



    }



}
