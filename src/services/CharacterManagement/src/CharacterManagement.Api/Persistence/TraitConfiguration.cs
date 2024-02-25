using CharacterManagement.Api.Models.Races;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CharacterManagement.Api.Persistence;

public class TraitConfiguration : IEntityTypeConfiguration<Trait>
{
    public void Configure (EntityTypeBuilder<Trait> builder)
    {
        builder.HasKey (t => t.Id);
        builder.Property (t => t.Id).ValueGeneratedNever ();
        builder.HasMany (t => t.Effects)
               .WithOne ()
               .OnDelete (DeleteBehavior.Cascade);
    }
}
