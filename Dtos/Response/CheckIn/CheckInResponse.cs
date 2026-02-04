namespace Dtos.Response.CheckIn;

public class CheckInResponse
{
    public int HabitId { get; set; }
    public List<DateOnly> CheckIns { get; set; }
}