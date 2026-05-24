using Microsoft.EntityFrameworkCore;
using E_Learning.Domain.Roles;

namespace E_Learning.Infrastructure.DateSeeds
{
    public static class RolesSeed
    {
        public static async Task SeedRolesAsync(ApplicationDbContext dbContext)
        {
            if (!await dbContext.Set<Role>().AnyAsync())
            {
                var roles = new List<Role>
                {
                    Role.Create(Name.Admin, NotType.Admin),
                    Role.Create(Name.Teacher, NotType.Teacher),
                    Role.Create(Name.Student, NotType.Student)
                };
                await dbContext.Set<Role>().AddRangeAsync(roles);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
