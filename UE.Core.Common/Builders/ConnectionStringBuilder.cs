using System.Text;

namespace UE.Core.Common.Builders;

public class ConnectionStringBuilder
{
    private StringBuilder _connectionStringBuilder;

    public ConnectionStringBuilder()
    {
        _connectionStringBuilder = new StringBuilder();
    }

    public ConnectionStringBuilder AddParameter(string parameterName, string value)
    {
        _connectionStringBuilder.Append($"{parameterName}={value};");
        return this;
    }

    public ConnectionStringBuilder AddTrustServerCertificate(bool trustServerCertificate)
    {
        if (trustServerCertificate)
        {
            _connectionStringBuilder.Append("TrustServerCertificate=True;");
        }
        return this;
    }

    public ConnectionStringBuilder AddEncrypt(bool encrypt)
    {
        if (encrypt)
        {
            _connectionStringBuilder.Append("Encrypt=True;");
        }
        return this;
    }

    public string Build()
    {
        return _connectionStringBuilder.ToString();
    }
}
