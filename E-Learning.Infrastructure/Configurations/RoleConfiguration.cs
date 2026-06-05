using E_Learning.Domain.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace E_Learning.Infrastructure.Configurations
{
    internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            // Table name
            builder.ToTable("Roles");
            // Primary key
            builder.HasKey(r => r.Id);
            // Properties
            builder.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(255)
                .HasConversion(n => n.Value , valu => new Name(valu));

            builder.Property(r => r.notType)
                .HasMaxLength(10)
                .HasConversion<string>()
                .IsRequired();

        }
    }
}
