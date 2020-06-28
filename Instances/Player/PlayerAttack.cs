using Godot;

namespace DieWisely.Instances.Player
{
    public class PlayerAttack : Node2D
    {
        private Player _player;
        private Area2D _hitBox;
        private CollisionShape2D _hitCollision;
        private float _hitBoxOriginalX;

        public bool Enabled { get; set; }
        public bool IsAttacking { get; private set; }

        public override void _Ready()
        {
            _player = GetNode<Player>("..");
            _hitBox = _player.GetNode<Area2D>("HitBox");
            _hitBoxOriginalX = _hitBox.Position.x;
            _hitCollision = _hitBox.GetNode<CollisionShape2D>("CollisionShape2D");
            _hitCollision.Disabled = true;
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
            _hitCollision.Disabled = true;
        }

        private bool WillAttack()
        {
            return Enabled && Input.IsActionJustPressed("ui_attack") && !_player.ActionLockTimer.IsLocked;
        }

        private void Attack()
        {
            _player.AttackSfx.Play();
            _hitBox.Position = new Vector2(_player.AnimatedSprite.FlipH ? -_hitBoxOriginalX : _hitBoxOriginalX, 0);
            _hitCollision.Disabled = false;
            IsAttacking = true;
            if (_player.IsOnFloor()) _player.Velocity.x = 0;
            _player.ActionLockTimer.Lock(float.PositiveInfinity);
        }

        private void _on_HitBox_body_entered(Node body)
        {
            (body as Blocker.Blocker)?.Die();
        }
    }
}
