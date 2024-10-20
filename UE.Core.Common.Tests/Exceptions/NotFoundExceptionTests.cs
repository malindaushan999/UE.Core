using UE.Core.Common.Exceptions;

namespace UE.Core.Common.Tests.Exceptions;

/// <summary>
/// This class contains xUnit test methods to verify the behavior of the
/// custom exception class, `NotFoundException`, used for handling "Not Found" errors
/// when an entity or resource is not found in the system.
/// </summary>
public class NotFoundExceptionTests
{
    /// <summary>
    /// Verifies that the `NotFoundException` constructor correctly sets the message
    /// to include the name and key of the missing entity or resource.
    /// </summary>
    [Fact]
    public void NotFoundException_Constructor_SetsErrorMessage()
    {
        // Arrange
        string name = "Product";
        object key = 42;

        // Act
        var exception = new NotFoundException(name, key);

        // Assert
        Assert.Equal($"{name} ({key}) was not found", exception.Message);
    }
}
