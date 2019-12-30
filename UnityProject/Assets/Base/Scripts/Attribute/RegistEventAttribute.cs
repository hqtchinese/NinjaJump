using System;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class RegistEventAttribute : Attribute
{
    public Object eventType;
    public RegistEventAttribute (Object _eventType)
    {
        eventType = _eventType;
    }
}
