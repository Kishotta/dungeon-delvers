using CharacterManagement.Api.Models.Characters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CharacterManagement.Api.Models.Races.TraitEffects;

public class MovementSpeedEffect : Effect, IEntityTypeConfiguration<MovementSpeedEffect>
{
    public int Speed { get; set; } = 30;

    public override void Apply (MaterializedCharacter materializedCharacter)
    {
        materializedCharacter.MovementSpeed = Speed;
    }

    public void Configure (EntityTypeBuilder<MovementSpeedEffect> builder)
    {
        builder.ToTable("MovementSpeedEffects");
    }
}
