namespace OverwatchStadiumDatabase.Models;

public class Hero : Entity<int>
{
    public string Name { get; set; }
    
    // public ICollection<HeroExclusive> HeroExclusives { get; set; } = new List<HeroExclusive>();
    public ICollection<Item> Items { get; set; }
    
}