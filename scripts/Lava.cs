using Godot;

namespace RoboRampage.scripts
{
    public partial class Lava : Area3D
    {
        public void OnBodyEntered(Node3D body)
        {
            if (body.IsInGroup("Player") || body is Enemy)
            {
                ((Player)body).HitPoints = 0;
                
            }
        }
    }
}