using Godot;

namespace TwoOrbs.Instances.Blocker
{
    public class Blocker : KinematicBody2D
    {
        private AnimatedSprite _animatedSprite;
        private CollisionShape2D _collision;

        public override void _Ready()
        {
            _animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
            _animatedSprite.Play("idle");
            _collision = GetNode<CollisionShape2D>("CollisionShape2D");
        }

        public void Die()
        {
            _collision.QueueFree();
            _animatedSprite.Play("death");
        }

        private void _on_AnimatedSprite_animation_finished()
        {
            if (_animatedSprite.Animation != "death") return;
            QueueFree();
        }
    }
}
