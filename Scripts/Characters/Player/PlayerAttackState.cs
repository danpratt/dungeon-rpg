using System;
using DungeonRPG;
using Godot;

public partial class PlayerAttackState : PlayerState
{
    [Export] private Timer comboResetTimerNode;
    [Export] private PackedScene lightningScene;
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
        characterNode.HitboxAreaNode.BodyEntered += HandleHitboxBodyEntered;
    }

    protected override void ExitState()
    {
        characterNode.AnimPlayerNode.AnimationFinished -= HandleAnimationFinished;
        characterNode.HitboxAreaNode.BodyEntered -= HandleHitboxBodyEntered;
        comboResetTimerNode.Start();
    }

    private void HandleAnimationFinished(StringName animName)
    {
        characterNode.HitboxAreaNode.Position = Vector3.Zero;
        characterNode.ToggleHitbox(false);
        comboCounter = Mathf.Wrap(comboCounter + 1, 1, maxComboCount + 1);
        characterNode.StateMachineNode.SwitchState<PlayerIdleState>();
    }


    private void HandleHitboxBodyEntered(Node3D body)
    {
        if (comboCounter != maxComboCount) { return; }
        Node3D lightningInstance = lightningScene.Instantiate<Node3D>();
        lightningInstance.GlobalPosition = body.GlobalPosition;
        GetTree().CurrentScene.AddChild(lightningInstance);
    }

    private void PerformHit()
    {
        float distanceMultiplier = 0.75f;
        Vector3 newPosition = characterNode.SpriteNode.FlipH
            ? Vector3.Left
            : Vector3.Right;

        characterNode.HitboxAreaNode.Position = newPosition * distanceMultiplier;
        characterNode.ToggleHitbox(true);
    }
}
