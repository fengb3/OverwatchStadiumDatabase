namespace OverwatchStadiumDatabase.Models;

public class Buff : Entity<int>
{
    public string BuffName { get; set; }
    public decimal Value { get; set; }
}

// public class BuffType : Entity<string>
// {
//     // public string Name { get; set; }
// }