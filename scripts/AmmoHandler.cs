using System.Collections.Generic;
using Godot;

namespace RoboRampage.scripts
{
    public partial class AmmoHandler : Node
    {
        public enum AmmoType
        {
            BULLET,
            SMALL_BULLET
        }

        public Dictionary<AmmoType, int> AmmoStorage = new()
        {
            {AmmoType.BULLET, 10},
            {AmmoType.SMALL_BULLET, 60},
        };

        public bool HasAmmo(AmmoType type)
        {
            return AmmoStorage[type] > 0;
        }

        public void UseAmmo(AmmoType type)
        {
            if (HasAmmo(type))
            {
                AmmoStorage[type] -= 1;
                GD.Print(AmmoStorage[type]);
            }
        }
    }
}