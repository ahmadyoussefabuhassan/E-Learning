

namespace E_Learning.Domain.RefreshTokens
{
    public interface IRefreshTokenRepository
    {
        Task AddSaveToken(RefreshToken token);

        Task<RefreshToken?> GetToken(string token);

        Task DeleteToken(string token);
    }
}
