using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RecipeRecommendation.Infrastructure.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RecipeRecommendation.Infrastructure.Helper
{
    public class GetResponse
    {

        private static GoogleApiSettings? _googleApiSettings;
        public static string Key { get; private set; } = string.Empty; // Avoid null issues

        /// <summary>
        /// Initializes the API key from configuration. Must be called before using the class.
        /// </summary>
        public static void Initialize(IOptions<GoogleApiSettings> googleApiSettings)
        {
            _googleApiSettings = googleApiSettings.Value ?? throw new ArgumentNullException(nameof(googleApiSettings), "Google API settings cannot be null.");

            if (string.IsNullOrEmpty(_googleApiSettings.ApiKey))
                throw new InvalidOperationException("API Key is missing. Ensure it is set in appsettings.Development.json.");

            Key = _googleApiSettings.ApiKey;
            Console.WriteLine($"Google API Key Initialized: {Key.Substring(0,6)}");
        }
        public GetResponse()
        {
         
        }

        /// <summary>
        /// Appends api key to url
        /// </summary>
        /// <returns> The complete url with api key </returns>
        private static string AppendKeyToUrl(string Key)
        {

            var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={Key}";
            return url;
        }


        public static async Task<string> GetGeminiResponse(string prompt)
        {
            using (HttpClient client = new HttpClient())
            {
                var requestBody = new
                {
                    contents = new[]
                    {
                            new
                            {
                                parts = new[]
                                {
                                    new { text = prompt }
                                }
                            }
                        }
                };

                string json = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(AppendKeyToUrl(Key), content);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<dynamic>(responseBody);

                return result?.candidates?[0]?.content?.parts?[0]?.text ?? "No response from API.";

               /* var jsonDoc = JsonDocument.Parse(result);
                var text = jsonDoc.RootElement
                                  .GetProperty("candidates")[0]
                                  .GetProperty("content")
                                  .GetProperty("parts")[0]
                                  .GetProperty("text")
                                  .GetString();

               return text ?? "No recipe generated.";*/
            
        }
        }
    }
}
