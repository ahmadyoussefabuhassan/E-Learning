
using E_Learning.Domain.Roles;
using E_Learning.Domain.User;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Infrastructure.DateSeeds
{
    public static class AdminSeed
    {
        public static async Task SeedAdminAsync(ApplicationDbContext dbContext)
            
        {
            var adminEmail = "admin@ELearning.com";
            var adminPassword = "A@s$w0rd";
            var adminrole = await dbContext.Set<Role>()
                 .FirstOrDefaultAsync(r =>  r.notType == NotType.Admin);
            if (adminrole is null)
                throw new ApplicationException("System Error: Admin role not found. Please run RoleSeed first.");
            var adminEmailObject = new Domain.User.Email(adminEmail);
            var adminExists = await dbContext.Set<User>()
           .AnyAsync(u => u.Email == adminEmailObject);
            if (!adminExists)
            {
                var adminUser = User.Create(
                    new FullName("Admin"),
                    new Domain.User.Email(adminEmail),
                    new Password(adminPassword),
                    new PhoneNumber("+963955920653"),
                    new Address("دمشق"),
                    null,
                    adminrole.Id);
                await dbContext.Set<User>().AddAsync(adminUser);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
