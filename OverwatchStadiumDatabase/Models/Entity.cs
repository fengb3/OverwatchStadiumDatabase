namespace OverwatchStadiumDatabase.Models;

public class Entity<T> where T : IEquatable<T>
{
    public T Id { get; set; } = default!;
}