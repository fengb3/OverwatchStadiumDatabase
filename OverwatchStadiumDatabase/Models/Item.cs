namespace OverwatchStadiumDatabase.Models;

public class Item
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required Uri ImageUri { get; set; }
    public required string Type { get; set; }
    public decimal Cost { get; set; }
    public required string Rarity { get; set; }
    public required string Description { get; set; }

    public ICollection<ItemBuff> ItemBuffs { get; set; } = new List<ItemBuff>();
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
