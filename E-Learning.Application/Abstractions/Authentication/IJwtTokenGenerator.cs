namespace E_Learning.Application.Abstractions.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(Guid userId, string email, string FullName, string rolename, string jit);
    }
}
