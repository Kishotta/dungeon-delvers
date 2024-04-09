namespace DungeonDelvers.Domain.Effects;

public class WeaponTypeProficiency(WeaponType weaponType) : Effect
{
    public WeaponType WeaponType { get; private set; } = weaponType;
    
    public override MaterializedCharacter Apply(MaterializedCharacter materializedCharacter)
    {
        materializedCharacter.WeaponTypeProficiencies |= WeaponType;
        return materializedCharacter;
    }
}