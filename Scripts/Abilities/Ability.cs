using Godot;

namespace DungeonRPG
{
    public abstract partial class Ability : Node3D
    {
        [Export] protected AnimationPlayer animationPlayer;
        [Export] public int damage { get; private set; } = 10;
    }
}