using Godot;

namespace DieWisely.Instances.Player
{
    public class PlayerAttack : Node2D
    {
        private Player _player;
    
        public bool Enabled { get; set; }
        public bool IsAttacking { get; private set; }
    
        public override void _Ready()
        {
            _player = GetNode<Player>("..");
        }

        public override void _PhysicsProcess(float delta)
        {
            if (WillAttack()) Attack();
        }

        public void _on_Attack_animation_finished()
        {
            if (_player.AnimatedSprite.Animation != "attack") return;
            IsAttacking = false;
            _player.ActionLockTimer.Unlock();
        }

        private bool WillAttack()
        {
            return Enabled && Input.IsActionJustPressed("ui_attack") && !_player.ActionLockTimer.IsLocked;
        }

        private void Attack()
        {
            IsAttacking = true;
            if (_player.IsOnFloor()) _player.Velocity.x = 0;
            _player.ActionLockTimer.Lock(float.PositiveInfinity);
        }
    }
}
