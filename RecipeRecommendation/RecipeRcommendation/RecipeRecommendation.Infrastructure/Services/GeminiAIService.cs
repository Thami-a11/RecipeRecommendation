using DotnetGeminiSDK.Client;
using DotnetGeminiSDK.Client.Interfaces;
using DotnetGeminiSDK.Model.Response;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RecipeRecommendation.Domain.Interfaces;
using RecipeRecommendation.Infrastructure.Helper;
using RecipeRecommendation.Infrastructure.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeRecommendation.Infrastructure.Services
{
    public class GeminiAIService : IGeminiAIService
    {
        private readonly IServiceProvider serviceProvider; 

        public GeminiAIService(IServiceProvider serviceProvider) 
        {
            this.serviceProvider = serviceProvider; 
        }

        public async Task<string> GenerateResponseAsync(string prompt)
        {
            var googleApiSettings = serviceProvider.GetRequiredService<IOptions<GoogleApiSettings>>();

            GetResponse.Initialize(googleApiSettings);

            var response = await GetResponse.GetGeminiResponse(prompt);
            return response;
        }
    }
}
