using Godot;
using System;

public delegate void ActionCallback();

public class ActionLockTimer : Timer
{
    private ActionCallback _actionCallback;
    
    public bool IsLocked { get; private set; }

    public void Lock(float duration)
    {
        Lock(duration, () => { });
    }

    public void Lock(float duration, ActionCallback actionCallback)
    {
        _actionCallback = actionCallback;
        Start(duration);
        IsLocked = true;
    }

    public void Unlock()
    {
        IsLocked = false;
        Stop();
    }
    
    public void _on_ActionLockTimer_timeout()
    {
        IsLocked = false;
        _actionCallback?.Invoke();
        Stop();
    }
}
