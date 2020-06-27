using Godot;
using System;

public class Player : KinematicBody2D
{
    private const float Speed = 70;
    private const float JumpHeight = 170;
    private const float Gravity = 400;
    private const float Acceleration = 0.25f;
    private const float Friction = 0.1f;
    private const float LandingSpeedReduction = 0.2f;
    
    public Vector2 Velocity = Vector2.Zero;

    public override void _PhysicsProcess(float delta)
    {
        GetHorizontalInput();
        ApplyGravity(delta);
        HandleJump();
        HandleMovement();
    }

    private void GetHorizontalInput()
    {
        var direction = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
        Velocity.x = Math.Abs(direction) > 0
            ? Mathf.Lerp(Velocity.x, direction * Speed, Acceleration)
            : Mathf.Lerp(Velocity.x, 0, Friction);
    }

    private void ApplyGravity(float delta)
    {
        Velocity.y += (Velocity.y < 0 ? Gravity : Gravity * (1 - LandingSpeedReduction)) * delta;
    }

    private void HandleJump()
    {
        if (!Input.IsActionJustPressed("ui_jump")) return;
        if (IsOnFloor())
        {
            Velocity.y = -JumpHeight;
        }
    }
    
    private void HandleMovement()
    {
        Velocity = MoveAndSlide(Velocity, Vector2.Up);
    }
}
