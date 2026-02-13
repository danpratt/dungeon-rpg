using DungeonRPG;
using Godot;

public partial class PlayerDashState : PlayerState
{
    [Export] private Timer dashTimerNode;
    [Export] private Timer dashCooldownTimerNode;
    [Export(PropertyHint.Range, "0, 20, 0.1")] private float speed = 10f;

    [Export] private PackedScene bombScene;
    public override void _Ready()
    {
        base._Ready();
        dashTimerNode.Timeout += HandleDashTimerTimeout;
        CanTransition = () => dashCooldownTimerNode.IsStopped();
    }

    public override void _PhysicsProcess(double delta)
    {
        characterNode.MoveAndSlide();
        characterNode.Flip();
    }


    protected override void EnterState()
    {
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_DASH);
        characterNode.Velocity = new(
            characterNode.direction.X,
            0,
            characterNode.direction.Y);

        // if the player is in an idle state, dash in the direction they are facing
        if (characterNode.Velocity == Vector3.Zero)
        {
            characterNode.Velocity = characterNode.SpriteNode.FlipH ?
                new Vector3(-1, 0, 0) :
                new Vector3(1, 0, 0);
        }

        characterNode.Velocity *= speed;

        dashTimerNode.Start();

        // Spawn a bomb at the player's position
        var bombInstance = bombScene.Instantiate<Bomb>();
        bombInstance.GlobalPosition = characterNode.GlobalPosition;
        GetTree().CurrentScene.AddChild(bombInstance);
    }

    private void HandleDashTimerTimeout()
    {
        dashCooldownTimerNode.Start();
        characterNode.StateMachineNode.SwitchState<PlayerIdleState>();
        characterNode.Velocity = Vector3.Zero;
    }
}
