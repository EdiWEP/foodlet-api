using FoodletAPI.Entities;
using FoodletAPI.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodletAPI.Repositories
{
    public class IngredientRepository : BaseRepository<Ingredient>, IIngredientRepository
    {
       
        public IngredientRepository(AppDbContext dbcontext) : base(dbcontext) { }

        
    }
}
