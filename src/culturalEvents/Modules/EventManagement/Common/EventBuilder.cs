using culturalEvents.Shared.Domain;

namespace culturalEvents.Modules.EventManagement.Common;

public interface IEventBuilder
{
    EventBuilder WithName(string name);
    EventBuilder WithDate(DateTime date);
    EventBuilder WithCategory(EventCategory category);
    EventBuilder WithStatus(EventStatus status);
    EventBuilder WithValue(EventValue value);
    CulturalEvent Build();
}

public sealed class EventBuilder : IEventBuilder
{
    private readonly CulturalEvent _event;

    public EventBuilder()
    {
        _event = new CulturalEvent();
    }

    public EventBuilder WithName(string name)
    {
        _event.Name = name;
        return this;
    }

    public EventBuilder WithDate(DateTime date)
    {
        _event.UtcDate = date;
        return this;
    }
    
    public EventBuilder WithCategory(EventCategory category)
    {
        _event.Category = category;
        return this;
    }

    public EventBuilder WithStatus(EventStatus status)
    {
        _event.Status = status;
        return this;
    }

    public EventBuilder WithValue(EventValue value)
    {
        _event.Value = value;
        return this;
    }

    public CulturalEvent Build()
    {
        return _event;
    }

}
