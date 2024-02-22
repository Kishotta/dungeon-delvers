using CharacterManagement.Api.Models;
using CharacterManagement.Api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CharacterManagement.Api;

public class CharacterManagementContext(DbContextOptions<CharacterManagementContext> options)
    : DbContext (options), IUnitOfWork
{
    public DbSet<Source>    Sources    => Set<Source> ();
    public DbSet<Race>      Races      => Set<Race> ();
    public DbSet<Character> Characters => Set<Character> ();
}
