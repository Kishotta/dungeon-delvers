using CharacterManagement.Domain;
using CharacterManagement.Domain.Characters;
using CharacterManagement.Domain.Races;
using CharacterManagement.Domain.Sources;
using Microsoft.EntityFrameworkCore;

namespace CharacterManagement.Infrastructure;

public class CharacterManagementContext(DbContextOptions<CharacterManagementContext> options)
    : DbContext (options), IUnitOfWork
{
    public DbSet<Source>    Sources    => Set<Source> ();
    public DbSet<Race>      Races      => Set<Race> ();
    public DbSet<Character> Characters => Set<Character> ();
}
