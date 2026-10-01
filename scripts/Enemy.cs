using Godot;
using System;

namespace RoboRampage.scripts
{
	public partial class Enemy : CharacterBody3D
	{
		[Export]
		public NavigationAgent3D NavigationAgent{get;set;}

		private CharacterBody3D Player {get;set;}
		private bool Provoked{get;set;} = false;
		private float AggroRange {get;set;} = 12.0f;

		public const float Speed = 5.0f;
		public const float JumpVelocity = 4.5f;

        public override void _Ready()
        {
			Player = (CharacterBody3D)GetTree().GetFirstNodeInGroup("Player");
            
        }

        public override void _Process(double delta)
        {
			if (Provoked)
			{
				NavigationAgent.TargetPosition = Player.GlobalPosition;
			}
        }


		public override void _PhysicsProcess(double delta)
		{
			var nextPosition = NavigationAgent.GetNextPathPosition();

			Vector3 velocity = Velocity;

			if (!IsOnFloor())
			{
				velocity += GetGravity() * (float)delta;
			}

			Vector3 direction = GlobalPosition.DirectionTo(nextPosition);
			float distance = GlobalPosition.DistanceTo(Player.GlobalPosition);

			if (distance <= AggroRange)
			{
				Provoked = true;
			}

			if (direction != Vector3.Zero)
			{
				velocity.X = direction.X * Speed;
				velocity.Z = direction.Z * Speed;
			}
			else
			{
				velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
				velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
			}

			Velocity = velocity;

			MoveAndSlide();
		}
	}
}
