namespace UE.Core.Common.Interfaces;

/// <summary>
/// Represents the pagination information for a data query.
/// </summary>
public interface IPagination
{
    /// <summary>
    /// Gets or sets the search text.
    /// </summary>
    string SearchText { get; set; }

    /// <summary>
    /// Gets or sets the number of items per page.
    /// </summary>
    int PageSize { get; set; }

    /// <summary>
    /// Gets or sets the page number.
    /// </summary>
    int PageNo { get; set; }
}
