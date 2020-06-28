using Godot;
using System;

public class LevelNote : Control
{
    [Export] public string Note;

    public override void _Ready()
    {
        GetNode<Label>("CanvasLayer/Label").Text = Note;
    }

    private void _on_Timer_timeout()
    {
        QueueFree();
    }
}
