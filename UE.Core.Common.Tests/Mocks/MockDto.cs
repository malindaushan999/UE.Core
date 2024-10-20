#nullable disable

using UE.Core.Common.DTOs;

namespace UE.Core.Common.Tests.Mocks;

public class MockDto : BaseDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
}
