using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;

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

    // Define type of response from OpenAI and handle error if type doesn't match expected type
    public async Task<object> SendCompletionRequestAsync(OpenAICompletionRequest completionRequest, double temperature = 0.7)
    {
        //var requestContent = new
        //{
        //    model = "gpt-3.5-turbo",
        //    messages = new[] { new { role = "user", content = messageContent } },
        //    temperature
        //};

        var serializationSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            }

        };

        var jsonContent = JsonConvert.SerializeObject(completionRequest, serializationSettings);
        var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");



        var response = await _httpClient.PostAsync("v1/chat/completions", httpContent);

        // TODO: Add error handling on malformed json response
        if (response.IsSuccessStatusCode)
        {
            var deserializationSettings = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                }
        };

        OpenAICompletionResponse? responseDTO = JsonConvert.DeserializeObject<OpenAICompletionResponse>(await response.Content.ReadAsStringAsync(), deserializationSettings);

        return responseDTO;
    }


        // Handle error response as appropriate for your application
        throw new HttpRequestException($"Error sending request: {response.StatusCode} {response.ToString()}");
        
}
}

