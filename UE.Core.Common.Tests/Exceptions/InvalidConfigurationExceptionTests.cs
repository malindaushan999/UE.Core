using FluentAssertions;
using UE.Core.Common.Exceptions;

namespace UE.Core.Common.Tests.Exceptions;

public class InvalidConfigurationExceptionTests
{
    /// <summary>
    /// Verifies that the constructor of InvalidConfigurationException sets the correct exception message.
    /// </summary>
    [Fact]
    public void Constructor_ShouldSetCorrectExceptionMessage()
    {
        // Arrange
        string key = "testKey";

        // Act
        var exception = new InvalidConfigurationException(key);

        // Assert
        exception.Message.Should().Be($"Configurations are invalid for the key: {key}");
    }

    /// <summary>
    /// Verifies that two instances of InvalidConfigurationException with the same key are considered equal.
    /// </summary>
    [Fact]
    public void InstancesWithSameKey_ShouldBeEqual()
    {
        // Arrange
        string key = "testKey";
        var exception1 = new InvalidConfigurationException(key);
        var exception2 = new InvalidConfigurationException(key);

        // Act & Assert
        exception1.Should().BeEquivalentTo(exception2);
    }

    /// <summary>
    /// Verifies that two instances of InvalidConfigurationException with different keys are not considered equal.
    /// </summary>
    [Fact]
    public void InstancesWithDifferentKeys_ShouldNotBeEqual()
    {
        // Arrange
        string key1 = "testKey1";
        string key2 = "testKey2";
        var exception1 = new InvalidConfigurationException(key1);
        var exception2 = new InvalidConfigurationException(key2);

        // Act & Assert
        exception1.Should().NotBeEquivalentTo(exception2);
    }
}
