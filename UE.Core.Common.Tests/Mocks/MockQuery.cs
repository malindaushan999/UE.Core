#nullable disable

using UE.Core.Common.Interfaces;

namespace UE.Core.Common.Tests.Mocks;

public class MockQuery : IPagination
{
    public string SearchText { get; set; }
    public int PageSize { get; set; }
    public int PageNo { get; set; }
}
