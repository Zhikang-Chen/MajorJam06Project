using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using random = UnityEngine.Random;

public class Search : IState
{
    private AlienBrain _alien;
    private SmellDetector _nose;
    private NavMeshAgent _agent;


    private const float STOPDISTANCE = 0.5f;
    private const float CHECKDISTANCE = 6f;

    public bool hasFinishedChecking = false;
    public bool playerHasRun = false;

    float initialSpeed;

    public Search(AlienBrain alien, SmellDetector smellDetector)
    {
        _alien = alien;
        _nose = smellDetector;
    }

    public void Enter()
    {
        Debug.Log("<Color=red>" + _alien.transform.name + "</color> has entered Search State");

        _agent = _alien.agent;

        initialSpeed = _agent.speed;

        hasFinishedChecking = false;
        playerHasRun = false;

        ChooseNewTarget();
    }

    public void Exit()
    {
        Debug.Log("<Color=red>" + _alien.transform.name + "</color> has exited Search State");
        _agent.speed = initialSpeed;
        _alien.isAttacking = false;
    }

    public void tick()
    {
        CheckTargetValidity();

        if(_agent.remainingDistance < CHECKDISTANCE)
        {
            CheckPlantSmell();
        }

        if(_agent.remainingDistance < STOPDISTANCE)
        {
            DestroyPlant();
        }
    }

  
    private void CheckPlantSmell()
    {
        var tar = _nose.NearestSmell.GetComponent<SmellAble>();

        if (tar != null)
        {
            _agent.speed = _alien.chase_Speed;

            if (_nose.NearestSmell.GetComponent<PlayerMovement>() != null)
            {
                _agent.speed = _alien.chase_Speed * _alien.crouch_Modifier;
            }
        }
    }

    private void DestroyPlant()
    {
        //Is this smart
        //God no
        //Will I do it anyways.. yes
        _alien.isAttacking = true;
    }

    private void ChooseNewTarget()
    {
        _agent.speed = initialSpeed;

        _agent.SetDestination(_nose.NearestSmell.transform.position);
    }


    public void CheckTargetValidity()
    {
        NavMesh.SamplePosition(_nose.NearestSmell.transform.position, out NavMeshHit hit, 1.0f, 1);

        if (hit.hit)
        {
            return;
        }

        playerHasRun = true;
    }
}

