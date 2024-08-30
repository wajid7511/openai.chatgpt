using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Moq;
using OpenAI.ChatGpt.Abstractions;
using OpenAI.ChatGpt.Extensions;
using OpenAI.ChatGpt.Options;

namespace OpenAI.ChatGpt.Tests;


[TestClass]
public class IServiceCollectionExtensionTests
{
    [TestMethod]
    public void AddCoreDI_ShouldRegisterServicesCorrectly()
    {
        // Arrange
        IServiceCollection services = new ServiceCollection();
        // Use a real ConfigurationBuilder to set up configuration
        var data = new KeyValuePair<string, string?>($"{ChatGptOptions.CONFIG_PATH}:ApiKey", "test-api-key");
        IEnumerable<KeyValuePair<string, string?>> keyValuePairs = [data];

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(keyValuePairs)
            .Build();

        // Act
        services.AddCoreDI(configuration);
        var serviceProvider = services.BuildServiceProvider();

        // Assert
        // Check that ChatGptOptions is configured correctly
        var options = serviceProvider.GetRequiredService<IOptions<ChatGptOptions>>().Value;
        Assert.AreEqual("test-api-key", options.ApiKey);

        // Check that IChatGptService is registered and the HttpClient is configured
        var chatGptService = serviceProvider.GetRequiredService<IChatGptService>();
        Assert.IsNotNull(chatGptService);

        // Verify that the HttpClient has the correct authorization header
        var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
        var httpClient = httpClientFactory.CreateClient(nameof(IChatGptService));
        Assert.IsTrue(httpClient.DefaultRequestHeaders.Authorization?.Parameter == "test-api-key");
    }
}
