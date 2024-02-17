using CharacterManagement.Domain;
using CharacterManagement.Domain.Characters;
using Microsoft.EntityFrameworkCore;

namespace CharacterManagement.Infrastructure;

public class CharacterManagementContext(DbContextOptions<CharacterManagementContext> options)
    : DbContext (options), IUnitOfWork
{
    public DbSet<Character> Characters => Set<Character>();
}
