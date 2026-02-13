using System;
using System.Linq;
using Godot;

namespace DungeonRPG
{
    public abstract partial class Character : CharacterBody3D
    {
        [Export] private StatResource[] Stats;
        [ExportGroup("Required Nodes")]
        [Export] public AnimationPlayer AnimPlayerNode { get; private set; }
        [Export] public Sprite3D SpriteNode { get; private set; }
        [Export] public StateMachine StateMachineNode { get; private set; }
        [Export] public Area3D HitboxAreaNode { get; private set; }
        [Export] public Area3D HurtboxAreaNode { get; private set; }
        [Export] public CollisionShape3D HitboxShapeNode { get; private set; }

        [ExportGroup("AI Nodes")]
        [Export] public Path3D PathNode { get; private set; }
        [Export] public NavigationAgent3D AgentNode { get; private set; }
        [Export] public Area3D ChaseAreaNode { get; private set; }

        [Export] public Area3D AttackAreaNode { get; private set; }

        public Vector2 direction = new();
        private ShaderMaterial shader;

        public override void _Ready()
        {
            base._Ready();
            shader = (ShaderMaterial)SpriteNode.MaterialOverlay;
            HurtboxAreaNode.AreaEntered += HandleHurtboxAreaEntered;
            SpriteNode.TextureChanged += () => HandleTextureChanged();
        }

        private void HandleTextureChanged()
        {
            shader.SetShaderParameter(
                "tex",
                SpriteNode.Texture
            );
        }

        private void HandleHurtboxAreaEntered(Area3D area)
        {
            if (area is not IHitBox hitbox) { return; }
            StatResource health = GetStatResource(Stat.Health);
            int damage = hitbox.GetDamage();
            area.SetDeferred("monitoring", false);
            health.StatValue -= damage;

            shader.SetShaderParameter("active", true);
            Timer timer = new Timer();
            timer.WaitTime = 0.2f;
            timer.OneShot = true;
            timer.Timeout += () => {
                shader.SetShaderParameter("active", false);
                timer.QueueFree();
            };
            AddChild(timer);
            timer.Start();
        }

        public StatResource GetStatResource(Stat stat)
        {
            StatResource statResource = Stats.Where((element) => element.StatType == stat).FirstOrDefault();

            if (statResource == null)
            {
                GD.PrintErr($"No StatResource of type {stat} found on {Name}");
            }

            return statResource;
        }

        public void Flip()
        {
            if (Velocity.X == 0) return;

            bool isMovingLeft = Velocity.X < 0;
            SpriteNode.FlipH = isMovingLeft;
        }

        public void ToggleHitbox(bool isActive)
        {
            HitboxShapeNode.Disabled = !isActive;
            HitboxAreaNode.Monitoring = isActive;
        }
    }
}