using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Application.Abstractions.Authentication
{
    public interface IJwtService
    {
        string GenerateToken(Guid userId, string email, string rolename);
    }
}
