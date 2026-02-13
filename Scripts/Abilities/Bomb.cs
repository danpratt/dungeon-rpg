using DungeonRPG;
using Godot;

public partial class Bomb : Ability
{
    public override void _Ready()
    {
        animationPlayer.AnimationFinished += OnAnimationFinished;
    }

    private void OnAnimationFinished(StringName animName)
    {
        if (animName == GameConstants.ANIM_EXPAND)
        {
            animationPlayer.Play(GameConstants.ANIM_EXPLODE);
        }
        else if (animName == GameConstants.ANIM_EXPLODE)
        {
            QueueFree();
        }
    }
}
