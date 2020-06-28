using Godot;

namespace DieWisely.Instances.Player
{
    public class PlayerWallJump : Node2D
    {
        private Player _player;

        private const float JumpSpeed = 150;
        private const float LockDuration = 0.2f;
    
        public bool Enabled { get; set; }
        public bool IsWallJumping { get; set; }

        public override void _Ready()
        {
            _player = GetNode<Player>("..");
        }

        public override void _PhysicsProcess(float delta)
        {
            if (_player.IsOnFloor()) IsWallJumping = false;
            if (WillWallJump()) WallJump();

        }

        public bool WillWallJump()
        {
            return Enabled && Input.IsActionJustPressed("ui_jump") && !_player.IsOnFloor() && _player.IsOnWall() &&
                   !_player.ActionLockTimer.IsLocked;
        }

        private void WallJump()
        {
            IsWallJumping = true;
            var horizontal = Input.GetActionStrength("ui_left") - Input.GetActionStrength("ui_right");
            _player.Velocity.x = horizontal * JumpSpeed;
            _player.Velocity.y = -JumpSpeed;
            _player.ActionLockTimer.Lock(LockDuration);
        }
    }
}
