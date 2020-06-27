using Godot;
using System;

public class Player : KinematicBody2D
{
    public ActionLockTimer ActionLockTimer;
    public Vector2 Velocity = Vector2.Zero;

    public override void _Ready()
    {
        ActionLockTimer = GetNode<ActionLockTimer>("ActionLockTimer");
    }

    public override void _PhysicsProcess(float delta)
    {
        Velocity = MoveAndSlide(Velocity, Vector2.Up);
    }
}
