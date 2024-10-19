using UE.Core.Security;

namespace UE.Core.Extensions;

public static class ShortExtensions
{
    /// <summary>
    /// Encrypts an short value.
    /// </summary>
    /// <param name="value">The short value to be encrypted.</param>
    /// <returns>The encrypted representation of the short value as a string.</returns>
    public static string Encrypt(this short value)
    {
        Cipher cipher = new();
        return cipher.Encrypt(value.ToString());
    }

    /// <summary>
    /// Encrypts a short short value using the provided password.
    /// </summary>
    /// <param name="value">The short value to be encrypted.</param>
    /// <param name="password">The password used for encryption.</param>
    /// <returns>The encrypted representation of the short short value as a string.</returns>
    public static string Encrypt(this short value, string password)
    {
        Cipher cipher = new(password);
        return cipher.Encrypt(value.ToString());
    }
}
