using culturalEvents.Shared.Abstractions;

namespace culturalEvents.Shared.Infrastructure.Logger;

public sealed class ConsoleLogWriter : ILogWriter
{
    public void Write(string message, string source, MessageType type)
    {
        Console.WriteLine($"[{DateTime.UtcNow}] - [{type}] - [{source}] - {message}");
    }
}

public static class ConsoleLogWriterExtensions
{
    public static WebApplicationBuilder AddLogWriter(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<ILogWriter, ConsoleLogWriter>();
        return builder;
    }
}
