using Models.User;

namespace Models.Habit;

public class HabitCheckIn
{
    public int Id { get; set; }
    public int HabitId { get; set; }
    public int UserId { get; set; }
    public DateOnly Date { get; set; }
    public DateTime CreatedAt { get; set; }
    public Habit Habit { get; set; }
    public User.User User { get; set; }
}