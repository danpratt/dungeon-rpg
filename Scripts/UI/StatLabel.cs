using System;
using DungeonRPG;
using Godot;

public partial class StatLabel : Label
{

    [Export] private StatResource statResource;
    public override void _Ready()
    {
        statResource.OnUpdate += HandleUpdate;
        HandleUpdate();
    }

    private void HandleUpdate()
    {
        Text = statResource.StatValue.ToString();
    }
}
