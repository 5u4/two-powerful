using Godot;
using System;

public class Player : KinematicBody2D
{
    private int _doubleJumpCount;
    private int _dashCount;
    
    private const float JumpHeight = 170;
    private const int MaxDoubleJumpCount = 1;
    private const int MaxDashCount = 1;
    
    public bool IsDoubleJumping { get; private set; }
    public Vector2 Velocity = Vector2.Zero;

    public override void _PhysicsProcess(float delta)
    {
        HandleJump();
        HandleDash();
        HandleMovement();
    }

    private void HandleJump()
    {
        var isOnFloor = IsOnFloor();
        if (isOnFloor)
        {
            IsDoubleJumping = false;
            _doubleJumpCount = MaxDoubleJumpCount;
        }
        if (!Input.IsActionJustPressed("ui_jump")) return;
        if (isOnFloor)
        {
            Velocity.y = -JumpHeight;
            return;
        }
        if (!CanDoubleJump()) return;
        IsDoubleJumping = true;
        Velocity.y = -JumpHeight;
        _doubleJumpCount--;
    }

    private void HandleDash()
    {
        if (IsOnFloor()) _dashCount = MaxDashCount;
        if (!Input.IsActionJustPressed("ui_dash") || !CanDash()) return;
        _dashCount--;
        // TODO: Dash
    }
    
    private void HandleMovement()
    {
        Velocity = MoveAndSlide(Velocity, Vector2.Up);
    }

    private bool CanDoubleJump()
    {
        return _doubleJumpCount > 0;
    }

    private bool CanDash()
    {
        return _dashCount > 0;
    }
}
