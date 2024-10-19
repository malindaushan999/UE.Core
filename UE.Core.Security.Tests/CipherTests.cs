namespace UE.Core.Security.Tests;

public class CipherTests
{
    // Test case: Encrypt and decrypt using the default password.
    [Fact]
    public void EncryptDecrypt_WithDefaultPassword_ShouldProduceOriginalText()
    {
        // Arrange
        var cipher = new Cipher();
        var originalText = "Hello, world!";

        // Act
        var encryptedText = cipher.Encrypt(originalText);
        var decryptedText = cipher.Decrypt(encryptedText);

        // Assert
        Assert.Equal(originalText, decryptedText);
    }

    // Test case: Encrypt and decrypt using a custom password.
    [Fact]
    public void EncryptDecrypt_WithCustomPassword_ShouldProduceOriginalText()
    {
        // Arrange
        var customPassword = "CustomP@ssw0rd";
        var cipher = new Cipher(customPassword);
        var originalText = "Testing custom password encryption.";

        // Act
        var encryptedText = cipher.Encrypt(originalText);
        var decryptedText = cipher.Decrypt(encryptedText, customPassword);

        // Assert
        Assert.Equal(originalText, decryptedText);
    }

    // Test case: Attempt to decrypt with an invalid password, should throw an exception.
    [Fact]
    public void Decrypt_WithInvalidPassword_ShouldThrowException()
    {
        // Arrange
        var validPassword = "ValidP@ssw0rd";
        var invalidPassword = "InvalidP@ssw0rd";
        var cipher = new Cipher();
        var originalText = "Secret data";

        // Encrypt with a valid password
        var encryptedText = cipher.Encrypt(originalText, validPassword);

        // Act and Assert
        Assert.Throws<System.Security.Cryptography.CryptographicException>(() => cipher.Decrypt(encryptedText, invalidPassword));
    }

    // Test case: Attempt to encrypt with an empty password, should throw an exception.
    [Fact]
    public void Encrypt_WithEmptyPassword_ShouldThrowException()
    {
        // Arrange
        var emptyPassword = string.Empty;
        var cipher = new Cipher();
        var originalText = "Sensitive info";

        // Act and Assert
        Assert.Throws<ArgumentException>(() => cipher.Encrypt(originalText, emptyPassword));
    }
}
