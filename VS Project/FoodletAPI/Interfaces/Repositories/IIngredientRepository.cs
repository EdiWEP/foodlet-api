using FoodletAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodletAPI.Interfaces.Repositories
{
    public interface IIngredientRepository : IBaseRepository<Ingredient>
    {
        void CreateRangeFromList(List<Ingredient> ingredients);

        Task<Ingredient> GetByName(string name);
        Task<List<Ingredient>> GetAllDefault();

        // Gets all ingredients accessible to user with userId(includes the defaults)
        Task<List<Ingredient>> GetByUserId(string userId);

        // Gets all ingredients added by the user with userId
        Task<List<Ingredient>> GetAllOfUser(string userId);

        Task<List<Ingredient>> SearchByName(string term, string userId);
    }
}
