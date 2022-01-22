using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodletAPI.Entities.Configurations
{
    public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnType("nvarchar(36)").HasMaxLength(36);

            builder.HasOne(x => x.User).WithOne(x => x.Profile).HasForeignKey<UserProfile>(x => x.UserId);

            builder.Property(x => x.FullName).HasColumnType("nvarchar(150)").HasMaxLength(150).IsRequired(false);
            builder.Property(x => x.PhoneNumber).HasColumnType("nvarchar(20)").HasMaxLength(20).IsRequired(false);
            builder.Property(x => x.Description).HasColumnType("nvarchar(500)").HasMaxLength(150).IsRequired(false);


        }

    }
    
    
}
