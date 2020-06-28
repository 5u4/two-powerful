using Godot;

namespace TwoOrbs.Instances.Gate
{
    public class Gate : Node2D
    {
        [Export] public PackedScene _nextScene;

        private void _on_Area2D_body_entered(Node body)
        {
            if (!body.GetGroups().Contains("Player")) return;
            GetTree().ChangeSceneTo(_nextScene);
        }
    }
}
