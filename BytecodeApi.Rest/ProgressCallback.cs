namespace BytecodeApi.Rest;

/// <summary>
/// Represents a callback that is triggered, while a download or upload is in progress.
/// </summary>
/// <param name="bytesRead">The number of bytes read.</param>
/// <param name="totalBytes">The total size of the download or upload.</param>
public delegate void ProgressCallback(long bytesRead, long totalBytes);