namespace OverwatchStadiumDatabase.Models;

public class HeroExclusive : Entity<int>
{
    public int HeroId { get; set; }
    public int ItemId { get; set; }
    
    public Hero Hero { get; set; } = default!;
    public Item Item { get; set; } = default!;
}