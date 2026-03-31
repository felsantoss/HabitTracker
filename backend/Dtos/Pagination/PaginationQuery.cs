using System.Diagnostics.CodeAnalysis;

namespace Dtos.Pagination;

[ExcludeFromCodeCoverage]
public class PaginationQuery
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int Skip => (PageNumber - 1) * PageSize;
}
