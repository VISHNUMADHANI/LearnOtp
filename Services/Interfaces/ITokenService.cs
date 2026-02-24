public interface ITokenService
{
    string CreateToken(string email, string role);
}
