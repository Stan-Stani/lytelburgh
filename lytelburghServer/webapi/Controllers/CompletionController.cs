using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class CompletionController : ControllerBase
{

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly OpenAIAPIService _openAIAPIService;

    public CompletionController(ILogger<WeatherForecastController> logger, OpenAIAPIService openAIApiService)
    {
        _logger = logger;
        _openAIAPIService = openAIApiService;
    }

    [HttpGet(Name = "GetCompletion")]
    public async Task<string> Get()
    {
        return await _openAIAPIService.SendCompletionRequestAsync("hey...");
    }

    public async Task<IActionResult> PostCompletionRequest([FromBody] CompletionRequestDTO completionRequestDTO)
    {
        if (completionRequestDTO == null || string.IsNullOrEmpty(completionRequestDTO.Content))
        {
            return BadRequest(new { message = "Invalid request" });
        }

        var completion = await _openAIAPIService.SendCompletionRequestAsync(completionRequestDTO.Content);
        return Ok(new { completion });

    }

    public class CompletionRequestDTO
    {
        public string Content { get; set; }
    }

}
