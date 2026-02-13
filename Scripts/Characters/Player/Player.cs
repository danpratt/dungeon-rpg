using DungeonRPG;
using Godot;
using System;

public partial class Player : Character
{
    public override void _Ready()
    {
        base._Ready();
        GameEvents.OnReward += HandleReceivedReward;
    }

    public override void _Input(InputEvent @event)
    {
        direction = Input.GetVector(
            GameConstants.INPUT_MOVE_LEFT,
            GameConstants.INPUT_MOVE_RIGHT,
            GameConstants.INPUT_MOVE_FORWARD,
            GameConstants.INPUT_MOVE_BACKWARD
        );
    }

       private void HandleReceivedReward(RewardResource resource)
    {
        GetStatResource(resource.TargetStat).StatValue += resource.Amount;
    }

}
