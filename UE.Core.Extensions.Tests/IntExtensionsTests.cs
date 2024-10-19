namespace UE.Core.Extensions.Tests;

/// <summary>
/// Contains unit tests for the extension methods defined in the IntExtensions class.
/// </summary>
public class IntExtensionsTests
{
    /// <summary>
    /// Verifies that the Encrypt extension method correctly encrypts an integer with the default password.
    /// </summary>
    [Fact]
    public void Encrypt_IntWithDefaultPassword_ReturnsEncryptedString()
    {
        // Arrange
        int value = 123;

        // Act
        string encrypted = value.Encrypt();

        // Assert
        Assert.NotNull(encrypted);
        Assert.NotEmpty(encrypted);

        // Additional comments:
        // - This test case checks if the Encrypt method for integers with the default password
        //   returns a non-null and non-empty encrypted string.
        // - You may want to add more specific assertions based on the encryption logic used in
        //   the Cipher class.
    }

    /// <summary>
    /// Verifies that the Encrypt extension method correctly encrypts a short integer with a provided password.
    /// </summary>
    [Fact]
    public void Encrypt_ShortWithPassword_ReturnsEncryptedString()
    {
        // Arrange
        int value = 456;
        string password = "MySecretPassword";

        // Act
        string encrypted = value.Encrypt(password);

        // Assert
        Assert.NotNull(encrypted);
        Assert.NotEmpty(encrypted);

        // Additional comments:
        // - This test case checks if the Encrypt method for shorts with a specified password
        //   returns a non-null and non-empty encrypted string.
        // - You may want to add more specific assertions based on the encryption logic used in
        //   the Cipher class.
    }
}
