using CharacterManagement.Api.Models.Races;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CharacterManagement.Api.Persistence;

public class RaceConfiguration : IEntityTypeConfiguration<Race>
{
    public void Configure (EntityTypeBuilder<Race> builder)
    {
        builder.HasKey (r => r.Id);
        builder.HasMany (r => r.Traits)
               .WithOne ()
               .OnDelete (DeleteBehavior.Cascade);
    }
}
