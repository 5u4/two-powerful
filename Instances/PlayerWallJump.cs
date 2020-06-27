using Godot;
using System;

public class PlayerWallJump : Node2D
{
    private Player _player;

    private const float JumpSpeed = 150;
    private const float LockDuration = 0.2f;
    
    public bool Enabled { get; set; }
    public bool IsWallJumping { get; set; }

    public override void _Ready()
    {
        _player = GetNode<Player>("..");
        Enabled = true;
    }

    public override void _PhysicsProcess(float delta)
    {
        if (_player.IsOnFloor()) IsWallJumping = false;
        if (!Input.IsActionJustPressed("ui_jump") || !WillWallJump()) return;
        IsWallJumping = true;
        var horizontal = Input.GetActionStrength("ui_left") - Input.GetActionStrength("ui_right");
        _player.Velocity.x = horizontal * JumpSpeed;
        _player.Velocity.y = -JumpSpeed;
        _player.ActionLockTimer.Lock(LockDuration);
    }

    public bool WillWallJump()
    {
        return Enabled && !_player.IsOnFloor() && _player.IsOnWall();
    }
}
