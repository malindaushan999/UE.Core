namespace UE.Core.Extensions.Tests.Mocks;

/// <summary>
/// A mock class representing a person for deserialization tests.
/// </summary>
public class PersonMock
{
    /// <summary>
    /// Gets or sets the name of the person.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the first name of the person.
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name of the person.
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// Gets or sets the age of the person.
    /// </summary>
    public int Age { get; set; }

    /// <summary>
    /// Gets or sets the password of the person (nullable).
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// Gets or sets the age of the person. Refer <see cref="AddressMock"/> class.
    /// </summary>
    public AddressMock? Address { get; set; }
}
