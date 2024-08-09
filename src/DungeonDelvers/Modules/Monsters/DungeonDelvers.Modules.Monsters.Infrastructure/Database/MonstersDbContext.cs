using DungeonDelvers.Modules.Monsters.Application.Abstractions.Data;
using DungeonDelvers.Modules.Monsters.Domain.Monsters;
using Microsoft.EntityFrameworkCore;

namespace DungeonDelvers.Modules.Monsters.Infrastructure.Database;

public class MonstersDbContext(DbContextOptions<MonstersDbContext> options) 
    : DbContext(options), IUnitOfWork
{
    internal DbSet<Monster> Monsters => Set<Monster>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(MonstersModule.Schema);
        
        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(Common.Infrastructure.AssemblyReference.Assembly);
        
        base.OnModelCreating(modelBuilder);
    }
}