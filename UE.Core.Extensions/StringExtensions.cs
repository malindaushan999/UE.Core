using HtmlAgilityPack;
using Newtonsoft.Json;
using UE.Core.Security;

namespace UE.Core.Extensions;

/// <summary>
/// Provides a set of extension methods for string manipulation and conversion.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Encrypts the input string using a default password.
    /// </summary>
    /// <param name="str">The string to be encrypted.</param>
    /// <returns>The encrypted string.</returns>
    public static string Encrypt(this string str)
    {
        Cipher cipher = new();
        return cipher.Encrypt(str);
    }

    /// <summary>
    /// Encrypts the input string using the provided password.
    /// </summary>
    /// <param name="str">The string to be encrypted.</param>
    /// <param name="password">The password used for encryption.</param>
    /// <returns>The encrypted string.</returns>
    public static string Encrypt(this string str, string password)
    {
        Cipher cipher = new(password);
        return cipher.Encrypt(str);
    }

    /// <summary>
    /// Decrypts the input encrypted text using the default password.
    /// </summary>
    /// <param name="str">The encrypted text to be decrypted.</param>
    /// <returns>The decrypted string.</returns>
    public static string Decrypt(this string str)
    {
        Cipher cipher = new();
        return cipher.Decrypt(str);
    }

    /// <summary>
    /// Decrypts the input string and converts it to the specified value type.
    /// </summary>
    /// <typeparam name="T">The value type to which the decrypted string should be converted.</typeparam>
    /// <param name="str">The input string to be decrypted and converted.</param>
    /// <returns>The decrypted and converted value of type T.</returns>
    /// <remarks>
    /// This method assumes that T is a value type (struct) and uses Convert.ChangeType
    /// to convert the decrypted string to the specified value type.
    /// </remarks>
    public static T Decrypt<T>(this string str) where T : struct
    {
        Cipher cipher = new();
        return (T)Convert.ChangeType(cipher.Decrypt(str), typeof(T));
    }

    /// <summary>
    /// Decrypts the input encrypted text using the provided password.
    /// </summary>
    /// <param name="str">The encrypted text to be decrypted.</param>
    /// <param name="password">The password used for decryption.</param>
    /// <returns>The decrypted string.</returns>
    public static string Decrypt(this string str, string password)
    {
        Cipher cipher = new(password);
        return cipher.Decrypt(str);
    }

    /// <summary>
    /// Decrypts the input string and converts it to the specified value type.
    /// </summary>
    /// <typeparam name="T">The value type to which the decrypted string should be converted.</typeparam>
    /// <param name="str">The input string to be decrypted and converted.</param>
    /// <param name="password">The password used for decryption.</param>
    /// <returns>The decrypted and converted value of type T.</returns>
    /// <remarks>
    /// This method assumes that T is a value type (struct) and uses Convert.ChangeType
    /// to convert the decrypted string to the specified value type.
    /// </remarks>
    public static T Decrypt<T>(this string str, string password) where T : struct
    {
        Cipher cipher = new(password);
        return (T)Convert.ChangeType(cipher.Decrypt(str), typeof(T));
    }

    /// <summary>
    /// Converts the input string to a 16-bit signed integer (short).
    /// </summary>
    /// <param name="str">The string to be converted.</param>
    /// <returns>A 16-bit signed integer (short) representation of the string, or zero if the conversion fails.</returns>
    public static short ToShort(this string str)
    {
        _ = short.TryParse(str, out short result);
        return result;
    }

    /// <summary>
    /// Converts the input string to a 32-bit signed integer (int).
    /// </summary>
    /// <param name="str">The string to be converted.</param>
    /// <returns>A 32-bit signed integer (int) representation of the string, or zero if the conversion fails.</returns>
    public static int ToInt(this string str)
    {
        _ = int.TryParse(str, out int result);
        return result;
    }

    /// <summary>
    /// Converts the input string to a 64-bit signed integer (long).
    /// </summary>
    /// <param name="str">The string to be converted.</param>
    /// <returns>A 64-bit signed integer (long) representation of the string, or zero if the conversion fails.</returns>
    public static long ToLong(this string str)
    {
        _ = long.TryParse(str, out long result);
        return result;
    }

    /// <summary>
    /// Deserializes a JSON string into an object of the specified type using Newtonsoft.Json (JSON.NET).
    /// </summary>
    /// <typeparam name="T">The type of the object to deserialize the JSON into.</typeparam>
    /// <param name="jsonString">The JSON string to be deserialized.</param>
    /// <returns>The deserialized object of type <typeparamref name="T"/> if successful; otherwise, the default value for <typeparamref name="T"/>.</returns>
    public static T? DeserializeJson<T>(this string jsonString)
    {
        try
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }
        catch (JsonException ex)
        {
            // Handle deserialization error
            Console.WriteLine($"Error deserializing JSON: {ex.Message}");
            return default; // Return default value for T (null for reference types, default value for value types)
        }
    }

    /// <summary>
    /// Removes specified HTML tags from the input HTML string.
    /// </summary>
    /// <param name="html">Input HTML string.</param>
    /// <param name="tagNames">Names of the tags to be removed.</param>
    /// <returns>Modified HTML string without the specified tags.</returns>
    public static string RemoveHtmlTags(this string value, params string[] tagNames)
    {
        var htmlDocument = new HtmlDocument();
        htmlDocument.LoadHtml(value);

        foreach (var tagName in tagNames)
        {
            var tagsToRemove = htmlDocument.DocumentNode.SelectNodes($"//{tagName}");
            if (tagsToRemove != null)
            {
                foreach (var tagToRemove in tagsToRemove.ToList())
                {
                    tagToRemove.Remove();
                }
            }
        }

        return htmlDocument.DocumentNode.OuterHtml;
    }

    /// <summary>
    /// Converts a string representation to an enum of type T.
    /// </summary>
    /// <typeparam name="T">The enum type to convert to.</typeparam>
    /// <param name="value">The string representation of the enum value.</param>
    /// <param name="ignoreCase">True to perform a case-insensitive conversion; otherwise, false.</param>
    /// <returns>The enum value.</returns>
    /// <exception cref="ArgumentException">Thrown when the string value cannot be converted to the specified enum type.</exception>
    public static T ToEnum<T>(this string value, bool ignoreCase = true) where T : struct, Enum
    {
        if (!Enum.TryParse<T>(value, ignoreCase, out var enumValue))
        {
            // If the conversion fails, throw an ArgumentException with a meaningful message.
            throw new ArgumentException($"Unsupported enum value: {value}");
        }

        return enumValue;
    }

    /// <summary>
    /// Unescapes and decrypts the given string value using Uri.UnescapeDataString
    /// and the default decryption method Decrypt<T>(). Returns the decrypted value of type T.
    /// </summary>
    /// <typeparam name="T">The type of the decrypted value, must be a struct.</typeparam>
    /// <param name="value">The string value to unescape and decrypt.</param>
    /// <returns>The decrypted value of type T.</returns>
    public static T UnescapeAndDecrypt<T>(this string value) where T : struct
    {
        value = Uri.UnescapeDataString(value);
        return value.Decrypt<T>();
    }

    /// <summary>
    /// Unescapes and decrypts the given string value using Uri.UnescapeDataString
    /// and a specified password for decryption. Returns the decrypted value of type T.
    /// </summary>
    /// <typeparam name="T">The type of the decrypted value, must be a struct.</typeparam>
    /// <param name="value">The string value to unescape and decrypt.</param>
    /// <param name="password">The password used for decryption.</param>
    /// <returns>The decrypted value of type T.</returns>
    public static T UnescapeAndDecrypt<T>(this string value, string password) where T : struct
    {
        value = Uri.UnescapeDataString(value);
        return value.Decrypt<T>(password);
    }
}
