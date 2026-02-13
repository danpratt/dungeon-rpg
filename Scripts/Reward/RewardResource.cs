using Godot;

namespace DungeonRPG
{
    [GlobalClass]
    public partial class RewardResource : Resource
    {
        [Export] public Texture2D SpriteTexture { get; private set; }
        [Export] public string Description { get; private set; }
        [Export] public Stat TargetStat { get; private set; }
        [Export(PropertyHint.Range, "1,100,1")] public int Amount { get; private set; }
    }
}