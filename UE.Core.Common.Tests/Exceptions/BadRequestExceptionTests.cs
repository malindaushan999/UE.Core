using FluentValidation.Results;
using UE.Core.Common.Exceptions;

namespace UE.Core.Common.Tests.Exceptions;

/// <summary>
/// This class contains xUnit test methods to verify the behavior of the
/// custom exception class, BadRequestException, used for handling bad requests
/// with validation errors.
/// </summary>
public class BadRequestExceptionTests
{
    /// <summary>
    /// Verifies that a BadRequestException with a custom error message
    /// correctly stores the provided message and has an empty ValidationErrors list.
    /// </summary>
    [Fact]
    public void BadRequestException_WithMessage_ShouldHaveMessage()
    {
        // Arrange
        string errorMessage = "Custom error message";

        // Act
        var exception = new BadRequestException(errorMessage);

        // Assert
        Assert.Equal(errorMessage, exception.Message);
        Assert.Empty(exception.ValidationErrors);
    }

    /// <summary>
    /// Verifies that a BadRequestException with a custom error message
    /// and a ValidationResult object correctly stores the provided message
    /// and validation errors in the ValidationErrors list.
    /// </summary>
    [Fact]
    public void BadRequestException_WithValidationResult_ShouldHaveMessageAndValidationErrors()
    {
        // Arrange
        string errorMessage = "Custom error message";
        var validationResult = new ValidationResult();
        validationResult.Errors.Add(new ValidationFailure("Field", "Validation error message"));

        // Act
        var exception = new BadRequestException(errorMessage, validationResult);

        // Assert
        Assert.Equal(errorMessage, exception.Message);
        Assert.Single(exception.ValidationErrors);
        Assert.Equal("Validation error message", exception.ValidationErrors[0]);
    }
}