using DungeonRPG;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class UIController : Control
{
    private Dictionary<ContainerType, UIContainer> containers;

    private bool canPause = false;

    public override void _Ready()
    {
        containers = GetChildren().Where(
            (element) => element is UIContainer
        ).Cast<UIContainer>().ToDictionary(
            (element) => element.container
        );

        containers[ContainerType.Start].Visible = true;
        containers[ContainerType.Start].ButtonNode.Pressed += () => HandleStartButtonPressed();
        containers[ContainerType.Pause].ButtonNode.Pressed += () => HandlePauseButtonPressed();
        GameEvents.OnEndGame += HandleEndGame;
        GameEvents.OnVictory += HandleVictory;
    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        if (!canPause || !Input.IsActionJustPressed(GameConstants.INPUT_PAUSE))
        {
            return;
        }
        else
        {
            containers[ContainerType.Stats].Visible = GetTree().Paused = GetTree().Paused;
            GetTree().Paused = !GetTree().Paused;
            containers[ContainerType.Pause].Visible = GetTree().Paused;
        }
    }

    private void HandleStartButtonPressed()
    {
        canPause = true;
        GetTree().Paused = false;
        containers[ContainerType.Start].Visible = false;
        containers[ContainerType.Stats].Visible = true;
        GameEvents.RaiseStartGame();
    }

    private void HandleEndGame()
    {
        canPause = false;
        GetTree().Paused = true;
        containers[ContainerType.Stats].Visible = false;
        containers[ContainerType.Defeat].Visible = true;
    }

      private void HandleVictory()
    {
        canPause = false;
        GetTree().Paused = true;
        containers[ContainerType.Stats].Visible = false;
        containers[ContainerType.Victory].Visible = true;
    }

        private void HandlePauseButtonPressed()
    {
        GetTree().Paused = false;
        containers[ContainerType.Pause].Visible = false;
        containers[ContainerType.Stats].Visible = true;
    }
}
