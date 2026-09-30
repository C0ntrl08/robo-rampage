using Godot;
using System;

namespace RoboRampage.scripts
{
	public partial class Player : CharacterBody3D
	{
		[Export]
		public float Speed {get;set;} = 5.0f;
		[Export]
		public float JumpVelocity {get;set;} = 4.5f;

		private const string _moveForward = "move_forward";
		private const string _moveLeft = "move_left";
		private const string _moveRight = "move_right";
		private const string _moveBack = "move_back";
		private const string _jump = "jump";

		public override void _PhysicsProcess(double delta)
		{
			Vector3 velocity = Velocity;

			// Add the gravity.
			if (!IsOnFloor())
			{
				velocity += GetGravity() * (float)delta;
			}

			// Handle Jump.
			if (Input.IsActionJustPressed(_jump) && IsOnFloor())
			{
				velocity.Y = JumpVelocity;
			}

			Vector2 inputDir = Input.GetVector(_moveLeft, _moveRight, _moveForward, _moveBack);
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
			MoveAndSlide();
		}
	}
}