namespace UE.Core.Common.Exceptions;

/// <summary>
/// Custom exception class representing an invalid configuration scenario.
/// </summary>
public class InvalidConfigurationException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidConfigurationException"/> class with the specified key.
    /// </summary>
    /// <param name="key">The key associated with the invalid configuration.</param>
    public InvalidConfigurationException(string key) : base($"Configurations are invalid for the key: {key}")
    {

    }
}
