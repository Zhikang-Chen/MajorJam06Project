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

    private List<GameObject> plants;
    private int _plantTarget = -1;

    private float _stopDistance = 0.5f;
    private float _CheckDistance = 3f;

    public bool hasFinishedChecking = false;
    public bool OnionDectected = false;

    public Search(AlienBrain alien, SmellDetector smellDetector)
    {
        _alien = alien;
    }

    public void Enter()
    {
        Debug.Log("<Color=red>" + _alien.transform.name + "</color> has entered Search State");
        SortPlantOrder(out plants);
        ChooseNewTarget();

        hasFinishedChecking = false;
        OnionDectected = false;
    }

    public void Exit()
    {
        Debug.Log("<Color=red>" + _alien.transform.name + "</color> has exited Search State");
    }

    public void tick()
    {
        if(_agent.remainingDistance < _CheckDistance)
        {
            CheckPlantSmell();
        }

        if(_agent.remainingDistance < _stopDistance)
        {
            DestroyPlant();
            ChooseNewTarget();
        }
    }

    private void CheckPlantSmell()
    {
        if (plants[_plantTarget].GetComponent<Plant>().plantType == PLANT_TYPE.Repel)
        {
            OnionDectected = true;
        }
    }

    private void DestroyPlant()
    {
        //Destroy animation would go here
        plants[_plantTarget].GetComponent<Plant>().DestroyPlant();
    }

    private void ChooseNewTarget()
    {
        _plantTarget++;
        if (_plantTarget > plants.Count) hasFinishedChecking = false;

        _agent.SetDestination(plants[0].transform.position);
    }

    public void SortPlantOrder(out List<GameObject> obj)
    {
        var sorted = _nose.plantsInRange.OrderBy(pair => Vector3.Distance(_alien.transform.position, pair.Key.transform.position));

        Debug.Log(sorted);

        var gameObject = sorted.ToList();

        obj = gameObject.Select(pair => pair.Key).ToList();
    }
}

