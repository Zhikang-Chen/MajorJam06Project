using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlienBrain : MonoBehaviour
{
    private StateMachine _stateMachine = new();

    public PlayerMovement player;
    public Animator anim;

    public NavMeshAgent agent { get; private set; }

    [Header("Base")]
    public float chase_Speed;
    public float crouch_Modifier;

    public SmellDetector AlienNose;

    [Header("Roam State")]
    public List<Transform> patrolPoints;

    [HideInInspector]
    public bool isAttacking = false;


    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        var roamState = new Roam(this);
        var searchState = new Search(this, AlienNose);

        _stateMachine.AddTranistion(searchState, roamState, () => searchState.hasFinishedChecking == true);

        _stateMachine.AddAnyTranistion(searchState, () => AlienNose.PlantInRange == true);

        _stateMachine.SetState(roamState);
    }


    // Update is called once per frame
    void Update()
    {
        _stateMachine.Tick();

        anim.SetFloat("Speed", agent.speed);
        anim.SetBool("Attack", isAttacking);
    }
}
