using System.Security.Cryptography;

namespace UE.Core.Security;

/// <summary>
/// Provides encryption and decryption functionality using a specified password.
/// </summary>
public class Cipher
{
    #region Fields
    private readonly int iterations = 10000;            // Number of iterations for PBKDF2
    private readonly int saltSize = 16;                 // Salt size in bytes
    private readonly int keySize = 32;                  // Derived key size in bytes
    private readonly string _password = string.Empty;   // Password string
    #endregion Fields

    #region Constructors
    /// <summary>
    /// Initializes a new instance of the Cipher class with default settings.
    /// </summary>
    public Cipher()
    {
        _password = "p@55w0rd@SMACipher";
    }

    /// <summary>
    /// Initializes a new instance of the Cipher class with a specified password.
    /// </summary>
    /// <param name="password">The password used for encryption and decryption.</param>
    public Cipher(string password)
    {
        _password = password;
    }
    #endregion Constructors

    #region Public
    /// <summary>
    /// Encrypts the input text using the default password.
    /// </summary>
    /// <param name="plainText">The text to be encrypted.</param>
    /// <returns>The encrypted representation of the input text as a string.</returns>
    public string Encrypt(string plainText)
    {
        return Encrypt(plainText, _password);
    }

    /// <summary>
    /// Encrypts the input text using the provided password.
    /// </summary>
    /// <param name="plainText">The text to be encrypted.</param>
    /// <param name="password">The password used for encryption.</param>
    /// <returns>The encrypted representation of the input text as a string.</returns>
    public string Encrypt(string plainText, string password)
    {
        if (string.IsNullOrEmpty(password))
        {
            throw new ArgumentException("Password cannot be empty.");
        }

        using Aes aesAlg = Aes.Create();
        byte[] salt = GenerateRandomSalt();
        byte[] key = GenerateKey(password, salt);

        aesAlg.Key = key;
        aesAlg.IV = GenerateRandomIV();

        ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

        using MemoryStream msEncrypt = new();
        using (CryptoStream csEncrypt = new(msEncrypt, encryptor, CryptoStreamMode.Write))
        {
            using StreamWriter swEncrypt = new(csEncrypt);
            swEncrypt.Write(plainText);
        }
        return Convert.ToBase64String(salt.Concat(aesAlg.IV).Concat(msEncrypt.ToArray()).ToArray());
    }

    /// <summary>
    /// Decrypts the input encrypted text using the default password.
    /// </summary>
    /// <param name="cipherText">The encrypted text to be decrypted.</param>
    /// <returns>The decrypted representation of the input encrypted text as a string.</returns>
    public string Decrypt(string cipherText)
    {
        return Decrypt(cipherText, _password);
    }

    /// <summary>
    /// Decrypts the input encrypted text using the provided password.
    /// </summary>
    /// <param name="cipherText">The encrypted text to be decrypted.</param>
    /// <param name="password">The password used for decryption.</param>
    /// <returns>The decrypted representation of the input encrypted text as a string.</returns>
    public string Decrypt(string cipherText, string password)
    {
        if (string.IsNullOrEmpty(password))
        {
            throw new ArgumentException("Password cannot be empty.");
        }

        using Aes aesAlg = Aes.Create();
        byte[] encryptedData = Convert.FromBase64String(cipherText);
        byte[] salt = encryptedData.Take(saltSize).ToArray();
        byte[] key = GenerateKey(password, salt);

        aesAlg.Key = key;
        aesAlg.IV = encryptedData.Skip(saltSize).Take(aesAlg.BlockSize / 8).ToArray();

        ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

        using MemoryStream msDecrypt = new(encryptedData.Skip(saltSize + aesAlg.BlockSize / 8).ToArray());
        using CryptoStream csDecrypt = new(msDecrypt, decryptor, CryptoStreamMode.Read);
        using StreamReader srDecrypt = new(csDecrypt);
        return srDecrypt.ReadToEnd();
    }
    #endregion Public

    #region Private
    /// <summary>
    /// Generates a random salt for cryptographic operations.
    /// </summary>
    /// <returns>An array of bytes representing the random salt.</returns>
    private byte[] GenerateRandomSalt()
    {
        byte[] salt = new byte[saltSize];
        using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }
        return salt;
    }

    /// <summary>
    /// Generates a random Initialization Vector (IV) for cryptographic operations.
    /// </summary>
    /// <returns>An array of bytes representing the random IV.</returns>
    private byte[] GenerateRandomIV()
    {
        byte[] iv = new byte[keySize / 2];
        using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(iv);
        }
        return iv;
    }

    /// <summary>
    /// Generates a cryptographic key based on the provided password and salt.
    /// </summary>
    /// <param name="password">The password used for key generation.</param>
    /// <param name="salt">The salt used for key derivation.</param>
    /// <returns>An array of bytes representing the generated key.</returns>
    private byte[] GenerateKey(string password, byte[] salt)
    {
        using Rfc2898DeriveBytes pbkdf2 = new(password, salt, iterations, HashAlgorithmName.SHA256);
        return pbkdf2.GetBytes(keySize);
    }
    #endregion Private
}
