using Godot;
using System;

public class Player : KinematicBody2D
{
    private int _dashCount;
    
    private const int MaxDashCount = 1;
    
    public Vector2 Velocity = Vector2.Zero;

    public override void _PhysicsProcess(float delta)
    {
        HandleDash();
        Velocity = MoveAndSlide(Velocity, Vector2.Up);
    }

    private void HandleDash()
    {
        if (IsOnFloor()) _dashCount = MaxDashCount;
        if (!Input.IsActionJustPressed("ui_dash") || !CanDash()) return;
        _dashCount--;
        // TODO: Dash
    }

    private bool CanDash()
    {
        return _dashCount > 0;
    }
}
