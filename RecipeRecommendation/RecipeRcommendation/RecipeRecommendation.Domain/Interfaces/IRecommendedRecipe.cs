using RecipeRecommendation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeRecommendation.Domain.Interfaces
{
    public interface IRecipeRecommendationService
    {
        Task<List<Recipe>> GetRecommendedRecipesAsync(GivenIngredients ingredients);
        Task<string> GetRecommendedRecipesAsStringAsync(GivenIngredients ingredients);
    }
}
