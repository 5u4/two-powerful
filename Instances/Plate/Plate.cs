using Godot;

namespace TwoOrbs.Instances.Plate
{
    public class Plate : Node2D
    {
        private Label _label;

        [Export] public string Hint;

        public override void _Ready()
        {
            _label = GetNode<Label>("Label");
            _label.Text = Hint;
        }

        private void _on_Area2D_body_entered(Node body)
        {
            if (!body.GetGroups().Contains("Player")) return;
            _label.Show();
        }

        private void _on_Area2D_body_exited(Node body)
        {
            if (!body.GetGroups().Contains("Player")) return;
            _label.Hide();
        }
    }
}
