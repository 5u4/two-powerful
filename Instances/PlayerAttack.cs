using Godot;
using System;

public class PlayerAttack : Node2D
{
    private Player _player;
    private PlayerAnimatedSprite _animatedSprite;
    
    public bool IsAttacking { get; private set; }
    
    public override void _Ready()
    {
        _player = GetNode<Player>("..");
        _animatedSprite = _player.GetNode<PlayerAnimatedSprite>("AnimatedSprite");
    }

    public override void _PhysicsProcess(float delta)
    {
        if (!Input.IsActionJustPressed("ui_attack") || _player.ActionLockTimer.IsLocked) return;
        IsAttacking = true;
        if (_player.IsOnFloor()) _player.Velocity.x = 0;
        _player.ActionLockTimer.Lock(float.PositiveInfinity);
    }

    public void _on_Attack_animation_finished()
    {
        if (_animatedSprite.Animation != "attack") return;
        IsAttacking = false;
        _player.ActionLockTimer.Unlock();
    }
}
