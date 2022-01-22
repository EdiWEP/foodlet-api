using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodletAPI.Entities.Configurations
{
    public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnType("nvarchar(36)").HasMaxLength(36);

            builder.HasOne(r => r.User).WithMany(u => u.Recipes).HasForeignKey(r => r.UserId).IsRequired(true);

            builder.Property(x => x.Name).IsRequired(true)
                .HasColumnType("nvarchar(150)").HasMaxLength(150);

            builder.Property(x => x.Calsperg).IsRequired(true);
            builder.Property(x => x.Carbs).IsRequired(true);
            builder.Property(x => x.Fat).IsRequired(true);
            builder.Property(x => x.Protein).IsRequired(true);

            builder.Property(x => x.NumberOfIngredients).IsRequired(true);
            builder.Property(x => x.ServingSize).IsRequired(true);

        }
    }
}
