using System.Linq;
using DungeonRPG;
using Godot;

public partial class EnemyAttackState : EnemyState
{
    private Vector3 targetPosition;
    protected override void EnterState()
    {
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_ATTACK);
        characterNode.AnimPlayerNode.AnimationFinished += HandleAnimationFinished;

        Node3D target = characterNode.AttackAreaNode.GetOverlappingBodies().First();
        targetPosition = target.GlobalPosition;
    }

    protected override void ExitState()
    {
        characterNode.AnimPlayerNode.AnimationFinished -= HandleAnimationFinished;
    }

    private void HandleAnimationFinished(StringName animName)
    {
        characterNode.ToggleHitbox(false);
        characterNode.HitboxAreaNode.Position = Vector3.Zero;
    
        Node3D target = characterNode.AttackAreaNode.GetOverlappingBodies().FirstOrDefault();
        if (target == null)
        {
            Node3D chaseTarget = characterNode.ChaseAreaNode.GetOverlappingBodies().FirstOrDefault();
            if (chaseTarget == null)
            {
                characterNode.StateMachineNode.SwitchState<EnemyReturnState>();
                return;
            }
            characterNode.StateMachineNode.SwitchState<EnemyChaseState>();
            return;
        }

        // make sure we are facing the player before performing hit logic
        Vector3 directionToTarget = characterNode.GlobalPosition.DirectionTo(target.GlobalPosition);
        characterNode.SpriteNode.FlipH = directionToTarget.X < 0;

        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_ATTACK);
        targetPosition = target.GlobalPosition;
    }

    private void PerformHit()
    {
        GD.Print("Enemy performing hit logic");
        characterNode.ToggleHitbox(true);
        characterNode.HitboxAreaNode.GlobalPosition = targetPosition;
    }
}
