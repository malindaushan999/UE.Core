#nullable disable

using UE.Core.Common.Entities;

namespace UE.Core.Common.Tests.Mocks;

public class MockEntity : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
}
