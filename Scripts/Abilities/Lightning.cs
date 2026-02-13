using Godot;
using DungeonRPG;

public partial class Lightning : Ability
{
        public override void _Ready()
        {
            animationPlayer.AnimationFinished += OnAnimationFinished;
        }
    
        private void OnAnimationFinished(StringName animName)
        {
            if (animName == GameConstants.ANIM_LIGHTNING)
            {
                QueueFree();
            }
        }
}
