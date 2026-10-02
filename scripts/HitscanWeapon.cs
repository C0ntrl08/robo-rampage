using Godot;

namespace RoboRampage.scripts
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
		public int WeaponDamage {get;set;} = 10;
		[Export]
		public Node3D WeaponMesh {get;set;}
		[Export]
		public RayCast3D RayCast3D {get;set;}
		[Export]
		public GpuParticles3D MuzzleFlash {get;set;}
		[Export]
		public PackedScene Sparks{get;set;}

		protected Vector3 WeaponPosition{get;set;}
		protected Enemy Enemy {get;set;}
		protected const string FIRE = "fire";


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
			MuzzleFlash.Restart();

			CooldownTimer.Start(1.0f / FireRate);
			var collider = RayCast3D.GetCollider();
			GD.Print(collider);
			
			WeaponMesh.Position = new Godot.Vector3(WeaponMesh.Position.X, WeaponMesh.Position.Y, WeaponMesh.Position.Z + Recoil);
			if (collider is Enemy enemy)
			{
				enemy.HitPoints -= WeaponDamage;
			}

			Node3D spark = Sparks.Instantiate<Node3D>();
			AddChild(spark);
			spark.GlobalPosition = RayCast3D.GetCollisionPoint();
		}
	}
}