using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Application.Users.GetProfileUser
{
    public sealed record UserResponse (
        string FullName,
        string Email,
        string PhoneNumber,
        string Address,
        string ImageUrl,
        string RoleName
    );
}
