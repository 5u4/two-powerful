using DieWisely.Instances.Abilities;
using DieWisely.Instances.Player;
using Godot;

namespace DieWisely.Scenes
{
    public class GameManager : Node2D
    {
        private Player _player;
        private Node2D _orbs;

        public override void _Ready()
        {
            _player = GetNode<Player>("../Player");
            _orbs = GetNode<Node2D>("../Orbs");
            foreach (var orb in _orbs.GetChildren())
            {
                (orb as AbilityOrb)?.Connect("PickUpOrb", this, nameof(_on_OrbPickUp));
            }
        }

        private void _on_OrbPickUp(AbilityOrb orb)
        {
            orb.PickedUp();
        }
    }
}
