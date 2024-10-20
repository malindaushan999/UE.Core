using FluentAssertions;
using UE.Core.Common.Builders;

namespace UE.Core.Common.Tests.Builders;

/// <summary>
/// Tests for the ConnectionStringBuilder class.
/// </summary>
public class ConnectionStringBuilderTests
{
    /// <summary>
    /// Ensures that Build method returns an empty string when no parameters are added.
    /// </summary>
    [Fact]
    public void Build_ReturnsEmptyString_WhenNoParametersAdded()
    {
        // Arrange
        var builder = new ConnectionStringBuilder();

        // Act
        var result = builder.Build();

        // Assert
        result.Should().BeEmpty();
    }

    /// <summary>
    /// Ensures that Build method returns the correct connection string when parameters are added.
    /// </summary>
    [Fact]
    public void Build_ReturnsCorrectConnectionString_WhenParametersAdded()
    {
        // Arrange
        var builder = new ConnectionStringBuilder();

        // Act
        var result = builder
            .AddParameter("Data Source", "localhost")
            .AddParameter("Initial Catalog", "SampleDB")
            .Build();

        // Assert
        result.Should().Be("Data Source=localhost;Initial Catalog=SampleDB;");
    }

    /// <summary>
    /// Ensures that Build method returns the correct connection string when TrustServerCertificate is added.
    /// </summary>
    [Fact]
    public void Build_ReturnsCorrectConnectionString_WhenTrustServerCertificateAdded()
    {
        // Arrange
        var builder = new ConnectionStringBuilder();

        // Act
        var result = builder
            .AddParameter("Data Source", "localhost")
            .AddTrustServerCertificate(true)
            .Build();

        // Assert
        result.Should().Be("Data Source=localhost;TrustServerCertificate=True;");
    }

    /// <summary>
    /// Ensures that Build method returns the correct connection string when Encrypt is added.
    /// </summary>
    [Fact]
    public void Build_ReturnsCorrectConnectionString_WhenEncryptAdded()
    {
        // Arrange
        var builder = new ConnectionStringBuilder();

        // Act
        var result = builder
            .AddParameter("Data Source", "localhost")
            .AddEncrypt(true)
            .Build();

        // Assert
        result.Should().Be("Data Source=localhost;Encrypt=True;");
    }

    /// <summary>
    /// Ensures that Build method returns the correct connection string when TrustServerCertificate and Encrypt are added.
    /// </summary>
    [Fact]
    public void Build_ReturnsCorrectConnectionString_WhenTrustServerCertificateAndEncryptAdded()
    {
        // Arrange
        var builder = new ConnectionStringBuilder();

        // Act
        var result = builder
            .AddParameter("Data Source", "localhost")
            .AddTrustServerCertificate(true)
            .AddEncrypt(true)
            .Build();

        // Assert
        result.Should().Be("Data Source=localhost;TrustServerCertificate=True;Encrypt=True;");
    }
}
