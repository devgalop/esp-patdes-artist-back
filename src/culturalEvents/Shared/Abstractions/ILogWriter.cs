namespace culturalEvents.Shared.Abstractions;

public enum MessageType
{
    INFO,
    WARNING,
    ERROR,
    DEBUG,
    CRITICAL
}

public interface ILogWriter
{
    void Write(string message, string source, MessageType type);
}
