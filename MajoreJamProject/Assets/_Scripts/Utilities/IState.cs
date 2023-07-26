using System;
public interface IState
{
    public void Enter();
    public void tick();
    public void Exit();
}

