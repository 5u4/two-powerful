using Godot;
using System;

public class PlayerDash : Node2D
{
    private Player _player;
    private PlayerGravity _gravity;
    private PlayerAnimatedSprite _animatedSprite;
    private int _dashCount;

    private const float DashDuration = 0.2f;
    private const float DashSpeed = 200f;
    private const int MaxDashCount = 1;
    
    public bool IsDashing { get; private set; }

    public override void _Ready()
    {
        _player = GetNode<Player>("..");
        _gravity = _player.GetNode<PlayerGravity>("Gravity");
        _animatedSprite = _player.GetNode<PlayerAnimatedSprite>("AnimatedSprite");
    }

    public override void _PhysicsProcess(float delta)
    {
        if (_player.IsOnFloor()) _dashCount = MaxDashCount;
        if (!Input.IsActionJustPressed("ui_dash") || _dashCount <= 0 || _player.ActionLockTimer.IsLocked) return;
        _gravity.SetPhysicsProcess(false);
        _dashCount--;
        IsDashing = true;
        var horizontal = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
        var vertical = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");
        if (horizontal == 0 && vertical == 0) horizontal = _animatedSprite.FlipH ? -1 : 1;
        _player.Velocity.x = horizontal * DashSpeed;
        _player.Velocity.y = vertical * DashSpeed;
        _player.ActionLockTimer.Lock(DashDuration, () =>
        {
            _gravity.SetPhysicsProcess(true);
            _player.Velocity = Vector2.Zero;
            IsDashing = false;
        });
    }
}
