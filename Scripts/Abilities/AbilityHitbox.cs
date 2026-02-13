using Godot;
using DungeonRPG;

public partial class AbilityHitbox : Area3D, IHitBox
{
    public bool CanStun()
    {
        return true;
    }

    // return damage value for the bomb explosion
    public int GetDamage()
    {
        return GetOwner<Ability>().damage;
    }
}
