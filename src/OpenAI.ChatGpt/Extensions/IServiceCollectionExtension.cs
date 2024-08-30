using System;
using Microsoft.Extensions.Options;
using OpenAI.ChatGpt.Abstractions;
using OpenAI.ChatGpt.Core;
using OpenAI.ChatGpt.Options;

namespace OpenAI.ChatGpt.Extensions;

public static class IServiceCollectionExtension
{
    public static IServiceCollection AddCoreDI(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ChatGptOptions>(configuration.GetSection(ChatGptOptions.CONFIG_PATH));

        services.AddHttpClient<IChatGptService, DefaultChatGptService>((serviceProvider, httpClient) =>
    {
        var options = serviceProvider.GetRequiredService<IOptions<ChatGptOptions>>().Value;
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {options.ApiKey}");
    });
        return services;
    }
}
