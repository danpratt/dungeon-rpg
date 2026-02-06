using System;
using DungeonRPG;
using Godot;

public partial class EnemyPatrolState : EnemyState
{
    [Export] private Timer idleTimerNode;
    [Export(PropertyHint.Range, "0.0, 20, 0.1")] private float maxIdleTime = 2.0f;
    private int currentPatrolPointIndex = 0;

        public override void _PhysicsProcess(double delta)
    {
        if (!idleTimerNode.IsStopped())
        {
            return;
        }
        Move();
    }
    protected override void EnterState()
    {
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_MOVE);
        currentPatrolPointIndex = 1;

        destination = GetPatrolPointGlobalPosition(currentPatrolPointIndex);
        characterNode.AgentNode.TargetPosition = destination;

        characterNode.AgentNode.NavigationFinished += HandleNavigationFinished;
        idleTimerNode.Timeout += HandleTimerTimeout;
        characterNode.ChaseAreaNode.BodyEntered += HandleChaseAreaBodyEntered;
    }

    protected override void ExitState()
    {
        characterNode.AgentNode.NavigationFinished -= HandleNavigationFinished;
        idleTimerNode.Timeout -= HandleTimerTimeout;
        characterNode.ChaseAreaNode.BodyEntered -= HandleChaseAreaBodyEntered;
    }

    private void HandleNavigationFinished()
    {
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_IDLE);
        RandomNumberGenerator rng = new RandomNumberGenerator();
        idleTimerNode.WaitTime = rng.RandfRange(0.0f, maxIdleTime);
        idleTimerNode.Start();
    }

    private void HandleTimerTimeout()
    {
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_MOVE);
        currentPatrolPointIndex = Mathf.Wrap(currentPatrolPointIndex + 1, 0, characterNode.PathNode.Curve.GetPointCount());
        destination = GetPatrolPointGlobalPosition(currentPatrolPointIndex);
        characterNode.AgentNode.TargetPosition = destination;
    }
}
