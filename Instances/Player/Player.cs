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
        public Vector2 Velocity = Vector2.Zero;

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
        }

        public override void _PhysicsProcess(float delta)
        {
            Velocity = MoveAndSlide(Velocity, Vector2.Up);
        }
    }
}
