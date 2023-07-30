using System;
using UnityEngine.AI;

public class Flee : IState
{
    private AlienBrain _alien;
    private SmellDetector _nose;
    private NavMeshAgent _agent;

    public Flee(AlienBrain alien)
    {
        _alien = alien;
    }

    public void Enter()
    {
        _nose = _alien.AlienNose;
        _agent = _alien.navMeshAgent;
    }

    public void Exit()
    {
        throw new NotImplementedException();
    }

    public void tick()
    {
        throw new NotImplementedException();
    }
}

