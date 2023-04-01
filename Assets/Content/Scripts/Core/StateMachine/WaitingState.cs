using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingState : State
{
    private static WaitingState instance;
    public static WaitingState Instance { get { return instance; } }
    public ActivityTimer timer;
    //* Нужно добавить делегаты и инвоукать их в функции
    public static WaitingState Init(ActivityTimer _timer)
    {
        if (instance == null)
        {
            instance = new WaitingState();
            instance.timer = _timer;
            return instance;
        }
        return instance;
    }
    public override void Enter()
    {
        base.Enter();
        //timer.IsStarted = false;
        timer.CanChangeState = false;
        ScreensViewController.HideActive.Invoke(() =>
        {
            ScreensViewController.ShowWaiting.Invoke();
            timer.CanChangeState = true;
        });
        // Explorer.Instance.LoadScreenView.ShowScreen();
    }

    public override void ContentLoading()
    {
        base.ContentLoading();
    }

    public override void LayoutUpdate()
    {
        base.LayoutUpdate();
    }

    public override void ContentRelease()
    {
        base.ContentRelease();
    }

    public override void Exit()
    {
        base.Exit();
        //timer.IsStarted = true;
    }


}
