using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodletAPI.Entities
{
    public class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
    {
        public void Configure(EntityTypeBuilder<Ingredient> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnType("nvarchar(36)").HasMaxLength(36);

            builder.Property(x => x.Name).IsRequired(true)
                .HasColumnType("nvarchar(150)").HasMaxLength(150);

            builder.Property(x => x.Calsperg).IsRequired(true);
            builder.Property(x => x.Carbs).IsRequired(true);
            builder.Property(x => x.Fat).IsRequired(true);
            builder.Property(x => x.Protein).IsRequired(true);
            
            
        }

    }
}
