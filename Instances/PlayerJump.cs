using Godot;
using System;

public class PlayerJump : Node2D
{
    private Player _player;
    private PlayerWallJump _playerWallJump;
    private int _jumpCount;
    private int _maxJumpCount = 2;
    
    private const float JumpHeight = 170;

    public override void _Ready()
    {
        _player = GetNode<Player>("..");
        _playerWallJump = _player.GetNode<PlayerWallJump>("WallJump");
    }

    public override void _PhysicsProcess(float delta)
    {
        if (_player.IsOnFloor()) _jumpCount = _maxJumpCount;
        if (!Input.IsActionJustPressed("ui_jump") || _jumpCount <= 0 || _player.ActionLockTimer.IsLocked ||
            _playerWallJump.WillWallJump()) return;
        _player.Velocity.y = -JumpHeight;
        _jumpCount--;
        _playerWallJump.IsWallJumping = false;
    }

    public bool IsDoubleJumping()
    {
        return CanDoubleJump() && _jumpCount < _maxJumpCount - 1;
    }

    public void EnableDoubleJump()
    {
        _maxJumpCount = 2;
    }

    public void DisableDoubleJump()
    {
        _maxJumpCount = 1;
    }

    public bool CanDoubleJump()
    {
        return _maxJumpCount == 2;
    }
}
