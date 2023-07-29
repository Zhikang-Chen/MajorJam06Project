using System;
using UnityEngine;
using UnityEngine.AI;
using random = UnityEngine.Random;

//State Behavior
/// <summary>
/// It will pick a random Position on the nav mesh, then move to the location chosen
/// </summary>
public class Roam : IState
{
    private AlienBrain _alien;
    private NavMeshAgent _agent;

    private float _pickRadius = 30f;
    private Vector3 _randonDirection;

    private float _stopDistance = 0.5f;


    public Roam(AlienBrain alien)
    {
        _alien = alien;
    }

    public void Enter()
    {
        Debug.Log("<Color=red>" + _alien.transform.name + "</color> has entered Roam State");
        _agent = _alien.navMeshAgent;
    }

    public void Exit()
    {
        Debug.Log("<Color=red>" + _alien.transform.name + "</color> has exited Roam State");
    }

    public void tick()
    {
        if(_alien.patrolPoints.Count > 0)
        {
            PatrolPoints();
        } else
        {
            PatrolRandomPoints();
        }
    }

    public void PatrolRandomPoints()
    {
        if (_agent.remainingDistance < _stopDistance)
        {
            Debug.Log("Patroling a random Position");
            _agent.SetDestination(GetRandomPosition());
        }
    }

    int i;
    public void PatrolPoints()
    {
        if(_agent.remainingDistance < _stopDistance)
        {
            i++;

            if (i > _alien.patrolPoints.Count) i = 0;

            Debug.Log("Patrolling a patrol point");
            _agent.SetDestination(_alien.patrolPoints[i].position);
        }
    }

    public Vector3 GetRandomPosition()
    {
        _randonDirection = random.insideUnitCircle * _pickRadius;
        _randonDirection += _alien.transform.position;

        NavMesh.SamplePosition(_randonDirection, out NavMeshHit hit, _pickRadius, 1);
        Debug.Log(hit.position);
        return hit.position;
    }

}

