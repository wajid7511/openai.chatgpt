using System;

namespace OpenAI.ChatGpt.Options;

public class ChatGptOptions
{
    public const string CONFIG_PATH = "OpenAI:ChatGPT";
    public string CompletionsUrl { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}

