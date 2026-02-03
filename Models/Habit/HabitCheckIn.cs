namespace Models.Habit;

public class HabitCheckIn
{
    public int Id { get; set; }
    public int HabitId { get; set; }
    public int UserId { get; set; }
    public DateOnly Date { get; set; }
    public DateTime CreatAt { get; set; }
}