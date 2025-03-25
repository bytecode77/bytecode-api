namespace BytecodeApi.Rest;

/// <summary>
/// Represents a callback that is triggered, when a server-sent event was received.
/// </summary>
/// <param name="type">The type of the server-sent event, usually "data", "event", "id" or "retry".</param>
/// <param name="data">The payload of the server-sent event.</param>
public delegate void ServerSentEventCallback(string type, string data);