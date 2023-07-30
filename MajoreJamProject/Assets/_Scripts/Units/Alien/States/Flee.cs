using System;
using UnityEngine;
using UnityEngine.AI;

public class Flee : IState
{
    private AlienBrain _alien;
    private NavMeshAgent _agent;

    private const float FLEE_SPEED = 6F;
    private const float FLEE_DISTANCE = 5F;

    public bool fled;

    public Flee(AlienBrain alien)
    {
        _alien = alien;
    }

    public void Enter()
    {
        _agent = _alien.agent;

        RunAway();
    }

    public void Exit()
    {
        
    }

    public void tick()
    {
        if(_agent.remainingDistance < 2f)
        {
            fled = true;
        }
    }

    private void RunAway()
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * FLEE_DISTANCE;

        Vector3 runAwayPosition = _agent.destination + randomDirection;

        NavMeshHit navMeshHit;

        if (NavMesh.SamplePosition(runAwayPosition, out navMeshHit, FLEE_DISTANCE, NavMesh.AllAreas))
        {
            _agent.SetDestination(navMeshHit.position);
            _agent.speed = FLEE_SPEED;
        }
    }

}

