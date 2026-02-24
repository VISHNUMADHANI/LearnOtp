using AutoMapper;
using Homecare_Dotnet.Models.DTOs;
using Homecare_Dotnet.Models.Entities;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Admin, AllAdminDTO>();
    }
}
