using Godot;

namespace TwoPowerful.Scenes.Levels
{
    public class PressToStartFinal : Label
    {
        [Export] public PackedScene NextScene;

        public override void _PhysicsProcess(float delta)
        {
            if (!Input.IsActionJustPressed("ui_jump")) return;
            GetTree().ChangeSceneTo(NextScene);
        }
    }
}
