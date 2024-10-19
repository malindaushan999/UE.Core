namespace UE.Core.Extensions.Tests;

public class LongExtensionsTests
{
    /// <summary>
    /// Verifies that the Encrypt extension method correctly encrypts an long with the default password.
    /// </summary>
    [Fact]
    public void Encrypt_IntWithDefaultPassword_ReturnsEncryptedString()
    {
        // Arrange
        long value = 12323554769;

        // Act
        string encrypted = value.Encrypt();

        // Assert
        Assert.NotNull(encrypted);
        Assert.NotEmpty(encrypted);

        // Additional comments:
        // - This test case checks if the Encrypt method for longs with the default password
        //   returns a non-null and non-empty encrypted string.
        // - You may want to add more specific assertions based on the encryption logic used in
        //   the Cipher class.
    }

    /// <summary>
    /// Verifies that the Encrypt extension method correctly encrypts a long with a provided password.
    /// </summary>
    [Fact]
    public void Encrypt_ShortWithPassword_ReturnsEncryptedString()
    {
        // Arrange
        long value = 45676950556;
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
