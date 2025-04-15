using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RecipeRecommendation.Domain.Interfaces
{
    public interface IGeminiAIService
    {
        Task<string> GenerateResponseAsync(string input);

       // Task GenerateResponseAsync(string input);
       
    }
}
