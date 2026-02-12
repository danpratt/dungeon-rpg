using Godot;
using System;
using System.Linq;

namespace DungeonRPG
{
    [GlobalClass]
    public partial class StatResource : Resource
    {
        public event Action OnZero;
        public event Action OnUpdate;
        [Export] public Stat StatType {get; private set; }
        private int _statValue;
        [Export] public int StatValue
        {
            get => _statValue;
            set
            {
                _statValue = Mathf.Clamp(value, 0, int.MaxValue);
                OnUpdate?.Invoke();
                if (_statValue == 0)
                {
                    OnZero?.Invoke();
                }
            }
        }
    }
}