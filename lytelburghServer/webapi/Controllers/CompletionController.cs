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

    //[HttpGet(Name = "GetCompletion")]
    //public async Task<string> Get()
    //{
    //    return await _openAIAPIService.SendCompletionRequestAsync("hey...");
    //}

    public async Task<IActionResult> PostCompletionRequest([FromBody] OpenAICompletionRequest completionRequestDTO)
    {
        if (completionRequestDTO == null )
        {
            return BadRequest(new { message = "Invalid request" });
        }

        var completion = await _openAIAPIService.SendCompletionRequestAsync(completionRequestDTO);
        return Ok(new { completion });




        // https://stackoverflow.com/questions/23577021/ignore-null-values-when-serializing-json
        return Ok();

    }

    //public class CompletionRequestDTO
    //{
    //    public string? Content { get; set; }
    //}

}
