using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

using var db = new ForthbroughtFadungFramework();

Console.WriteLine($"Database path: {db.DbPath}.");

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApiDocument();

//builder.Services.AddScoped<IMyService, MyService>();

builder.Services.AddHttpClient<OpenAIAPIService>();
// TODO: Define interface for service
builder.Services.AddSingleton(x =>
{
    string apiKey = builder.Configuration["AppSettings:OpenAIapiKey"];
    if (apiKey == null)
    {
        throw new InvalidOperationException("OpenAI API key is not configured.");
    }
    return new OpenAIAPIService(x.GetRequiredService<HttpClient>(), apiKey);
});



#if DEBUG
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "debug-cors",
                      policy =>
                      {
                          policy.WithOrigins("*")
                                .AllowAnyMethod()
                                .AllowAnyHeader(); 
                      });
});
#endif



var app = builder.Build();

#if DEBUG
app.UseCors("debug-cors");
#endif

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Add OpenAPI 3.0 document serving middleware
    // Available at: http://localhost:<port>/swagger/v1/swagger.json
    app.UseOpenApi();

    // Add web UIs to interact with the document
    // Available at: http://localhost:<port>/swagger
    app.UseSwaggerUi3();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

