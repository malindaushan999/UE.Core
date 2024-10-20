#nullable enable

using FluentAssertions;
using UE.Core.Common.Settings;
using UE.Core.Extensions;

namespace UE.Core.Common.Tests.Settings;

public class DatabaseSettingsTests
{
    /// <summary>
    /// Verifies that BuildConnectionString method generates correct connection string for SqlServer provider.
    /// </summary>
    [Fact]
    public void BuildConnectionString_ForSqlServer_ShouldGenerateCorrectConnectionString()
    {
        // Arrange
        var databaseSettings = new DatabaseSettings
        {
            Provider = "SqlServer",
            Server = "localhost",
            DatabaseName = "TestDb",
            Username = "user",
            Password = "password".Encrypt("cipherPassword"),
            TrustServerCertificate = true,
            Encrypt = true,
            ConnectionTimeout = 30
        };

        // Act
        var connectionString = databaseSettings.BuildConnectionString("cipherPassword");

        // Assert
        connectionString.Should().Contain("Server=localhost");
        connectionString.Should().Contain("Database=TestDb");
        connectionString.Should().Contain("User Id=user");
        connectionString.Should().Contain("Password=password");
        connectionString.Should().Contain("TrustServerCertificate=True");
        connectionString.Should().Contain("Encrypt=True");
        connectionString.Should().Contain("Connection Timeout=30");
    }

    /// <summary>
    /// Verifies that BuildConnectionString method generates correct connection string for MySql provider.
    /// </summary>
    [Fact]
    public void BuildConnectionString_ForMySql_ShouldGenerateCorrectConnectionString()
    {
        // Arrange
        var databaseSettings = new DatabaseSettings
        {
            Provider = "MySql",
            Server = "localhost",
            DatabaseName = "TestDb",
            Username = "user",
            Password = "password".Encrypt("cipherPassword"),
            TrustServerCertificate = true,
            Encrypt = true,
            ConnectionTimeout = 30
        };

        // Act
        var connectionString = databaseSettings.BuildConnectionString("cipherPassword");

        // Assert
        connectionString.Should().Contain("Server=localhost");
        connectionString.Should().Contain("Database=TestDb");
        connectionString.Should().Contain("User Id=user");
        connectionString.Should().Contain("Password=password");
        connectionString.Should().Contain("TrustServerCertificate=True");
        connectionString.Should().Contain("Encrypt=True");
        connectionString.Should().Contain("Connection Timeout=30");
    }

    /// <summary>
    /// Verifies that BuildConnectionString method generates correct connection string for PostgreSql provider.
    /// </summary>
    [Fact]
    public void BuildConnectionString_ForPostgreSql_ShouldGenerateCorrectConnectionString()
    {
        // Arrange
        var databaseSettings = new DatabaseSettings
        {
            Provider = "PostgreSql",
            Server = "localhost",
            DatabaseName = "TestDb",
            Username = "user",
            Password = "password".Encrypt("cipherPassword"),
            TrustServerCertificate = true,
            Encrypt = true,
            ConnectionTimeout = 30
        };

        // Act
        var connectionString = databaseSettings.BuildConnectionString("cipherPassword");

        // Assert
        connectionString.Should().Contain("Host=localhost");
        connectionString.Should().Contain("Database=TestDb");
        connectionString.Should().Contain("User Id=user");
        connectionString.Should().Contain("Password=password");
        connectionString.Should().Contain("TrustServerCertificate=True");
        connectionString.Should().Contain("Encrypt=True");
        connectionString.Should().Contain("Connection Timeout=30");
    }

    /// <summary>
    /// Verifies that BuildConnectionString method throws ArgumentException for unsupported database provider.
    /// </summary>
    [Fact]
    public void BuildConnectionString_WithUnsupportedProvider_ShouldThrowArgumentException()
    {
        // Arrange
        var databaseSettings = new DatabaseSettings
        {
            Provider = "UnsupportedProvider",
            Server = "localhost",
            DatabaseName = "TestDb",
            ConnectionTimeout = 30
        };

        // Act & Assert
        databaseSettings.Invoking(d => d.BuildConnectionString("cipherPassword"))
            .Should().Throw<ArgumentException>().WithMessage("Unsupported database provider: UnsupportedProvider");
    }
}
