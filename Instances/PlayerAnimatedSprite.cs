using Godot;
using System;

public class PlayerAnimatedSprite : AnimatedSprite
{
    private Player _player;
    private PlayerJump _playerJump;
    private PlayerDash _playerDash;
    private PlayerAttack _playerAttack;
    private bool _borning = true;
    
    private const float RunningThreshold = 10f;

    public override void _Ready()
    {
        _player = GetNode<Player>("..");
        _playerJump = _player.GetNode<PlayerJump>("Jump");
        _playerDash = _player.GetNode<PlayerDash>("Dash");
        _playerAttack = _player.GetNode<PlayerAttack>("Attack");
    }

    public override void _Process(float delta)
    {
        HandleHorizontalFlip();
        Play(GetAnimationState());
    }

    private void HandleHorizontalFlip()
    {
        var facing = Math.Sign(_player.Velocity.x);
        if (facing != 0) FlipH = facing != 1;
    }

    private string GetAnimationState()
    {
        if (_borning)
        {
            _player.ActionLockTimer.Lock(float.PositiveInfinity);
            return "born";
        }
        if (_playerAttack.IsAttacking) return "attack";
        if (_playerDash.IsDashing) return "dash";
        if (_player.IsOnFloor()) return Math.Abs(_player.Velocity.x) > RunningThreshold ? "run" : "idle";
        if (_player.Velocity.y < 0) return _playerJump.IsDoubleJumping() ? "doublejump" : "jump";
        return _player.Velocity.y > 0 ? "fall" : null;
    }

    private void _on_Born_animation_finished()
    {
        if (Animation != "born") return;
        _borning = false;
        _player.ActionLockTimer.Unlock();
    }
}
