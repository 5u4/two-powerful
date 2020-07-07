using System;
using Godot;

namespace TwoPowerful.Instances.Player
{
    public class PlayerAnimatedSprite : AnimatedSprite
    {
        private Player _player;
        private bool _borning = true;

        private const float RunningThreshold = 10f;

        public override void _Ready()
        {
            _player = GetNode<Player>("..");
            GetNode<ActionLockTimer>("../ActionLockTimer").Lock(float.PositiveInfinity);
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
            if (_borning) return "born";
            if (_player.Attack.IsAttacking) return "attack";
            if (_player.Dash.IsDashing) return "dash";
            if (_player.IsOnFloor()) return Math.Abs(_player.Velocity.x) > RunningThreshold ? "run" : "idle";
            if (_player.Velocity.y < 0)
                return _player.Jump.IsDoubleJumping() && !_player.WallJump.IsWallJumping ? "doublejump" : "jump";
            return _player.Velocity.y > 0 ? "fall" : null;
        }

        private void _on_Born_animation_finished()
        {
            if (Animation != "born") return;
            _borning = false;
            _player.ActionLockTimer.Unlock();
            _player.Gravity.Enabled = true;
        }
    }
}
