var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://0.0.0.0:8080");

builder.Services.AddOpenApi();

var app = builder.Build();

app.MapOpenApi();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};


app.MapGet("/", () => "API de Cristian Chamo, Keily Lopez y Delmi Fajardo - Estado: ONLINE");

app.MapGet("/status", () => new {
    Estado = "Servidor Activo",
    Estudiantes = "Cristian Chamo, Keily Lopez y Delmi Fajardo",
    Universidad = "Mariano Gálvez",
    Proyecto = "Hidden Valley API",
    FechaHora = DateTime.Now.ToString("G"),
    Rutas = new[] { "/", "/status", "/weatherforecast" }
});

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
});

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}