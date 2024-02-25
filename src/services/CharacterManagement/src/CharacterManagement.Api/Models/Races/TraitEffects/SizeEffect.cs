using CharacterManagement.Api.Models.Characters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CharacterManagement.Api.Models.Races.TraitEffects;

public class SizeEffect : Effect, IEntityTypeConfiguration<SizeEffect>
{
    public Size Size { get; set; } = Size.Medium;

    public override void Apply (MaterializedCharacter materializedCharacter)
    {
        materializedCharacter.Size = Enum.GetName (typeof(Size), Size) ?? "Unknown";
    }

    public void Configure (EntityTypeBuilder<SizeEffect> builder)
    {
        builder.ToTable ("SizeEffects");
    }
}
