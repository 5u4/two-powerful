using TwoOrbs.Instances.Player;
using Godot;

namespace TwoOrbs.UI
{
    public class PlayerHealth : Control
    {
        private Player _player;
        private AnimatedSprite heart1;
        private AnimatedSprite heart2;

        [Export] public NodePath PlayerNodePath;

        public override void _Ready()
        {
            _player = GetNode<Player>(PlayerNodePath);
            _player.Connect("HealthChange", this, nameof(_on_Player_HealthChange));
            heart1 = GetNode<AnimatedSprite>("H1");
            heart2 = GetNode<AnimatedSprite>("H2");
        }

        private void _on_Player_HealthChange()
        {
            var health = _player.Health;
            switch (health)
            {
                case 2:
                    GainHeart(heart2);
                    GainHeart(heart1);
                    break;
                case 1:
                    LostHeart(heart2);
                    GainHeart(heart1);
                    break;
                default:
                    LostHeart(heart2);
                    LostHeart(heart1);
                    break;
            }
        }

        private void LostHeart(AnimatedSprite heart)
        {
            if (heart.Animation != "full") return;
            heart.Play("disappear");
        }

        private void GainHeart(AnimatedSprite heart)
        {
            heart.Play("full");
        }
    }
}



