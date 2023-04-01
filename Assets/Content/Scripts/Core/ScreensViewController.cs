using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreensViewController : MonoBehaviour
{
    [SerializeField] private ActiveScreenController activeScreenController;
    [SerializeField] private WaitingScreenController waitingScreenController;

    public delegate void VoidDelegate(System.Action action = null);
    public static VoidDelegate ShowActive, ShowWaiting, HideActive, HideWaiting;

    private void Awake()
    {
        ShowActive = (s) => activeScreenController.ShowScreen(s);
        ShowWaiting = (s) => waitingScreenController.ShowScreen(s);
        HideActive = (s) => activeScreenController.HideScreen(s);
        HideWaiting = (s) => waitingScreenController.HideScreen(s);
    }

    private void Start()
    {
        StateMachine.Initialize(StateType.Waiting);
    }

}
