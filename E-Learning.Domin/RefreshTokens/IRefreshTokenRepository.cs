using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Domain.RefreshTokens
{
    public interface IRefreshTokenRepository
    {
        Task AddSaveToken(RefreshToken token);

        Task<RefreshToken?> GetToken(string token);

        Task DeleteToken(string token);
    }
}
