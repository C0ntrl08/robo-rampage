using Godot;
using System;

namespace RoboRampage.scripts
{
	public partial class Enemy : CharacterBody3D
	{
		[Export]
		public NavigationAgent3D NavigationAgent{get;set;}

		private CharacterBody3D Player {get;set;}

		public const float Speed = 5.0f;
		public const float JumpVelocity = 4.5f;

        public override void _Ready()
        {
			Player = (CharacterBody3D)GetTree().GetFirstNodeInGroup("Player");
            
        }


		public override void _PhysicsProcess(double delta)
		{
			NavigationAgent.TargetPosition = Player.GlobalPosition;
			var nextPosition = NavigationAgent.GetNextPathPosition();

			Vector3 velocity = Velocity;

			// Add the gravity.
			if (!IsOnFloor())
			{
				velocity += GetGravity() * (float)delta;
			}

			// Handle Jump.
			if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
			{
				velocity.Y = JumpVelocity;
			}

			// Get the input direction and handle the movement/deceleration.
			// As good practice, you should replace UI actions with custom gameplay actions.
			Vector2 inputDir = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
			Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
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
			// MoveAndSlide();
		}
	}
}
