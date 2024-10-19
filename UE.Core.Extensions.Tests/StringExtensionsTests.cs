
using FluentAssertions;
using UE.Core.Extensions.Tests.Mocks;

namespace UE.Core.Extensions.Tests;
/// <summary>
/// Contains unit tests for the extension methods defined in the StringExtensions class.
/// </summary>
public class StringExtensionsTests
{
    private const string TestString = "Hello, World!";
    private const string Password = "MySecretPassword";

    #region Encryption/Decryption

    /// <summary>
    /// Verifies that the Encrypt extension method successfully encrypts a string using the default password.
    /// </summary>
    [Fact]
    public void Encrypt_DefaultPassword_Success()
    {
        // Act
        var encryptedString = TestString.Encrypt();

        // Assert
        Assert.NotNull(encryptedString);
        Assert.NotEqual(TestString, encryptedString);
    }

    /// <summary>
    /// Verifies that the Encrypt extension method successfully encrypts a string using a custom password.
    /// </summary>
    [Fact]
    public void Encrypt_CustomPassword_Success()
    {
        // Act
        var encryptedString = TestString.Encrypt(Password);

        // Assert
        Assert.NotNull(encryptedString);
        Assert.NotEqual(TestString, encryptedString);
    }

    /// <summary>
    /// Verifies that the Decrypt extension method successfully decrypts an encrypted string using the default password.
    /// </summary>
    [Fact]
    public void Decrypt_DefaultPassword_Success()
    {
        // Arrange
        var encryptedString = TestString.Encrypt();

        // Act
        var decryptedString = encryptedString.Decrypt();

        // Assert
        Assert.NotNull(decryptedString);
        Assert.Equal(TestString, decryptedString);
    }

    /// <summary>
    /// Verifies that the Decrypt extension method successfully decrypts an encrypted string using a custom password.
    /// </summary>
    [Fact]
    public void Decrypt_CustomPassword_Success()
    {
        // Arrange
        var encryptedString = TestString.Encrypt(Password);

        // Act
        var decryptedString = encryptedString.Decrypt(Password);

        // Assert
        Assert.NotNull(decryptedString);
        Assert.Equal(TestString, decryptedString);
    }

    #endregion Encryption/Decryption

    #region Conversions

