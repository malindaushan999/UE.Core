#nullable disable

using UE.Core.Extensions;

namespace UE.Core.Common.Settings;

public class ApplicationSettings
{
    private string encryptionPassword;

    public string EncryptionPassword
    {
        get => encryptionPassword.Decrypt();
        set => encryptionPassword = value;
    }
}
