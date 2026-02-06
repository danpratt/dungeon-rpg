using DungeonRPG;
using Godot;

public partial class EnemyReturnState : EnemyState
{
        public override void _PhysicsProcess(double delta)
    {
        if (characterNode.AgentNode.IsNavigationFinished())
        {
            GD.Print("Enemy reached destination.");
            characterNode.StateMachineNode.SwitchState<EnemyPatrolState>();
            return;
        }

        Move();
    }

    public override void _Ready()
    {
        base._Ready();
        destination = GetPatrolPointGlobalPosition(0);
        characterNode.AgentNode.TargetPosition = destination;
    }
    protected override void EnterState()
    {
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_MOVE);
        characterNode.AgentNode.TargetPosition = destination;
        characterNode.ChaseAreaNode.BodyEntered += HandleChaseAreaBodyEntered;
    }

    protected override void ExitState()
    {
        characterNode.ChaseAreaNode.BodyEntered -= HandleChaseAreaBodyEntered;
    }
}
