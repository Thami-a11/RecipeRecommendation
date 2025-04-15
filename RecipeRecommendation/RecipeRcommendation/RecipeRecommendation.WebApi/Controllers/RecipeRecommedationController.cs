using Microsoft.AspNetCore.Mvc;
using RecipeRecommendation.Domain.Entities;
using RecipeRecommendation.Domain.Interfaces;

namespace RecipeRecommendation.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipeRecommedationController : ControllerBase
    {
        private readonly IRecipeRecommendationService _recipeRecommendationService;

        public RecipeRecommedationController(IRecipeRecommendationService recipeRecommendationService)
        {
            _recipeRecommendationService = recipeRecommendationService;
        }

        [HttpPost("recommend")]
        public async Task<IActionResult> GetRecommendedRecipes([FromBody] GivenIngredients ingredients)
        {
            var recommendedRecipes = await _recipeRecommendationService.GetRecommendedRecipesAsync(ingredients);
            return Ok(recommendedRecipes);  
        }

        [HttpPost("recommendAsString")]
        public async Task<IActionResult> GetRecommendedRecipesAsString([FromBody] GivenIngredients ingredients)
        {
            var recommendedRecipes = await _recipeRecommendationService.GetRecommendedRecipesAsStringAsync(ingredients);
            return Ok(recommendedRecipes);
        }
    }
}
