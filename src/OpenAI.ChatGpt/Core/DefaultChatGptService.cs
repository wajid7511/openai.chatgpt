using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using OpenAI.ChatGpt.Abstractions;
using OpenAI.ChatGpt.Abstractions.Dtos;
using OpenAI.ChatGpt.Options;


namespace OpenAI.ChatGpt.Core;

public class DefaultChatGptService : IChatGptService
{
    private readonly HttpClient _httpClient;
    private readonly ChatGptOptions _options;
    private readonly ILogger<DefaultChatGptService>? _logger;

    public DefaultChatGptService(HttpClient httpClient, IOptions<ChatGptOptions> options, ILogger<DefaultChatGptService>? logger = null)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        _logger = logger;
    }

    public async ValueTask<ChatGptResponseDto> GetChatGptResponseAsync(string prompt)
    {
        try
        {
            var requestBody = new
            {
                model = _options.Model,
                messages = new[]
                {
                    new
                    {
                        role = _options.Role,
                        content = prompt
                    }
                }
            };

            var jsonData = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_options.CompletionsUrl, content);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ChatGptRootResponse>();
            return new ChatGptResponseDto(result);
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Error while getting data from ChatGPT");
            return new ChatGptResponseDto(null, ex);
        }
    }
}
