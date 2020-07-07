using Godot;

namespace TwoPowerful.Scenes.Levels
{
    public class Splash : Node2D
    {
        public override void _Ready()
        {
            Position = GetViewportRect().Size / 2;
        }
    }
}
