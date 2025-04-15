using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RecipeRecommendation.Domain.Entities
{
    public class RecipeWrapper
    {
        [JsonPropertyName("recipes")]
        public List<Recipe> Recipes { get; set; }
    }

    public class Recipe
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("ingredients")]
        public List<string> Ingredients { get; set; }

        [JsonPropertyName("instructions")]
        public List<string> Instructions { get; set; }

        [JsonPropertyName("difficulty")]
        public string Difficulty { get; set; }

        [JsonPropertyName("estimatedCookingTime")]
        public string EstimatedCookingTime { get; set; }

        [JsonPropertyName("whyItWorks")]
        public string WhyItWorks {  get; set; }
    }
}
