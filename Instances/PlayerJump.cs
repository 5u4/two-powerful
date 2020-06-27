using Godot;
using System;

public class PlayerJump : Node2D
{
    private Player _player;
    
    private const float JumpHeight = 170;

    public override void _Ready()
    {
        _player = GetNode<Player>("..");
    }

    public override void _PhysicsProcess(float delta)
    {
        if (!Input.IsActionJustPressed("ui_jump") || !_player.IsOnFloor()) return;
        _player.Velocity.y = -JumpHeight;
    }
}
