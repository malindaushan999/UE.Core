namespace UE.Core.Common.Exceptions;

/// <summary>
/// Custom exception class representing a "Not Found" error, typically used
/// when an entity or resource is not found in the system.
/// </summary>
public class NotFoundException : Exception
{
    /// <summary>
    /// Constructor for creating a `NotFoundException` with a custom error message.
    /// </summary>
    /// <param name="name">The name or description of the entity or resource that was not found.</param>
    /// <param name="key">The key or identifier of the missing entity or resource.</param>
    public NotFoundException(string? name, object key) : base($"{name} ({key}) was not found")
    {

    }
}
