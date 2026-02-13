using Godot;
using DungeonRPG;

public partial class Enemy : Character
{
    public override void _Ready()
    {
        base._Ready();
        
        HurtboxAreaNode.AreaEntered += HandleHurtboxAreaEntered;
}

    private void HandleHurtboxAreaEntered(Area3D area)
    {
        if (area is not IHitBox hitbox) { return; }

        if (hitbox.CanStun() && GetStatResource(Stat.Health).StatValue > hitbox.GetDamage())
        {
            StateMachineNode.SwitchState<EnemyStunState>();
        }
    }
}