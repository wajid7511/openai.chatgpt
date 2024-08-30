
using System.Net;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using OpenAI.ChatGpt.Abstractions.Dtos;
using OpenAI.ChatGpt.Core;
using OpenAI.ChatGpt.Options;

namespace OpenAI.ChatGpt.Tests;

[TestClass]
public class DefaultChatGptServiceTests
{
    private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock = new();
    private readonly Mock<IOptions<ChatGptOptions>> _optionsMock = new();
    private readonly Mock<ILogger<DefaultChatGptService>> _loggerMock = new();
    private HttpClient _httpClient = null!;
    private DefaultChatGptService _service = null!;

    [TestInitialize]
    public void Setup()
    {
        // Mock the options 
        _optionsMock.Setup(o => o.Value).Returns(new ChatGptOptions
        {
            Model = "gpt-3.5-turbo",
            Role = "user",
            CompletionsUrl = "https://api.openai.com/v1/completions",
            ApiKey = "testing-key"
        });
        // Create HttpClient with mocked handler
        _httpClient = new HttpClient(_httpMessageHandlerMock.Object);

        // Create the service
        _service = new DefaultChatGptService(_httpClient, _optionsMock.Object, _loggerMock.Object);
    }

    [TestMethod]
    public async Task GetChatGptResponseAsync_ShouldReturnResponse_WhenApiCallIsSuccessful()
    {
        // Arrange
        var prompt = "What is the capital of France?";
        var apiResponse = new ChatGptRootResponse
        {
            // Assuming ChatGptRootResponse has a field called Choices
            Choices = [new Choice { Text = "Paris" }]
        };

        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize(apiResponse))
            });

        // Act
        var result = await _service.GetChatGptResponseAsync(prompt);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Response);
        Assert.AreEqual("Paris", result.Response.Choices[0].Text);
    }

    [TestMethod]
    public async Task GetChatGptResponseAsync_ShouldLogError_WhenApiCallFails()
    {
        // Arrange
        var prompt = "What is the capital of France?";
        var exceptionMessage = "Network error";

        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ThrowsAsync(new HttpRequestException(exceptionMessage));

        // Act
        var result = await _service.GetChatGptResponseAsync(prompt);

        // Assert
        Assert.IsNull(result.Response);
        Assert.IsNotNull(result.Exception);
        Assert.AreEqual(exceptionMessage, result.Exception.Message);
        _loggerMock.Verify(
                l => l.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString() == "Error while getting data from ChatGPT"),
                    It.Is<Exception>(ex => ex.Message == exceptionMessage),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
    }
}