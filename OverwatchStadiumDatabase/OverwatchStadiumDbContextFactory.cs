using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace OverwatchStadiumDatabase;

public class OverwatchStadiumDesignTimeDbContextFactory
    : IDesignTimeDbContextFactory<OverwatchStadiumDbContext>
{
    public OverwatchStadiumDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<OverwatchStadiumDbContext>();
        optionsBuilder.ConfigureOverwatchStadiumDatabase();
        return new OverwatchStadiumDbContext(optionsBuilder.Options);
    }
}
