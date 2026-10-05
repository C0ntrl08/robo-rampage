using System.Linq;
using Godot;

namespace RoboRampage.scripts
{

    public partial class WeaponHandler : Node3D
    {
        [Export]
        public Node3D Weapon1{get; set;}
        [Export]
        public Node3D Weapon2{get; set;}

        private const string WEAPON1 = "weapon_1";
        private const string WEAPON2 = "weapon_2";
        private const string NEXTWEAPON = "next_weapon";
        private const string LASTWEAPON = "last_weapon";

        public override void _Ready()
        {
            Equip(Weapon1);
        }

        public override void _UnhandledInput(InputEvent inputEvent)
        {
            if (inputEvent.IsActionPressed(WEAPON1))
            {
                Equip(Weapon1);
            }
            if (inputEvent.IsActionPressed(WEAPON2))
            {
                Equip(Weapon2);
            }
            if (inputEvent.IsActionPressed(NEXTWEAPON))
            {
                NextWeapon();
            }
            if (inputEvent.IsActionPressed(LASTWEAPON))
            {
                LastWeapon();
            }
        }


        public void Equip(Node3D activeWeapon)
        {
            foreach (var child in GetChildren())
            {
                if (child == activeWeapon)
                {
                    ((Node3D)child).Visible = true;
                    child.SetProcess(true);
                    var weapon = (HitscanWeapon)child;
                    weapon.AmmoHandler.UpdateAmmoLabel(weapon.AmmoType);
                }
                else
                {
                    ((Node3D)child).Visible = false;
                    child.SetProcess(false);
                }
            }
        }

        public void NextWeapon()
        {
            int index = GetCurrentIndex();
            index = Mathf.Wrap(index + 1, 0, GetChildCount());
            Equip((Node3D)GetChild(index));
        }

        public void LastWeapon()
        {
            int index = GetCurrentIndex();
            index = Mathf.Wrap(index - 1, 0, GetChildCount());
            Equip((Node3D)GetChild(index));
        }

        private int GetCurrentIndex()
        {
            for (int index = 0; index < GetChildCount(); index++)
            {
                if (((Node3D)GetChild(index)).Visible)
                {
                    return index;
                }
            }

            return 0;
        }
    }
}