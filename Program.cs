var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register HttpClient for making external requests
builder.Services.AddHttpClient();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapGet("/posts", async (HttpClient httpClient) =>
{
    var response = await httpClient.GetStringAsync("https://jsonplaceholder.typicode.com/posts");
    return Results.Content(response, "application/json");
})
.WithName("GetPosts")
.WithOpenApi();

app.MapGet("/comments", async (HttpClient httpClient) =>
{
    var response = await httpClient.GetStringAsync($"https://jsonplaceholder.typicode.com/comments");
    return Results.Content(response, "application/json");
})
.WithName("GetComments")
.WithOpenApi();

app.MapGet("/albums", async (HttpClient httpClient) =>
{
    var response = await httpClient.GetStringAsync("https://jsonplaceholder.typicode.com/albums");
    return Results.Content(response, "application/json");
}) 
.WithName("GetAlbums")
.WithOpenApi();

app.MapGet("/photos", async (HttpClient httpClient) =>
{
    var response = await httpClient.GetStringAsync("https://jsonplaceholder.typicode.com/photos");
    return Results.Content(response, "application/json");
})
.WithName("GetPhotos")
.WithOpenApi();

app.MapGet("/todos", async (HttpClient httpClient) =>
{
    var response = await httpClient.GetStringAsync("https://jsonplaceholder.typicode.com/todos");
    return Results.Content(response, "application/json");
})
.WithName("GetTodos")
.WithOpenApi();

app.MapGet("/users", async (HttpClient httpClient) =>
{
    var response = await httpClient.GetStringAsync("https://jsonplaceholder.typicode.com/users");
    return Results.Content(response, "application/json");
})
.WithName("GetUsers")
.WithOpenApi();
app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
