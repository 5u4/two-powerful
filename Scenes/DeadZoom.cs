using Godot;

namespace TwoPowerful.Scenes
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

        private void _on_DeadZoom_body_entered(Node body)
        {
            if (!body.GetGroups().Contains("Player")) return;
            body.QueueFree();
            _fallSfx.Play();
            _timer.Start();
        }

        private void _on_Timer_timeout()
        {
            _gameManager.ReloadLevel();
        }
    }
}
