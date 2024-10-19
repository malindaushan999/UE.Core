using UE.Core.Security;

namespace UE.Core.Extensions;

/// <summary>
/// Provides a set of extension methods for integer manipulation and conversion.
/// </summary>
public static class IntExtensions
{
    /// <summary>
    /// Encrypts an integer value.
    /// </summary>
    /// <param name="value">The integer value to be encrypted.</param>
    /// <returns>The encrypted representation of the integer value as a string.</returns>
    public static string Encrypt(this int value)
    {
        Cipher cipher = new();
        return cipher.Encrypt(value.ToString());
    }

    /// <summary>
    /// Encrypts a short integer value using the provided password.
    /// </summary>
    /// <param name="value">The integer value to be encrypted.</param>
    /// <param name="password">The password used for encryption.</param>
    /// <returns>The encrypted representation of the short integer value as a string.</returns>
    public static string Encrypt(this int value, string password)
    {
        Cipher cipher = new(password);
        return cipher.Encrypt(value.ToString());
    }
}
