using System;

namespace OpenAI.ChatGpt.Abstractions.Dtos;

public class ChatGptResponseDto
{
    public ChatGptResponseDto(ChatGptRootResponse? document, Exception? exception = null)
    {
        Response = document;
        Exception = exception;
    }

    public ChatGptRootResponse? Response { get; set; }
    public bool IsError => Response == null || Exception != null;
    public Exception? Exception { get; set; }
}
public class ChatGptRootResponse
{
    public string Id { get; set; } = string.Empty;
    public string Object { get; set; } = string.Empty;
    public long Created { get; set; }
    public List<Choice> Choices { get; set; } = new();
    public Usage? Usage { get; set; }
}

public class Choice
{
    public string Text { get; set; } = string.Empty;
    public int Index { get; set; }
    public object? Logprobs { get; set; }
    public string FinishReason { get; set; } = string.Empty;
}

public class Usage
{
    public int PromptTokens { get; set; }
    public int CompletionTokens { get; set; }
    public int TotalTokens { get; set; }
}
