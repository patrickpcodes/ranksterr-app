using Microsoft.AspNetCore.Mvc.Testing;

namespace Ranksterr.Api.IntegrationTests;

public class WeatherForecastTests
{
    private HttpClient _client;
    private WebApplicationFactory<Ranksterr.Api.Program> _factory;

    [SetUp]
    public void Setup()
    {
        _factory = new WebApplicationFactory<Program>();
        _client = _factory.CreateClient();
    }

    [Test]
    public async Task GetWeatherForecast_ReturnsSuccessStatusCode()
    {
        // Arrange
        var request = "/WeatherForecast";

        // Act
        var response = await _client.GetAsync( request );

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        var responseString = await response.Content.ReadAsStringAsync();
        Assert.IsNotEmpty( responseString );
    }

    [TearDown]
    public void Teardown()
    {
        _client.Dispose();
        _factory.Dispose();
    }
}