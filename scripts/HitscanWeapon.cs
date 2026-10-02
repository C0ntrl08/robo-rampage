using Godot;

namespace RoboRampage.script
{
	public partial class HitscanWeapon : Node3D
	{
		[Export]
		public float FireRate{get;set;} = 14.0f; // times / seconds
		[Export]
		public Timer CooldownTimer{get;set;}

		private const string FIRE = "fire";

		public override void _Ready()
		{
		}

		public override void _Process(double delta)
		{
			if (Input.IsActionPressed(FIRE))
			{
				if (CooldownTimer.IsStopped())
				{
					CooldownTimer.Start(1.0f / FireRate);
					GD.Print("Weapon fired!");
				}
			}
		}
	}
}