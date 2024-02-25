using CharacterManagement.Api.Models;
using CharacterManagement.Api.Models.Characters;
using CharacterManagement.Api.Models.Races;
using CharacterManagement.Api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CharacterManagement.Api.Persistence;

public class CharacterManagementContext(DbContextOptions<CharacterManagementContext> options)
    : DbContext (options), IUnitOfWork
{
    public DbSet<Source>    Sources    => Set<Source> ();
    public DbSet<Race>      Races      => Set<Race> ();
    public DbSet<Character> Characters => Set<Character> ();

    protected override void OnModelCreating (ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly (typeof (CharacterManagementContext).Assembly);
        modelBuilder.Entity<Effect> ().UseTptMappingStrategy ();

        base.OnModelCreating (modelBuilder);
    }
}
