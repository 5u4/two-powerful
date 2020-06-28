using Godot;

namespace DieWisely.Scenes
{
    public class DeadZoom : Area2D
    {
        private GameManager _gameManager;
        private AudioStreamPlayer2D _fallSfx;
        private Timer _timer;

        public override void _Ready()
        {
            _gameManager = GetNode<GameManager>("../GameManager");
            _fallSfx = GetNode<AudioStreamPlayer2D>("FallSfx");
            _timer = GetNode<Timer>("Timer");
        }

        private void _on_DeadZoom_body_entered(object body)
        {
            _fallSfx.Play();
            _timer.Start();
        }

        private void _on_Timer_timeout()
        {
            _gameManager.ReloadLevel();
        }
    }
}
