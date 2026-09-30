using Godot;
using System;
using System.Numerics;

namespace RoboRampage.scripts
{
    [Tool]
    public partial class Crosshair : Control
    {
        public override void _Draw()
        {
            DrawCircle(Godot.Vector2.Zero, 4f, Colors.DimGray); // outer
            DrawCircle(Godot.Vector2.Zero, 3f, Colors.White); // inner

            DrawLine(new Godot.Vector2(16, 0), new Godot.Vector2(24, 0), Colors.White, 2); // right
            DrawLine(new Godot.Vector2(-16, 0), new Godot.Vector2(-24, 0), Colors.White, 2); // left

            DrawLine(new Godot.Vector2(0, 16), new Godot.Vector2(0, 24), Colors.White, 2); // top
            DrawLine(new Godot.Vector2(0, -16), new Godot.Vector2(0, -24), Colors.White, 2); // down
        }

    }
}