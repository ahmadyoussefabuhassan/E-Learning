
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
                 .FirstOrDefaultAsync(r => r.Name.Value == Name.Admin.Value && r.notType == NotType.Admin);
            if (adminrole is null)
                throw new ApplicationException("System Error: Admin role not found. Please run RoleSeed first.");
            var adminExists = await dbContext.Set<User>()
           .AnyAsync(u => u.Email.Value == adminEmail);
            if (!adminExists)
            {
                var adminUser = User.Create(
                    new FullName("Admin"),
                    new Email(adminEmail),
                    new Password(adminPassword),
                    new PhoneNumber("+963 955920653"),
                    new Address("دمشق"),
                    null,
                    adminrole.Id);
                await dbContext.Set<User>().AddAsync(adminUser);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
