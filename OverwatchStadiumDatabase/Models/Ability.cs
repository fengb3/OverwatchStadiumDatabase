namespace OverwatchStadiumDatabase.Models;

public class Ability : Entity<int>
{
    public string Name { get; set; } = string.Empty;
    
    // navigation properties
    
    /// <summary>
    /// 所属英雄 
    /// </summary>
    public Hero Hero { get; set; } = null!;
    
    /// <summary>
    /// 该能力可被增强的Buff列表
    /// </summary>
    public ICollection<Buff> BonusBy { get; set; } = new List<Buff>();
}