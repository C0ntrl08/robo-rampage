using Godot;

namespace RoboRampage.scripts
{
	public partial class Enemy : CharacterBody3D
	{
		[Export]
		public NavigationAgent3D NavigationAgent{get;set;}
		[Export]
		public float AttackRange {get;set;} = 1.5f;
		[Export]
		public int MaxHitPoints{get;set;} = 100;
		[Export]
		public int AttackDamage {get;set;} = 20;
		// [Export]
		// public AnimationPlayer AttackAnimationPlayer {get;set;}
		[Export]
		public AnimationTree AnimationTree { get; set; }

		private AnimationNodeStateMachinePlayback Playback { get; set; }
		private CharacterBody3D Player {get;set;}
		private bool Provoked{get;set;} = false;
		private float AggroRange {get;set;} = 12.0f;
		private int _hitPoints;
		public int HitPoints
		{
			get { return _hitPoints; }
			set
			{
				_hitPoints = value;
				if (_hitPoints <= 0)
				{
					QueueFree();
				}
				Provoked = true;
			}
		}

		public const float Speed = 5.0f;
		public const float JumpVelocity = 4.5f;

        public override void _Ready()
        {
			Player = (CharacterBody3D)GetTree().GetFirstNodeInGroup("Player");
			_hitPoints = MaxHitPoints;

			Playback = (AnimationNodeStateMachinePlayback)(GodotObject)AnimationTree.Get("parameters/playback");
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

			if (Provoked)
			{
				if (distance <= AttackRange)
				{
					Playback.Travel("Attack");
					// AttackAnimationPlayer.Play("Attack");
				}
			}

			if (direction != Vector3.Zero)
			{
				LookAtTarget(direction);
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

		public void Attack()
		{
			// GD.Print("Enemy Attack");
			((Player)Player).HitPoints -= AttackDamage;
		}

		private void LookAtTarget(Vector3 direction)
		{
			Vector3 adjustedDirection = direction;
			adjustedDirection.Y = 0;

			LookAt(GlobalPosition + adjustedDirection, Vector3.Up, true);
		}

		
	}
}
