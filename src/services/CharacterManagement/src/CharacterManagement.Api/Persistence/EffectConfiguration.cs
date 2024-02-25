using CharacterManagement.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CharacterManagement.Api.Persistence;

public class EffectConfiguration : IEntityTypeConfiguration<Effect>
{
    public void Configure (EntityTypeBuilder<Effect> builder)
    {
        builder.HasKey (e => e.Id);
        builder.Property (e => e.Id).ValueGeneratedNever ();
    }
}
