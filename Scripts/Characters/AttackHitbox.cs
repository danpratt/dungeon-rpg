using DungeonRPG;
using Godot;

public partial class AttackHitbox : Area3D, IHitBox
{
    public bool CanStun()
    {
        return false;
    }

    public int GetDamage()
    {
        // For now, return a fixed damage value. You can expand this to include different damage types or scaling.
        return GetOwner<Character>().GetStatResource(Stat.Strength).StatValue;
    }
}
