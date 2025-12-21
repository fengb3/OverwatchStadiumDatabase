namespace OverwatchStadiumDatabase.Models;

public class Item
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Uri ImageUri { get; set; }
    public string Type { get; set; }
    public decimal Cost { get; set; }
    public string Rarity { get; set; }
    public string Description { get; set; }
    
    public ICollection<Buff> Buffs { get; set; }
}

/*
| ability_name = Aftermarket Firing Pin
    | hero_name = All heroes
    | ability_image = Aftermarket_Firing_Pin.png
    | ability_type = General Item (Weapon)
    | stadium_buffs = Attack Speed;;10%::Move Speed;;5%
| official_description =
| stadium_rarity = Rare
| stadium_cost = 3750
*/