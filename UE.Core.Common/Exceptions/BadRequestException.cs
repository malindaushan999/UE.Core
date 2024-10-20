using FluentValidation.Results;

namespace UE.Core.Common.Exceptions;

/// <summary>
/// Custom exception class for handling bad requests with validation errors.
/// </summary>
public class BadRequestException : Exception
{
    /// <summary>
    /// Gets or sets the list of validation error messages associated with the bad request.
    /// </summary>
    public List<string> ValidationErrors { get; set; } = new();

    /// <summary>
    /// Constructor for creating a BadRequestException with a custom message.
    /// </summary>
    /// <param name="message">A custom error message.</param>
    public BadRequestException(string message) : base(message)
    {
        // No specific logic here, as it simply calls the base constructor with the provided message.
    }

    /// <summary>
    /// Constructor for creating a BadRequestException with a custom message
    /// and a ValidationResult object, which contains validation errors.
    /// </summary>
    /// <param name="message">A custom error message.</param>
    /// <param name="validationResult">An object containing validation errors.</param>
    public BadRequestException(string message, ValidationResult validationResult) : base(message)
    {
        /// <summary>
        /// Iterate through each validation error in the ValidationResult object
        /// and add the error messages to the ValidationErrors list.
        /// </summary>
        foreach (var error in validationResult.Errors)
        {
            ValidationErrors.Add(error.ErrorMessage);
        }
    }
}

