using System;
using DungeonRPG;
using Godot;

public partial class PlayerAttackState : PlayerState
{
    [Export] private Timer comboResetTimerNode;
    private int comboCounter = 1;
    private int maxComboCount = 2;

    public override void _Ready()
    {
        base._Ready();
        comboResetTimerNode.Timeout += () => comboCounter = 1;
    }

    protected override void EnterState()
    {
        characterNode.AnimPlayerNode.Play(
            GameConstants.ANIM_ATTACK + comboCounter,
            -1,
            1.5f
        );

        characterNode.AnimPlayerNode.AnimationFinished += HandleAnimationFinished;
    }

    protected override void ExitState()
    {
        characterNode.AnimPlayerNode.AnimationFinished -= HandleAnimationFinished;
        comboResetTimerNode.Start();
    }

    private void HandleAnimationFinished(StringName animName)
    {
        characterNode.HitboxAreaNode.Position = Vector3.Zero;
        characterNode.ToggleHitbox(false);
        comboCounter = Mathf.Wrap(comboCounter + 1, 1, maxComboCount + 1);
        characterNode.StateMachineNode.SwitchState<PlayerIdleState>();
    }

    private void PerformHit()
    {
        GD.Print("Performing hit logic");
        float distanceMultiplier = 0.75f;
        Vector3 newPosition = characterNode.SpriteNode.FlipH
            ? Vector3.Left
            : Vector3.Right;

        characterNode.HitboxAreaNode.Position = newPosition * distanceMultiplier;
        characterNode.ToggleHitbox(true);
    }
}
