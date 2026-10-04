using Godot;
using System;

namespace RoboRampage.scripts
{
	public partial class Player : CharacterBody3D
	{
		[Export]
		public float Speed {get;set;} = 5.0f;
		[Export]
		public float JumpHeight {get;set;} = 1.0f;
		[Export]
		public float FallMultiplier{get;set;} = 3.0f;
		[Export]
		public float MouseSensitivity {get;set;} = 0.001f;
		[Export]
		public Node3D CameraPivot{get;set;}
		[Export]
		public int MaxHitPoints{get;set;} = 100;
		[Export]
		public AnimationPlayer DamageAnimationPlayer{get;set;}

		private int _hitPoints;
		public int HitPoints
		{
			get { return _hitPoints; }
			set
			{
				if (value < _hitPoints)
				{
					DamageAnimationPlayer.Stop(false);
					DamageAnimationPlayer.Play("TakeDamage");
				}
				_hitPoints = value;
				GD.Print(_hitPoints);
				if (_hitPoints <= 0)
				{
					GetTree().Quit();
				}
			}
		}
		
		private Vector2 _mouseMotion = Vector2.Zero;

		private const string _moveForward = "move_forward";
		private const string _moveLeft = "move_left";
		private const string _moveRight = "move_right";
		private const string _moveBack = "move_back";
		private const string _jump = "jump";
		private const string _cancel = "ui_cancel";

        public override void _Ready()
        {
            Input.MouseMode = Input.MouseModeEnum.Captured;
			_hitPoints = MaxHitPoints;
        }

		public override void _PhysicsProcess(double delta)
		{
			Vector3 velocity = Velocity;

			HandleCameraRotation();

			// Add the gravity.
			if (!IsOnFloor())
			{
				if (velocity.Y >= 0)
				{
					velocity.Y += (float)(GetGravity().Y * delta);
				}
				else
				{
					velocity.Y += (float)(GetGravity().Y * delta * FallMultiplier);
				}
			}

			// Handle Jump.
			if (Input.IsActionJustPressed(_jump) && IsOnFloor())
			{
				double gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsDouble();
				velocity.Y = (float)Math.Sqrt(JumpHeight * 2.0f * gravity);
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

        public override void _Input(InputEvent @event)
        {
            if (@event is InputEventMouseMotion eventMouseMotion)
			{
				if (Input.MouseMode == Input.MouseModeEnum.Captured)
				{
					_mouseMotion = -eventMouseMotion.Relative * MouseSensitivity;
				}
			}

			if (@event.IsActionPressed(_cancel))
			{
				Input.MouseMode = Input.MouseModeEnum.Visible;
			}
        }

		public void HandleCameraRotation()
		{
			RotateY(_mouseMotion.X);
			CameraPivot.RotateX(_mouseMotion.Y);
			Vector3 rotationDegrees = CameraPivot.RotationDegrees;
			rotationDegrees.X = Mathf.Clamp(rotationDegrees.X, -90.0f, 90.0f);
			CameraPivot.RotationDegrees = rotationDegrees;
			_mouseMotion = Vector2.Zero;
		}

	}
}