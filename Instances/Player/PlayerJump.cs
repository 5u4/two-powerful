using Godot;

namespace TwoPowerful.Instances.Player
{
    public class PlayerJump : Node2D
    {
        private Player _player;
        private int _jumpCount;
        private int _maxJumpCount = 1;
    
        private const float JumpHeight = 170;

        public override void _Ready()
        {
            _player = GetNode<Player>("..");
        }

        public override void _PhysicsProcess(float delta)
        {
            if (_player.IsOnFloor()) _jumpCount = _maxJumpCount;
            if (!Input.IsActionJustPressed("ui_jump") || _jumpCount <= 0 || _player.ActionLockTimer.IsLocked ||
                _player.WallJump.WillWallJump()) return;
            _player.JumpSfx.Play();
            _player.Velocity.y = -JumpHeight;
            _jumpCount--;
            _player.WallJump.IsWallJumping = false;
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

        private bool CanDoubleJump()
        {
            return _maxJumpCount == 2;
        }
    }
}
