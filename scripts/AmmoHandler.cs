using System.Collections.Generic;
using Godot;

namespace RoboRampage.scripts
{
    public partial class AmmoHandler : Node
    {
        [Export]
        public Label AmmoLabel{get;set;}
        [Export]
        public Node3D WeaponHandler{get;set;}

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
                UpdateAmmoLabel(((WeaponHandler)WeaponHandler).GetWeaponAmmo());
            }
        }

        public void AddAmmo(AmmoType type, int amount)
        {
            AmmoStorage[type] += amount;
            UpdateAmmoLabel(((WeaponHandler)WeaponHandler).GetWeaponAmmo());
        }

        public void UpdateAmmoLabel(AmmoType type)
        {
            AmmoLabel.Text = AmmoStorage[type].ToString();
        }
    }
}