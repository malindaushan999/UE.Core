#nullable disable

using UE.Core.Common.Builders;
using UE.Core.Common.Enums;
using UE.Core.Extensions;

namespace UE.Core.Common.Settings;

public class DatabaseSettings
{
    public string Provider { get; set; }
    public string Server { get; set; }
    public string DatabaseName { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public bool TrustServerCertificate { get; set; }
    public bool Encrypt { get; set; }
    public int ConnectionTimeout { get; set; }

    public string BuildConnectionString(string encryptionPassword)
    {
        var connectionStringBuilder = new ConnectionStringBuilder();

        // Convert the string value from configuration to DatabaseProvider enum
        if (!Enum.TryParse<DatabaseProvider>(Provider, true, out var providerEnum))
        {
            throw new ArgumentException($"Unsupported database provider: {Provider}");
        }

        switch (providerEnum)
        {
            case DatabaseProvider.SqlServer:
                connectionStringBuilder
                    .AddParameter("Server", Server)
                    .AddParameter("Database", DatabaseName);
                break;
            case DatabaseProvider.MySql:
                connectionStringBuilder
                    .AddParameter("Server", Server)
                    .AddParameter("Database", DatabaseName);
                break;
            case DatabaseProvider.PostgreSql:
                connectionStringBuilder
                    .AddParameter("Host", Server)
                    .AddParameter("Database", DatabaseName);
                break;
            // Add more providers as needed
            default:
                throw new ArgumentException($"Unsupported database provider: {Provider}");
        }

        if (!string.IsNullOrEmpty(Username))
        {
            connectionStringBuilder.AddParameter("User Id", Username);
        }

        if (!string.IsNullOrEmpty(Password))
        {
            connectionStringBuilder.AddParameter("Password", !string.IsNullOrEmpty(encryptionPassword) ? Password.Decrypt(encryptionPassword) : Password.Decrypt());
        }

        connectionStringBuilder
            .AddTrustServerCertificate(TrustServerCertificate)
            .AddEncrypt(Encrypt)
            .AddParameter("Connection Timeout", ConnectionTimeout.ToString());

        return connectionStringBuilder.Build();
    }
}
