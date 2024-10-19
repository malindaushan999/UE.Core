using UE.Core.Security;

namespace UE.Core.Extensions;

public static class LongExtensions
{
    /// <summary>
    /// Encrypts an long value.
    /// </summary>
    /// <param name="value">The long value to be encrypted.</param>
    /// <returns>The encrypted representation of the long value as a string.</returns>
    public static string Encrypt(this long value)
    {
        Cipher cipher = new();
        return cipher.Encrypt(value.ToString());
    }

    /// <summary>
    /// Encrypts a short long value using the provided password.
    /// </summary>
    /// <param name="value">The long value to be encrypted.</param>
    /// <param name="password">The password used for encryption.</param>
    /// <returns>The encrypted representation of the short long value as a string.</returns>
    public static string Encrypt(this long value, string password)
    {
        Cipher cipher = new(password);
        return cipher.Encrypt(value.ToString());
    }
}
