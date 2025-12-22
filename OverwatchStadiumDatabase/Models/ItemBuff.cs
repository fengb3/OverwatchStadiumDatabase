namespace OverwatchStadiumDatabase.Models;

public class ItemBuff : Entity<int>
{
    public int ItemId { get; set; }
    public Item Item { get; set; }

    public int BuffId { get; set; }
    public Buff Buff { get; set; }

    public decimal Value { get; set; }
}
