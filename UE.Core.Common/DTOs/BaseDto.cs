using System.Text.Json.Serialization;

namespace UE.Core.Common.DTOs;

public class BaseDto
{
    [JsonIgnore]
    public int Id { get; set; }
    public string? EncryptedKey { get; set; }

    [JsonIgnore]
    public bool IsDeleted { get; set; } = false;
}
