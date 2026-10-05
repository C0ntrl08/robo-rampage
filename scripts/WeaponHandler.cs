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
        }


        public void Equip(Node3D activeWeapon)
        {
            foreach (Node3D child in GetChildren().Cast<Node3D>())
            {
                if (child == activeWeapon)
                {
                    child.Visible = true;
                    child.SetProcess(true);
                }
                else
                {
                    child.Visible = false;
                    child.SetProcess(false);
                }
            }
        }
    }
}