using Authenticate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authenticate.Infra.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(t => t.Id);
            builder.Property(x => x.Name).IsRequired().HasColumnType("NVARCHAR").HasMaxLength(100);

            builder.Property(i => i.Image).HasColumnType("VARCHAR").HasMaxLength(255).IsRequired();

            builder.OwnsOne(x => x.Email).Property(e => e.Address).HasColumnType("VARCHAR")
                    .HasColumnName("Email").HasMaxLength(255).IsRequired();

            builder.OwnsOne(x => x.Email).OwnsOne(x => x.EmailVerification).Property(x => x.Code)
                    .HasColumnName("EmailVerificationCode").IsRequired();

            builder.OwnsOne(x => x.Email).OwnsOne(x => x.EmailVerification).Property(x => x.ExpiresAt)
                    .HasColumnName("EmailExpiresAt").IsRequired(false);

            builder.OwnsOne(x => x.Email).OwnsOne(x => x.EmailVerification).Property(x => x.VerifiedAt)
                    .HasColumnName("EmailVerifiedAt").IsRequired(false);

            builder.OwnsOne(x => x.Email).OwnsOne(x => x.EmailVerification).Ignore(x => x.IsActive);


            builder.OwnsOne(x => x.Password).Property(x => x.Hash).HasColumnName("PasswordHash").IsRequired();

            builder.OwnsOne(x => x.Password).Property(x => x.ResetCode).HasColumnName("PasswordResetCode").IsRequired();
        }
    }
}
