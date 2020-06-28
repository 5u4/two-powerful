using System;
using DieWisely.Instances.Abilities;
using DieWisely.Instances.Player;
using Godot;

namespace DieWisely.Scenes
{
    public class GameManager : Node2D
    {
        private Player _player;
        private Node2D _orbs;
        private Label _label1;
        private Label _label2;
        private Label _instruction;
        private Ability _ability1;
        private Ability _ability2;

        public override void _Ready()
        {
            _player = GetNode<Player>("../Player");
            _orbs = GetNodeOrNull<Node2D>("../Orbs");
            _label1 = GetNode<Label>("Control/Ability1");
            _label2 = GetNode<Label>("Control/Ability2");
            _instruction = GetNode<Label>("Control/Instruction");
            if (_orbs != null)
            {
                foreach (var orb in _orbs.GetChildren())
                {
                    (orb as AbilityOrb)?.Connect("PickUpOrb", this, nameof(_on_OrbPickUp));
                }
            }
            UpdateLabels();
        }

        public override void _Process(float delta)
        {
            UpdateInstructionDisplay();
            if (!Input.IsActionJustPressed("ui_restart")) return;
            ReloadLevel();
        }

        public void ReloadLevel()
        {
            GetTree().ReloadCurrentScene();
        }

        private void _on_OrbPickUp(AbilityOrb orb)
        {
            _ability2 = _ability1;
            _ability1 = orb.Ability;
            orb.PickedUp();
            UpdateLabels();
            DisableAllAbilities();
            EnableAbility(_ability1);
            EnableAbility(_ability2);
        }

        private void UpdateLabels()
        {
            _label1.Text = _ability1.ToString();
            _label2.Text = _ability2.ToString();
        }

        private void UpdateInstructionDisplay()
        {
            if (Input.IsActionPressed("ui_info")) _instruction.Show();
            else _instruction.Hide();
        }

        private void DisableAllAbilities()
        {
            _player.Attack.Enabled = false;
            _player.Dash.Enabled = false;
            _player.Jump.DisableDoubleJump();
            _player.WallJump.Enabled = false;
        }

        private void EnableAbility(Ability ability)
        {
            switch (ability)
            {
                case Ability.Attack:
                    _player.Attack.Enabled = true;
                    break;
                case Ability.Dash:
                    _player.Dash.Enabled = true;
                    break;
                case Ability.DoubleJump:
                    _player.Jump.EnableDoubleJump();
                    break;
                case Ability.WallJump:
                    _player.WallJump.Enabled = true;
                    break;
                case Ability.Empty:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(ability), ability, null);
            }
        }
    }
}
