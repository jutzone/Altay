using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class StateMachine
{
    private static UnityEvent stateChanged;
    private static State currentState;
    public static State CurrentState
    {
        get
        {
            return currentState;
        }
        set
        {
            if (currentState != value)
            {
                currentState?.Exit();
                Debug.Log(currentState + " state exit");
                currentState = value;
                currentState.Enter();
                Debug.Log(currentState + " state enter");
                stateChanged.Invoke();
            }
        }
    }

    public static void Initialize(StateType startingStateType)
    {
        ActivityTimer timer = ActivityTimer._instance;
        WaitingState.Init(timer);
        ActiveState.Init(timer);
        stateChanged = new UnityEvent();
        stateChanged.AddListener(() => InitTimer(timer));
        CurrentState = startingStateType == StateType.Active ? ActiveState.Instance : WaitingState.Instance;
        // timer.TimerDropped.AddListener(() =>
        // {
        //     WaitingState.Instance.Enter();
        // });
        // timer.TimerStarted.AddListener(() =>
        // {
        //     ActiveState.Instance.Enter();
        // });
        ActiveState.OnEnter = () =>
        {
            //Debug.Log("active state enter");
        };
        WaitingState.OnEnter = () =>
        {
            //Debug.Log("waiting state enter");
        };

    }

    static void InitTimer(ActivityTimer timer)
    {
        timer.TimerDropped.AddListener(() =>
                {
                    WaitingState.Instance.Enter();
                });
        timer.TimerStarted.AddListener(() =>
        {
            ActiveState.Instance.Enter();
        });
    }

    public static void RefreshCurrentState(State newState)
    {

    }
}
