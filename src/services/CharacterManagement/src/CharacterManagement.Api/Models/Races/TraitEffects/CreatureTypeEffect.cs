using CharacterManagement.Api.Models.Characters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CharacterManagement.Api.Models.Races.TraitEffects;

public class CreatureTypeEffect : Effect, IEntityTypeConfiguration<CreatureTypeEffect>
{
    public CreatureType CreatureType { get; set; } = CreatureType.Humanoid;

    public override void Apply (MaterializedCharacter materializedCharacter)
    {
        materializedCharacter.CreatureType = Enum.GetName (typeof(CreatureType), CreatureType) ?? "Unknown";
    }

    public void Configure (EntityTypeBuilder<CreatureTypeEffect> builder)
    {
        builder.ToTable ("CreatureTypeEffects");
    }
}
