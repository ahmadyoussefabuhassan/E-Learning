using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Domain.User
{
    public sealed record PasswordResetCode(string Value)
    {
        public static PasswordResetCode Generate() =>
                 new(new Random().Next(1000, 9999).ToString());
    }
}
