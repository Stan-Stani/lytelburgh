using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

public class OpenAIAPIService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public OpenAIAPIService(HttpClient httpClient, string apiKey)
    {
        _httpClient = httpClient;
        _apiKey = apiKey;

        _httpClient.BaseAddress = new System.Uri("https://api.openai.com/");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
        
    }

    public async Task<string> SendCompletionRequestAsync(string messageContent, double temperature = 0.7)
    {
        var requestContent = new
        {
            model = "gpt-3.5-turbo",
            messages = new[] { new { role = "user", content = messageContent } },
            temperature
        };

        var jsonContent = JsonConvert.SerializeObject(requestContent);
        var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("v1/chat/completions", httpContent);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();
        }

        // Handle error response as appropriate for your application
        throw new HttpRequestException($"Error sending request: {response.StatusCode}");
    }
}

public class OpenAICompletionRequest
{
    string Model { get; set; } = "gpt-3.5-turbo";
    ICollection<Message> Messages { get; set; }
    double Temperature { get; set; } = 0.7;

    OpenAICompletionRequest(ICollection<Message> messages)
    {
        Messages = messages;
    }

    public class Message
    {
        [JsonConverter(typeof(StringEnumConverter), typeof(CamelCaseNamingStrategy))]
        public RoleEnum Role { get; set; } = RoleEnum.User;
        public string Content { get; set; }

        public Message(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                throw new ArgumentException("Content cannot be null or empty");
            }

            Content = content;
        }

        public enum RoleEnum
        {
            System,
            User,
            Assistant,
            Function

        }
    }

}

