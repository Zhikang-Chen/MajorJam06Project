using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private IState _currentState;

    private Dictionary<Type, List<Transition>> _transition = new Dictionary<Type, List<Transition>>();

    //Current State Transtions
    private List<Transition> _currenTransition = new List<Transition>();

    //Transition from Any State
    private List<Transition> _anyTransition = new List<Transition>();

    public void Tick()
    {
        var t = GetTransition();
        //Check if there is a state to tranition to
        if (t != null)
            SetState(t.transitionTo);

        //Call State update func
        _currentState?.tick();
    }

    public void AddTranistion(IState from, IState to, Func<bool> Condition)
    {
        //If this state is not initliazed in the dictionary
        if(_transition.TryGetValue(from.GetType(), out var t) == false)
        {
            //Creating a list of all transition for this state
            t = new List<Transition>();

            //Catalog the State Transition list to the dictionary
            _transition[from.GetType()] = t;
        }

        //Add Tranistion to list
        t.Add(new Transition(Condition, to));
    }

    public void AddAnyTranistion(IState to, Func<bool> Condition)
    {
        _anyTransition.Add(new Transition(Condition, to));
    }

    public void SetState(IState state)
    {
        //Check if we're setting to active state
        if (_currentState == state)
            return;

        //Call exit State
        _currentState?.Exit();
        //Set new state
        _currentState = state;

        //Update to State transitions
        _transition.TryGetValue(_currentState.GetType(), out _currenTransition);

        //Call Enter State
        _currentState?.Enter();
    }

    private Transition GetTransition()
    {
        foreach(var t in _currenTransition)
        {
            if (t.Condtion())
                return t;
        }

        foreach(var t in _anyTransition)
        {
            if (t.Condtion())
                return t;
        }

        return null;
    }

    private class Transition
    {
        public Func<bool> Condtion { get; }
        public IState transitionTo { get; }

        public Transition(Func<bool> condtion, IState transitionTo)
        {
            Condtion = condtion;
            this.transitionTo = transitionTo;
        }
    }
}
