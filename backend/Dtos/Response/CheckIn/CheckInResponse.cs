using System.Diagnostics.CodeAnalysis;

namespace Dtos.Response.CheckIn;

[ExcludeFromCodeCoverage]
public class CheckInResponse
{
    public int HabitId { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
    public List<CheckInItemResponse> Items { get; set; } = [];
}

[ExcludeFromCodeCoverage]
public class CheckInItemResponse
{
    public DateOnly Date { get; set; }
    public DateTime CreatedAt { get; set; }
}
