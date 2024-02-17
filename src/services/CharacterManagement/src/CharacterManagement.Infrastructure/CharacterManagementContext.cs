using CharacterManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace CharacterManagement.Infrastructure;

public class CharacterManagementContext(DbContextOptions<CharacterManagementContext> options) : DbContext (options)
{
    public DbSet<Character> Characters => Set<Character>();
}
