using Godot;

namespace DieWisely.Instances.Player
{
    public class Player : KinematicBody2D
    {
        public PlayerAnimatedSprite AnimatedSprite;
        public PlayerGravity Gravity;
        public PlayerRun Run;
        public PlayerJump Jump;
        public PlayerDash Dash;
        public PlayerAttack Attack;
        public PlayerWallJump WallJump;
        public ActionLockTimer ActionLockTimer;
        public AudioStreamPlayer2D JumpSfx;
        public AudioStreamPlayer2D AttackSfx;
        public AudioStreamPlayer2D DashSfx;
        public AudioStreamPlayer2D EnterSfx;
        public Vector2 Velocity = Vector2.Zero;

        public int Health { get; private set; }

        [Signal] public delegate void HealthChange();

        public override void _Ready()
        {
            AnimatedSprite = GetNode<PlayerAnimatedSprite>("AnimatedSprite");
            Gravity = GetNode<PlayerGravity>("Gravity");
            Run = GetNode<PlayerRun>("Run");
            Jump = GetNode<PlayerJump>("Jump");
            Dash = GetNode<PlayerDash>("Dash");
            Attack = GetNode<PlayerAttack>("Attack");
            WallJump = GetNode<PlayerWallJump>("WallJump");
            ActionLockTimer = GetNode<ActionLockTimer>("ActionLockTimer");
            JumpSfx = GetNode<AudioStreamPlayer2D>("JumpSfx");
            AttackSfx = GetNode<AudioStreamPlayer2D>("AttackSfx");
            DashSfx = GetNode<AudioStreamPlayer2D>("DashSfx");
            EnterSfx = GetNode<AudioStreamPlayer2D>("EnterSfx");
            EnterSfx.Play();
        }

        public override void _PhysicsProcess(float delta)
        {
            Velocity = MoveAndSlide(Velocity, Vector2.Up);
        }

        public void Hit()
        {
            Health--;
            EmitSignal(nameof(HealthChange));
        }
    }
}
