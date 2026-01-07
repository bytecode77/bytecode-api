namespace BytecodeApi.Rest;

/// <summary>
/// Represents a callback that is triggered, when a server-sent event was received.
/// </summary>
/// <param name="eventType">The type of the server-sent event.</param>
/// <param name="data">The payload of the server-sent event.</param>
/// <param name="id">The id of the server-sent event.</param>
public delegate void ServerSentEventCallback(string eventType, string data, string? id);