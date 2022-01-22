using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodletAPI.Entities.Configurations
{
    public class RecipeIngredientConfiguration : IEntityTypeConfiguration<RecipeIngredient>
    {
        public void Configure(EntityTypeBuilder<RecipeIngredient> builder)
        {

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnType("nvarchar(36)").HasMaxLength(36);

            builder.HasOne(x => x.Ingredient).WithMany(i => i.Recipes).HasForeignKey(x => x.IngredientId).IsRequired(true);
            builder.HasOne(x => x.Recipe).WithMany(i => i.RecipeIngredients).HasForeignKey(x => x.RecipeId).IsRequired(true);

            builder.Property(x => x.Grams).IsRequired(true);

        }

    }
}
