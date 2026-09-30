using Godot;
using System;
using System.Runtime;

namespace RoboRampage.scripts
{
	public partial class SmoothCamera : Camera3D
	{
		[Export]		
		public float Speed {get;set;} = 44.0f;

        public override void _PhysicsProcess(double delta)
        {
			float weight = (float)Mathf.Clamp(delta * Speed, 0.0f, 1.0f);

            this.GlobalTransform = GlobalTransform.InterpolateWith(GetParent<Node3D>().GlobalTransform, weight);
			this.GlobalPosition = GetParent<Node3D>().GlobalPosition;
        }

	}
}