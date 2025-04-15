using RecipeRecommendation.Domain.Entities;
using RecipeRecommendation.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RecipeRecommendation.Application.Services
{
    public class RecommedationService : IRecipeRecommendationService
    {
        private readonly IGeminiAIService _geminiAIService;
        private readonly string _inputStringTemplate =
"Suggest recipes based on the following ingredients: {0}. " +
    "Return a JSON object in **strict C#-friendly format** using this exact schema: " +
    "{{ \"recipes\": [ {{ \"title\": string, \"ingredients\": string[], \"instructions\": string[], " +
    "\"difficulty\": string, \"estimatedCookingTime\": string, \"whyItWorks\": string }} ] }}. " +
    "The response MUST: " +
    "(1) include only valid, double-quoted JSON — NO markdown (no ```), " +
    "(2) contain no extra text before or after the JSON, " +
    "(3) be parsable by System.Text.Json without modification.";



        public RecommedationService(IGeminiAIService geminiAIService)
        {
            _geminiAIService = geminiAIService;
        }

        public async Task<List<Recipe>> GetRecommendedRecipesAsync(GivenIngredients ingredients)
        {
            if (ingredients?.Ingredients == null || !ingredients.Ingredients.Any())
            {
                throw new ArgumentException("Ingredients cannot be null or empty", nameof(ingredients));
            }

            string input = string.Format(_inputStringTemplate, string.Join(", ", ingredients.Ingredients));

            var aiResponse = await _geminiAIService.GenerateResponseAsync(input);

            RecipeWrapper recommendedRecipes = ParseAiResponseToWrapper(aiResponse);

            return recommendedRecipes?.Recipes ?? new List<Recipe>();
        }

        public async Task<string> GetRecommendedRecipesAsStringAsync(GivenIngredients ingredients)
        {
            if (ingredients?.Ingredients == null || !ingredients.Ingredients.Any())
            {
                throw new ArgumentException("Ingredients cannot be null or empty", nameof(ingredients));
            }

            string input = string.Format(_inputStringTemplate, string.Join(", ", ingredients.Ingredients));

            var aiResponse = await _geminiAIService.GenerateResponseAsync(input);

            return aiResponse;
        }

        private List<Recipe> ParseAiResponse(string aiResponse)
        {
            var cleanedResponse = CleanJson(aiResponse);
            var recipes = JsonSerializer.Deserialize<List<Recipe>>(cleanedResponse) ?? new List<Recipe>();
            return recipes;
        }
        private RecipeWrapper ParseAiResponseToWrapper(string aiResponse)
        {
            var cleanedResponse = CleanJson(aiResponse);
            var recipes = JsonSerializer.Deserialize<RecipeWrapper>(cleanedResponse);
            return recipes;
        }
        private string CleanJson(string aiResponse)
        {
            // Remove markdown code block wrappers (```json ... ```)
            if (aiResponse.StartsWith("```"))
            {
                aiResponse = Regex.Replace(aiResponse, "^```[a-z]*\\s*", "", RegexOptions.IgnoreCase);
                aiResponse = Regex.Replace(aiResponse, "\\s*```$", "", RegexOptions.IgnoreCase);
            }

            // Trim whitespace
            return aiResponse.Trim();
        }

    }
}
