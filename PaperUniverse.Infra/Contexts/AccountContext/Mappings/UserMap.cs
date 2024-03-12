using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaperUniverse.Core.Contexts.AccountContext.Entities;

namespace PaperUniverse.Infra.Contexts.AccountContext.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);

            builder.Property(x => x.Image)
                .IsRequired()
                .HasColumnName("Image")
                .HasColumnType("VARCHAR")
                .HasMaxLength(255);
            
            builder.Ignore(x => x.Notifications);

            builder.OwnsOne(x => x.Email)
                .Property(x => x.Address)
                .IsRequired()
                .HasColumnName("Email")
                .HasColumnType("VARCHAR")
                .HasMaxLength(120);

            builder.OwnsOne(x => x.Email)
                .OwnsOne(y => y.Verification)
                .Property(z => z.Code)
                .IsRequired()
                .HasColumnName("EmailVerificationCode")
                .HasColumnType("CHAR")
                .HasMaxLength(6);

            builder.OwnsOne(x => x.Email)
                .Ignore(y => y.Notifications);
            
            builder.OwnsOne(x => x.Email)
                .OwnsOne(y => y.Verification)
                .Property(z => z.ExpiresAt)
                .IsRequired(false)
                .HasColumnName("EmailVerificationExpiresAt")
                .HasColumnType("DATETIME");

            builder.OwnsOne(x => x.Email)
                .OwnsOne(y => y.Verification)
                .Property(z => z.VerifiedAt)
                .IsRequired(false)
                .HasColumnName("EmailVerificationVerifiedAt")
                .HasColumnType("DATETIME");

            builder.OwnsOne(x => x.Email)
                .OwnsOne(y => y.Verification)
                .Ignore(z => z.IsActive);

            builder.OwnsOne(x => x.Email)
                .OwnsOne(y => y.Verification)
                .Ignore(z => z.Notifications);

            builder.OwnsOne(x => x.Password)
                .Property(y => y.Hash)
                .IsRequired()
                .HasColumnName("Password")
                .HasColumnType("VARCHAR")
                .HasMaxLength(255);
            
            builder.OwnsOne(x => x.Password)
                .Property(y => y.ResetCode)
                .IsRequired()
                .HasColumnName("PasswordResetCode")
                .HasColumnType("CHAR")
                .HasMaxLength(8);

            builder.OwnsOne(x => x.Password)
                .Ignore(y => y.Notifications);
        }
    }
}