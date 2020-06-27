using Godot;
using System;

public class PlayerGravity : Node2D
{
    private Player _player;
    private const float Gravity = 400;
    private const float LandingSpeedReduction = 0.2f;

    public override void _Ready()
    {
        _player = GetNode<Player>("..");
    }

    public override void _PhysicsProcess(float delta)
    {
        _player.Velocity.y += (_player.Velocity.y < 0 ? Gravity : Gravity * (1 - LandingSpeedReduction)) * delta;
    }
}
