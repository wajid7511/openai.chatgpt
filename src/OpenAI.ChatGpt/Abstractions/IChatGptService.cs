using System;
using OpenAI.ChatGpt.Abstractions.Dtos;

namespace OpenAI.ChatGpt.Abstractions;

public interface IChatGptService
{
    ValueTask<ChatGptResponseDto> GetChatGptResponseAsync(string prompt);
}
