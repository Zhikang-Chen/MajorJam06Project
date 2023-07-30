using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlienBrain : MonoBehaviour
{
    private StateMachine _stateMachine = new();

    public PlayerMovement player;
    public NavMeshAgent navMeshAgent { get; private set; }

    [Header("Base")]
    public float chase_Speed;
    public float crouch_Modifier;

    public SmellDetector AlienNose;

    [Header("Roam State")]
    public List<Transform> patrolPoints;



    public void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        var roamState = new Roam(this);
        var searchState = new Search(this, AlienNose);
        var fleeState = new Flee(this);

        _stateMachine.AddTranistion(roamState, searchState, () => AlienNose.ObjectSmelled.Count > 0);
        _stateMachine.AddTranistion(searchState, roamState, () => searchState.hasFinishedChecking == true);

        _stateMachine.AddAnyTranistion(fleeState, () =>
        {
            foreach (var item in AlienNose.ObjectSmelled)
            {
                if (item.GetComponent<SmellAble>().smellType == SMELL_TYPE.Repel)
                {
                    if (Vector3.Distance(transform.position, item.gameObject.transform.position) < 4f) return true;
                }
                 
            }
            return false;
        });

        _stateMachine.SetState(roamState);
    }


    // Update is called once per frame
    void Update()
    {
        _stateMachine.Tick();
    }
}
