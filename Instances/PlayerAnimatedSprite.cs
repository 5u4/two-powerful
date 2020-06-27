using Godot;
using System;

public class PlayerAnimatedSprite : AnimatedSprite
{
    private Player _player;
    private const float RunningThreshold = 10f;
    
    public override void _Ready()
    {
        _player = GetNode<Player>("..");
    }

    public override void _Process(float delta)
    {
        HandleHorizontalFlip();
        Play(GetAnimationState());
    }

    private void HandleHorizontalFlip()
    {
        int facing = Math.Sign(_player.Velocity.x);
        if (facing != 0) FlipH = facing != 1;
    }

    private string GetAnimationState()
    {
        return Math.Abs(_player.Velocity.x) > RunningThreshold ? "run" : "idle";
    }
}
