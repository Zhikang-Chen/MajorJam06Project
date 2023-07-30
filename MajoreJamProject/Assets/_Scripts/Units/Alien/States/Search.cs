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

    private List<GameObject> ObjectSmelled;
    private int _target = -1;

    private const float STOPDISTANCE = 0.5f;
    private const float CHECKDISTANCE = 4f;

    public bool hasFinishedChecking = false;
    public bool OnionDectected = false;

    float initialSpeed;

    public Search(AlienBrain alien, SmellDetector smellDetector)
    {
        _alien = alien;
        _nose = smellDetector;
    }

    public void Enter()
    {
        Debug.Log("<Color=red>" + _alien.transform.name + "</color> has entered Search State");

        _target = -1;

        SortPlantOrder(out ObjectSmelled);
        ChooseNewTarget();

        initialSpeed = _agent.speed;

        hasFinishedChecking = false;
        OnionDectected = false;
    }

    public void Exit()
    {
        Debug.Log("<Color=red>" + _alien.transform.name + "</color> has exited Search State");
        _agent.speed = initialSpeed;
    }

    public void tick()
    {
        if(_agent.remainingDistance < CHECKDISTANCE)
        {
            CheckPlantSmell();
        }

        if(_agent.remainingDistance < STOPDISTANCE)
        {
            DestroyPlant();
            ChooseNewTarget();
        }
    }

  
    private void CheckPlantSmell()
    {
        var tar = ObjectSmelled[_target].GetComponent<SmellAble>();

        if (tar.smellType == SMELL_TYPE.Repel)
        {
            OnionDectected = true;
        }

        if(tar.smellType == SMELL_TYPE.Attract)
        {
            _agent.speed = _alien.chase_Speed;

            if (ObjectSmelled[_target].GetComponent<PlayerMovement>() != null)
            {
                _agent.speed = _alien.chase_Speed * _alien.crouch_Modifier;
            }
        }
    }

    private void DestroyPlant()
    {
        //Destroy animation would go here
        ObjectSmelled[_target].SetActive(false);
        ObjectSmelled.Remove(ObjectSmelled[_target]);
    }

    private void ChooseNewTarget()
    {
        _agent.speed = initialSpeed;

        _target++;
        if (_target > ObjectSmelled.Count) hasFinishedChecking = false;


        _agent.SetDestination(ObjectSmelled[0].transform.position);
    }

    public void SortPlantOrder(out List<GameObject> obj)
    {
        var sorted = _nose.ObjectSmelled.OrderBy(pair => Vector3.Distance(_alien.transform.position, pair.transform.position)).ToList();

        obj = sorted;
    }
}

