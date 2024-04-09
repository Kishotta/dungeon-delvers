namespace DungeonDelvers.Domain.Effects;

public class SkillProficiency(Skill skill) : Effect
{
    public Skill Skill { get; private set; } = skill;

    public override MaterializedCharacter Apply(MaterializedCharacter materializedCharacter)
    {
        materializedCharacter.SkillProficiencies |= Skill;
        return materializedCharacter;
    }
}