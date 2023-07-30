using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlienBrain : MonoBehaviour
{
    private StateMachine _stateMachine = new();


    public NavMeshAgent navMeshAgent { get; private set; }

    [Header("Base")]
    public float chase_Speed;
    public SmellDetector AlienNose;

    [Header("Roam State")]
    public List<Transform> patrolPoints;
    private Roam _roamState;

    //Search state
    private Search _searchState;


    public void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        _roamState = new(this);
        _searchState = new(this, AlienNose);

        _stateMachine.AddTranistion(_roamState, _searchState, () => AlienNose.plantsInRange.Count > 0);
        _stateMachine.AddTranistion(_searchState, _roamState, () => _searchState.hasFinishedChecking == true);

        _stateMachine.SetState(_roamState);
    }


    // Update is called once per frame
    void Update()
    {
        _stateMachine.Tick();
    }
}
