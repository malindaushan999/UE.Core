using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using FluentAssertions;
using UE.Core.Extensions.Tests.Mocks;

namespace UE.Core.Extensions.Tests;

/// <summary>
/// Contains unit tests for the <see cref="ObjectExtensions"/> class.
/// </summary>
public class ObjectExtensionsTests
{
    /// <summary>
    /// Tests the serialization of a <see cref="PersonMock"/> object and asserts that
    /// the resulting JSON string matches the expected JSON.
    /// </summary>
    [Fact]
    public void ToJson_SerializePersonMockObject_ReturnsValidJsonString()
    {
        // Arrange
        var person = new PersonMock
        {
            FirstName = "John",
            LastName = "Doe",
            Age = 30,
            // Add any other properties here as needed
        };

        // Expected JSON for relevant properties
        string expectedJson = JsonConvert.SerializeObject(person);

        // Act
        string json = person.ToJson();

        // Assert
        json.Should().BeEquivalentTo(expectedJson);
    }

    /// <summary>
    /// Tests the behavior when serializing a null object and asserts that the result is null.
    /// </summary>
    [Fact]
    public void ToJson_SerializeNullObject_ReturnsNull()
    {
        // Arrange
        PersonMock? person = null;

        // Act
        string json = person.ToJson();

        // Assert
        json.Should().BeNull();
    }
}
