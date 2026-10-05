using Godot;

namespace RoboRampage.scripts
{
    public partial class Pickup : Area3D
    {
        [Export]
        public AmmoHandler.AmmoType AmmoType {get;set;}
        [Export]
        public int Amount {get;set;} = 20;

        public void OnBodyEntered(Node3D body)
        {
            if (body.IsInGroup("Player") && body is Player player)
            {
                player.AmmoHandler.AddAmmo(AmmoType, Amount);
                QueueFree();
            }
        }
    }
}