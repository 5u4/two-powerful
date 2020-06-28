using Godot;

namespace DieWisely.Scenes
{
    public class DeadZoom : Area2D
    {
        private GameManager _gameManager;

        public override void _Ready()
        {
            _gameManager = GetNode<GameManager>("../GameManager");
        }

        private void _on_DeadZoom_body_entered(object body)
        {
            _gameManager.ReloadLevel();
        }
    }
}
