using DungeonDelvers.Modules.Monsters.Domain.ChallengeRatings;
using DungeonDelvers.Modules.Monsters.Domain.DiceExpressions;
using DungeonDelvers.Modules.Monsters.Domain.Monsters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DungeonDelvers.Modules.Monsters.Infrastructure.Monsters;

internal sealed class MonsterConfiguration : IEntityTypeConfiguration<Monster>
{
    public void Configure(EntityTypeBuilder<Monster> builder)
    {
        builder.HasKey(monster => monster.Id);
        
        builder.Property(monster => monster.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.OwnsOne<DiceExpression>(monster => monster.HitPoints);
        
        builder.OwnsOne<ChallengeRating>(
            monster => monster.ChallengeRating,
            challengeRating =>
            {
                challengeRating.Property(cr => cr.Value)
                    .HasColumnName("challenge_rating")
                    .IsRequired();
            });
    }
}