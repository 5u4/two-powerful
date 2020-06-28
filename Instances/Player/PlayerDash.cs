using Godot;

namespace DieWisely.Instances.Player
{
    public class PlayerDash : Node2D
    {
        private Player _player;
        private int _dashCount;

        private const float DashDuration = 0.2f;
        private const float DashSpeed = 200f;
        private const int MaxDashCount = 1;
    
        public bool Enabled { get; set; }
        public bool IsDashing { get; private set; }

        public override void _Ready()
        {
            _player = GetNode<Player>("..");
        }

        public override void _PhysicsProcess(float delta)
        {
            if (!Enabled) return;
            if (_player.IsOnFloor()) _dashCount = MaxDashCount;
            if (WillDash()) Dash();
        }

        private void Dash()
        {
            _player.Gravity.Enabled = false;
            IsDashing = true;
            _dashCount--;
        
            var horizontal = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
            var vertical = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");
            if (horizontal == 0 && vertical == 0) horizontal = _player.AnimatedSprite.FlipH ? -1 : 1;
        
            _player.Velocity.x = horizontal * DashSpeed;
            _player.Velocity.y = vertical * DashSpeed;

            void FinishDash()
            {
                _player.Gravity.Enabled = true;
                _player.Velocity = Vector2.Zero;
                IsDashing = false;
            }

            _player.ActionLockTimer.Lock(DashDuration, FinishDash);
        }

        private bool WillDash()
        {
            return Enabled && Input.IsActionJustPressed("ui_dash") && _dashCount > 0 && !_player.ActionLockTimer.IsLocked;
        }
    }
}
