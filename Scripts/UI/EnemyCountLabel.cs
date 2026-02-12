using Godot;
using DungeonRPG;

public partial class EnemyCountLabel : Label
{
    public override void _Ready()
    {
        GameEvents.OnNewEnemyCount += HandleNewEnemyCount;
    }

    private void HandleNewEnemyCount(int count)
    {
        GD.Print($"Enemy Count Updated: {count}");
        Text = count.ToString();
    }
}
