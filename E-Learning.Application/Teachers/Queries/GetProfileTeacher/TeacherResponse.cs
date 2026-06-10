using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Application.Teachers.Queries.GetProfileTeacher
{
    public sealed record TeacherResponse(
        string FullName,
        string Email,
        string PhoneNumber,
        string Address,
        string ImageUrl,
        string Education,
        string SahmCash,
        string RoleName
    );
}