    /// <summary>
    /// Verifies that the ToShort extension method correctly converts valid input strings to short integers
    /// and returns the expected short integer value, or returns 0 for invalid or null input strings.
    /// </summary>
    [Theory]
    [InlineData("1234", 1234)]       // Valid positive input
    [InlineData("-4321", -4321)]     // Valid negative input
    [InlineData("0", 0)]             // Valid zero input
    [InlineData("abc", 0)]           // Invalid input should return 0
    [InlineData(null, 0)]            // Null input should return 0
    public void ToShort_ValidInput_ReturnsExpectedValue(string? input, short expected)
    {
        // Act: Call the ToShort extension method with the input
#pragma warning disable CS8604 // Possible null reference argument.
        var result = input.ToShort();
#pragma warning restore CS8604 // Possible null reference argument.

        // Assert: Verify that the actual result matches the expected result
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Verifies that the ToInt extension method correctly converts valid input strings to integers
    /// and returns the expected integer value, or returns 0 for invalid or null input strings.
    /// </summary>
    [Theory]
    [InlineData("12345", 12345)]     // Valid positive input
    [InlineData("-54321", -54321)]   // Valid negative input
    [InlineData("0", 0)]             // Valid zero input
    [InlineData("abc", 0)]           // Invalid input should return 0
    [InlineData(null, 0)]            // Null input should return 0
    public void ToInt_ValidInput_ReturnsExpectedValue(string? input, int expected)
    {
        // Act: Call the ToInt extension method with the input
#pragma warning disable CS8604 // Possible null reference argument.
        var result = input.ToInt();
#pragma warning restore CS8604 // Possible null reference argument.

        // Assert: Verify that the actual result matches the expected result
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Verifies that the ToLong extension method correctly converts valid input strings to long integers
    /// and returns the expected long integer value, or returns 0 for invalid or null input strings.
    /// </summary>
    [Theory]
    [InlineData("12345", 12345)]     // Valid positive input
    [InlineData("-54321", -54321)]   // Valid negative input
    [InlineData("0", 0)]             // Valid zero input
    [InlineData("abc", 0)]           // Invalid input should return 0
    [InlineData(null, 0)]            // Null input should return 0
    public void ToLong_ValidInput_ReturnsExpectedValue(string? input, long expected)
    {
        // Act: Call the ToLong extension method with the input
#pragma warning disable CS8604 // Possible null reference argument.
        var result = input.ToLong();
#pragma warning restore CS8604 // Possible null reference argument.

        // Assert: Verify that the actual result matches the expected result
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests the Decrypt method for an integer value.
    /// </summary>
    [Fact]
    public void Decrypt_IntValue_ShouldReturnDecryptedInt()
    {
        // Arrange
        string encryptedValue = 42.Encrypt();

        // Act
        int decryptedValue = encryptedValue.Decrypt<int>();

        // Assert
        decryptedValue.Should().Be(42, "because the encrypted value represents the integer 42");
    }

    /// <summary>
    /// Tests the Decrypt method for a long value.
    /// </summary>
    [Fact]
    public void Decrypt_LongValue_ShouldReturnDecryptedLong()
    {
        // Arrange
        string encryptedValue = 9876543210.Encrypt();

        // Act
        long decryptedValue = encryptedValue.Decrypt<long>();

        // Assert
        decryptedValue.Should().Be(9876543210, "because the encrypted value represents the long 9876543210");
    }

    /// <summary>
    /// Tests the Decrypt method for a short value.
    /// </summary>
    [Fact]
    public void Decrypt_ShortValue_ShouldReturnDecryptedShort()
    {
        // Arrange
        string encryptedValue = 99.Encrypt();

        // Act
        short decryptedValue = encryptedValue.Decrypt<short>();

        // Assert
        decryptedValue.Should().Be(99, "because the encrypted value represents the short 99");
    }

    #endregion Conversions

    #region JSON

    /// <summary>
    /// Verifies that the DeserializeJson extension method correctly deserializes valid JSON
    /// and returns the expected object.
    /// </summary>
    [Fact]
    public void DeserializeJson_ValidJson_ShouldDeserialize()
    {
        // Arrange
        string jsonString = "{\"Name\":\"John\",\"Age\":30}";

        // Act
        var person = jsonString.DeserializeJson<PersonMock>();

        // Assert
        Assert.NotNull(person); // Ensure the deserialization result is not null
        Assert.Equal("John", person.Name); // Check that the 'Name' property is as expected
        Assert.Equal(30, person.Age); // Check that the 'Age' property is as expected
    }

    /// <summary>
    /// Verifies that the DeserializeJson extension method gracefully handles invalid JSON
    /// and returns the default value for the specified object type.
    /// </summary>
    [Fact]
    public void DeserializeJson_InvalidJson_ShouldReturnDefault()
    {
        // Arrange
        string jsonString = "Invalid JSON";

        // Act
        var result = jsonString.DeserializeJson<PersonMock>();

        // Assert
        Assert.Null(result); // Ensure that the method returns the default value (null for reference types)
    }

    #endregion JSON

    #region HTML Stripping

    /// <summary>
    /// Test to ensure that the RemoveHtmlTags method correctly removes the specified HTML tags.
    /// </summary>
    [Fact]
    public void RemoveHtmlTags_RemovesSpecifiedTags()
    {
        // Arrange
        string html = "<div><p>Some text</p><span>More text</span></div>";
        string[] tagsToRemove = { "p", "span" };

        // Act
        string result = html.RemoveHtmlTags(tagsToRemove);

        // Assert
        result.Should().NotContain("<p>").And.NotContain("<span>").And.NotContain("</p>").And.NotContain("</span>");
    }

    /// <summary>
    /// Test to ensure that the RemoveHtmlTags method handles the case when the input HTML string is empty.
    /// </summary>
    [Fact]
    public void RemoveHtmlTags_EmptyHtmlString_ReturnsEmptyString()
    {
        // Arrange
        string emptyHtml = string.Empty;
        string[] tagsToRemove = { "p", "span" };

        // Act
        string result = emptyHtml.RemoveHtmlTags(tagsToRemove);

        // Assert
        result.Should().BeEmpty();
    }

    /// <summary>
    /// Test to ensure that the RemoveHtmlTags method handles the case when the specified tags are not present in the HTML string.
    /// </summary>
    [Fact]
    public void RemoveHtmlTags_TagsNotPresent_ReturnsOriginalHtml()
    {
        // Arrange
        string html = "<div><p>Some text</p><span>More text</span></div>";
        string[] tagsToRemove = { "a", "b" };

        // Act
        string result = html.RemoveHtmlTags(tagsToRemove);

        // Assert
        result.Should().Be(html);
    }

    /// <summary>
    /// Test to ensure that the RemoveHtmlTags method handles the case when multiple instances of the same tag are present in the HTML string.
    /// </summary>
    [Fact]
    public void RemoveHtmlTags_MultipleInstancesOfSameTag_RemovesAllInstances()
    {
        // Arrange
        string html = "<div><p>First paragraph</p><p>Second paragraph</p></div>";
        string[] tagsToRemove = { "p" };

        // Act
        string result = html.RemoveHtmlTags(tagsToRemove);

        // Assert
        result.Should().NotContain("<p>").And.NotContain("</p>");
    }

    /// <summary>
    /// Test to ensure that the RemoveHtmlTags method handles the case when the input HTML string contains self-closing tags.
    /// </summary>
    [Fact]
    public void RemoveHtmlTags_SelfClosingTags_RemovesSelfClosingTags()
    {
        // Arrange
        string html = "<div><img src='image.jpg' /><br /></div>";
        string[] tagsToRemove = { "img", "br" };

        // Act
        string result = html.RemoveHtmlTags(tagsToRemove);

        // Assert
        result.Should().NotContain("<img").And.NotContain("<br").And.NotContain("/>");
    }

    #endregion HTML Stripping

    #region Enum

    /// <summary>
    /// Tests the <see cref="StringExtensions.ToEnum{T}"/> method with a valid enum value.
    /// </summary>
    [Fact]
    public void ToEnum_ValidEnumValue_ShouldReturnEnumValue()
    {
        // Arrange
        string validEnumValue = "Value2"; // Replace with an actual valid enum value
        MockEnum expectedEnum = MockEnum.Value2; // Replace with the expected enum value

        // Act
        MockEnum result = validEnumValue.ToEnum<MockEnum>();

        // Assert
        result.Should().Be(expectedEnum);
    }

    /// <summary>
    /// Tests the <see cref="StringExtensions.ToEnum{T}"/> method with an invalid enum value.
    /// </summary>
    [Fact]
    public void ToEnum_InvalidEnumValue_ShouldThrowArgumentException()
    {
        // Arrange
        string invalidEnumValue = "InvalidValue"; // Replace with an actual invalid enum value

        // Act and Assert
        Action action = () => invalidEnumValue.ToEnum<MockEnum>();

        action.Should().Throw<ArgumentException>()
            .WithMessage($"Unsupported enum value: {invalidEnumValue}");
    }

    /// <summary>
    /// Tests the <see cref="StringExtensions.ToEnum{T}"/> method with a case-sensitive conversion.
    /// </summary>
    [Fact]
    public void ToEnum_CaseSensitiveConversion_ShouldReturnEnumValue()
    {
        // Arrange
        string validEnumValue = "Value3"; // Replace with an actual valid enum value
        MockEnum expectedEnum = MockEnum.Value3; // Replace with the expected enum value

        // Act
        MockEnum result = validEnumValue.ToEnum<MockEnum>(ignoreCase: false);

        // Assert
        result.Should().Be(expectedEnum);
    }

    #endregion Enum

}