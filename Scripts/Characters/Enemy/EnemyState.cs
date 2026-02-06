using Godot;

namespace DungeonRPG
{
    public abstract partial class EnemyState : CharacterState
    {
        protected Vector3 destination;

        public override void _Ready()
        {
             base._Ready();
             characterNode.GetStatResource(Stat.Health).OnZero += HandleZeroHealth;
        }

        protected Vector3 GetPatrolPointGlobalPosition(int pointIndex)
        {
            Vector3 localPosition = characterNode.PathNode.Curve.GetPointPosition(pointIndex);
            Vector3 globalPosition = characterNode.PathNode.GlobalPosition;
            return globalPosition + localPosition;
        }

        protected void Move()
        {
            characterNode.AgentNode.GetNextPathPosition();
            characterNode.Velocity = characterNode.GlobalPosition.DirectionTo(destination);
            characterNode.MoveAndSlide();

            characterNode.Flip();
        }

        protected void HandleChaseAreaBodyEntered(Node3D body)
        {
            characterNode.StateMachineNode.SwitchState<EnemyChaseState>();
        }

        private void HandleZeroHealth()
        {
            characterNode.StateMachineNode.SwitchState<EnemyDeathState>();
        }
    }
}