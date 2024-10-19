using Newtonsoft.Json;

namespace UE.Core.Extensions;

#nullable disable

/// <summary>
/// Provides extension methods for JSON serialization using Newtonsoft.Json.
/// </summary>
public static class ObjectExtensions
{
    /// <summary>
    /// Serializes an object to a JSON string using Newtonsoft.Json.
    /// </summary>
    /// <typeparam name="T">The type of the object to serialize.</typeparam>
    /// <param name="obj">The object to serialize.</param>
    /// <returns>A JSON string representing the serialized object.</returns>
    public static string ToJson<T>(this T obj)
    {
        // Use JsonConvert.SerializeObject to serialize the object to JSON
        return obj != null ? JsonConvert.SerializeObject(obj) : null;
    }
}
