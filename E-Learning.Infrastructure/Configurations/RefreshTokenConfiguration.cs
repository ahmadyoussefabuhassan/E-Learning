using E_Learning.Domain.RefreshTokens;
using E_Learning.Domain.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Learning.Infrastructure.Configurations
{
    internal sealed class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            // table name
            builder.ToTable("RefreshTokens");
            // primary key
            builder.HasKey(x => x.Id);
            // Properties
            builder.Property(x => x.Token)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(x => x.Expires)
                .IsRequired();
            builder.Property(x => x.CreatedAt)
                .IsRequired();
            builder.Property(x => x.JWTId)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(x => x.IsUsed)
                .IsRequired()
                .HasDefaultValue(false); 
            builder.Property(x => x.IsRevoked)
                .IsRequired()
                .HasDefaultValue(false); 
            // Indexes
            builder.HasIndex(x => x.Token).IsUnique();
            // Relationships
            builder.HasOne(r => r.User)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
