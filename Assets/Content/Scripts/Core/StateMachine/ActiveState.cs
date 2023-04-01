using System.Collections.Generic;

public class ActiveState : State
{
    private static ActiveState instance;
    public static ActiveState Instance { get { return instance; } }
    public ActivityTimer timer;
    public static ActiveState Init(ActivityTimer _timer)
    {
        if (instance == null)
        {
            instance = new ActiveState();
            instance.timer = _timer;
            return instance;
        }
        else
            return instance;
    }
    public override void Enter()
    {
        base.Enter();
        timer.CanChangeState = false;
        ScreensViewController.HideWaiting.Invoke(() =>
        {
            ScreensViewController.ShowActive.Invoke();
            timer.CanChangeState = true;
        });
        // Explorer.Instance.LoadScreenView.HideScreen();
        // Explorer.Instance.OpenMainScreenWithHistoryClean(ScreensTags.CATALOGUE_SCREEN_TAG, new Dictionary<string, object>
        //     {
        //         { ParamsNames.CONTENT_STRUCTURE, Structures_Tree._MAIN_ }
        //     });
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
        //timer.IsStarted = false;
    }
}
