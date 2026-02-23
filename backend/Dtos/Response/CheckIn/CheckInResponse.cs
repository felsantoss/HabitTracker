namespace Dtos.Response.CheckIn;

public class CheckInResponse
{
    public int HabitId { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
    public List<CheckInItemResponse> Items { get; set; } = [];
}

public class CheckInItemResponse
{
    public DateOnly Date { get; set; }
    public DateTime CreatedAt { get; set; }
}