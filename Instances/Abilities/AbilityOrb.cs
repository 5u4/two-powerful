using Godot;

namespace DieWisely.Instances.Abilities
{
    public enum Ability
    {
        Empty,
        DoubleJump,
        Dash,
        WallJump,
        Attack,
    }

    public class AbilityOrb : Node2D
    {
        private bool _pickable;
        private AnimatedSprite _animatedSprite;
        private Label _abilityLabel;
        private Area2D _area2D;

        [Signal] public delegate void PickUpOrb(AbilityOrb orb);
        [Export] public Ability Ability;

        public override void _Ready()
        {
            _animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
            _animatedSprite.Play();
            _abilityLabel = GetNode<Label>("Label");
            _abilityLabel.Text = Ability.ToString();
            _area2D = GetNode<Area2D>("Area2D");
        }

        public override void _PhysicsProcess(float delta)
        {
            UpdateTextDisplay();
            if (!_pickable || !Input.IsActionJustPressed("ui_pick")) return;
            EmitSignal(nameof(PickUpOrb), this);
        }

        public void PickedUp()
        {
            _animatedSprite.Play("pickedup");
            _area2D.QueueFree();
        }

        public void UpdateTextDisplay()
        {
            if (_pickable || Input.IsActionPressed("ui_info")) _abilityLabel.Show();
            else _abilityLabel.Hide();
        }

        private void _on_Area2D_body_entered(Node body)
        {
            if (!body.GetGroups().Contains("Player")) return;
            _pickable = true;
        }

        private void _on_Area2D_body_exited(Node body)
        {
            if (!body.GetGroups().Contains("Player")) return;
            _pickable = false;
        }

        private void _on_AnimatedSprite_animation_finished()
        {
            if (_animatedSprite.Animation != "pickedup") return;
            QueueFree();
        }
    }
}
