using Godot;
using System;

public class PlayerDoubleJump : Node2D
{
    private Player _player;
    private int _doubleJumpCount;

    private const float JumpHeight = 170;
    private const int MaxDoubleJumpCount = 1;
    
    public bool IsDoubleJumping { get; private set; }
    
    public override void _Ready()
    {
        _player = GetNode<Player>("..");
    }

    public override void _PhysicsProcess(float delta)
    {
        if (_player.IsOnFloor())
        {
            _doubleJumpCount = MaxDoubleJumpCount;
            IsDoubleJumping = false;
            return;
        }

        if (!Input.IsActionJustPressed("ui_jump") || _doubleJumpCount <= 0 || _player.IsOnWall()) return;

        _player.Velocity.y = -JumpHeight;
        IsDoubleJumping = true;
        _doubleJumpCount--;
    }
}
