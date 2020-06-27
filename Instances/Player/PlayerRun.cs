using System;
using Godot;

namespace DieWisely.Instances.Player
{
    public class PlayerRun : Node2D
    {
        private Player _player;
        private const float Speed = 70;
        private const float Acceleration = 0.25f;
        private const float Friction = 0.1f;

        public override void _Ready()
        {
            _player = GetNode<Player>("..");
        }

        public override void _PhysicsProcess(float delta)
        {
            if (_player.ActionLockTimer.IsLocked) return;
            var direction = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
            _player.Velocity.x = Math.Abs(direction) > 0
                ? Mathf.Lerp(_player.Velocity.x, direction * Speed, Acceleration)
                : Mathf.Lerp(_player.Velocity.x, 0, Friction);
        }
    }
}
