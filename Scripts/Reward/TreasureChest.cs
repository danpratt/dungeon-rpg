using DungeonRPG;
using Godot;

public partial class TreasureChest : StaticBody3D
{
    [Export] private Area3D areaNode;
    [Export] private Sprite3D spriteNode;
    [Export] private RewardResource reward;

    public override void _Ready()
    {
        areaNode.BodyEntered += OnBodyEntered;
        areaNode.BodyExited += OnBodyExited;
    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        if (spriteNode.Visible && @event.IsActionPressed(GameConstants.INPUT_INTERACT))
        {
            areaNode.Monitoring = false;
            GetNode<Sprite3D>("Sprite3D").RegionRect = new Rect2(32.0f, 8.0f, 16.0f, 24.0f);
            GameEvents.RaiseReward(reward);
        }
    }

    private void OnBodyEntered(Node3D body)
    {
        spriteNode.Visible = true;
    }

       private void OnBodyExited(Node3D body)
    {
        spriteNode.Visible = false;
    }
}
