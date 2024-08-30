using OpenAI.ChatGpt.Abstractions.Dtos;
namespace OpenAI.ChatGpt.Tests;


[TestClass]
public class ChatGptResponseDtoTests
{
    [TestMethod]
    public void ChatGptResponseDto_ShouldSetResponseAndExceptionCorrectly_WhenBothAreProvided()
    {
        // Arrange
        var chatGptResponse = new ChatGptRootResponse();
        var exception = new Exception("Test exception");

        // Act
        var dto = new ChatGptResponseDto(chatGptResponse, exception);

        // Assert
        Assert.AreEqual(chatGptResponse, dto.Response);
        Assert.AreEqual(exception, dto.Exception);
        Assert.IsTrue(dto.IsError);
    }

    [TestMethod]
    public void ChatGptResponseDto_ShouldIndicateError_WhenResponseIsNullAndExceptionIsProvided()
    {
        // Arrange
        var exception = new Exception("Test exception");

        // Act
        var dto = new ChatGptResponseDto(null, exception);

        // Assert
        Assert.IsNull(dto.Response);
        Assert.AreEqual(exception, dto.Exception);
        Assert.IsTrue(dto.IsError);
    }

    [TestMethod]
    public void ChatGptResponseDto_ShouldIndicateError_WhenResponseIsNullAndExceptionIsNotProvided()
    {
        // Arrange
        // No response and no exception provided

        // Act
        var dto = new ChatGptResponseDto(null);

        // Assert
        Assert.IsNull(dto.Response);
        Assert.IsNull(dto.Exception);
        Assert.IsTrue(dto.IsError);
    }

    [TestMethod]
    public void ChatGptResponseDto_ShouldNotIndicateError_WhenResponseIsProvidedAndExceptionIsNull()
    {
        // Arrange
        var chatGptResponse = new ChatGptRootResponse();

        // Act
        var dto = new ChatGptResponseDto(chatGptResponse);

        // Assert
        Assert.AreEqual(chatGptResponse, dto.Response);
        Assert.IsNull(dto.Exception);
        Assert.IsFalse(dto.IsError);
    }
}

