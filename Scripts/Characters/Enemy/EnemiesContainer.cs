using DungeonRPG;
using Godot;
using System;

public partial class EnemiesContainer : Node3D
{
    public override void _Ready()
    {
        UpdateEnemyCount(false);
        ChildExitingTree += HandleChildExitingTree;
    }

    private void HandleChildExitingTree(Node node)
    {
        UpdateEnemyCount(true);
    }

    private void UpdateEnemyCount(bool isEnemyDefeated = true)
    {
        int enemyCount = GetChildCount();
        if (isEnemyDefeated)
        {
            enemyCount = Math.Max(0, enemyCount - 1);
        }
        GameEvents.RaiseNewEnemyCount(enemyCount);

        if (enemyCount == 0)
        {
            GameEvents.RaiseVictory();
        }
    }
}
