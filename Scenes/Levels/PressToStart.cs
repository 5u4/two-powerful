using Godot;

namespace DieWisely.Scenes.Levels
{
    public class PressToStart : Label
    {
        [Export] public PackedScene NextScene;

        public override void _Input(InputEvent @event)
        {
            if (!(@event is InputEventKey) || !@event.IsPressed()) return;
            GetTree().ChangeSceneTo(NextScene);
        }
    }
}
