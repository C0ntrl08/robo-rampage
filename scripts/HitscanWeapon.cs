using Godot;

namespace RoboRampage.script
{
	public partial class HitscanWeapon : Node3D
	{
		[Export]
		public float FireRate{get;set;} = 14.0f; // times / seconds
		[Export]
		public Timer CooldownTimer{get;set;}
		[Export]
		public float Recoil {get; set;} = 0.05f;
		[Export]
		public Node3D WeaponMesh {get;set;}
		[Export]
		public RayCast3D RayCast3D {get;set;}

		protected Vector3 WeaponPosition{get;set;}
		private const string FIRE = "fire";


		public override void _Ready()
		{
			WeaponPosition = WeaponMesh.Position;
		}

		public override void _Process(double delta)
		{
			if (Input.IsActionPressed(FIRE))
			{
				if (CooldownTimer.IsStopped())
				{
					Shoot();
				}
			}

			WeaponMesh.Position = WeaponMesh.Position.Lerp(WeaponPosition, (float)delta * 10.0f);
		}

		public void Shoot()
		{
			CooldownTimer.Start(1.0f / FireRate);
			GD.Print(RayCast3D.GetCollider());
			
			WeaponMesh.Position = new Godot.Vector3(WeaponMesh.Position.X, WeaponMesh.Position.Y, WeaponMesh.Position.Z + Recoil);
		}
	}
}