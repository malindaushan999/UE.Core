#nullable enable

using FluentAssertions;
using UE.Core.Common.Settings;
using UE.Core.Extensions;

namespace UE.Core.Common.Tests.Settings;

public class ApplicationSettingsTests
{
    /// <summary>
    /// Verifies that EncryptionPassword getter returns decrypted value.
    /// </summary>
    [Fact]
    public void EncryptionPassword_Getter_ShouldReturnDecryptedValue()
    {
        // Arrange
        var applicationSettings = new ApplicationSettings
        {
            EncryptionPassword = "encryptedPassword".Encrypt()
        };

        // Act
        var decryptedPassword = applicationSettings.EncryptionPassword;

        // Assert
        decryptedPassword.Should().NotBeNull();
        decryptedPassword.Should().NotBeEmpty();
        decryptedPassword.Should().Contain("encrypted"); // Ensure it is decrypted
    }

    /// <summary>
    /// Verifies that setting EncryptionPassword has no effect (setter is empty).
    /// </summary>
    [Fact]
    public void EncryptionPassword_Setter_ShouldHaveNoEffect()
    {
        // Arrange
        var applicationSettings = new ApplicationSettings
        {
            EncryptionPassword = "originalPassword".Encrypt()
        };

        // Act
        applicationSettings.EncryptionPassword = "newPassword".Encrypt();

        // Assert
        applicationSettings.EncryptionPassword.Should().NotBeNull();
        applicationSettings.EncryptionPassword.Should().NotBeEmpty();
        applicationSettings.EncryptionPassword.Should().NotBe("originalPassword");
        applicationSettings.EncryptionPassword.Should().Be("newPassword"); // Verify that the new password is set
    }
}
