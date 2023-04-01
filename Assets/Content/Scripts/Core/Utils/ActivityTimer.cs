using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActivityTimer : MonoBehaviour
{
    public UnityEvent TimerDropped, TimerStarted;
    private bool canPlayAction;
    private bool isStarted;
    public bool IsStarted
    {
        get
        {
            return isStarted;
        }
        set
        {
            if (value != isStarted)
            {
                if (value == false)
                    TimerDropped.Invoke();
                else
                    TimerStarted.Invoke();

                isStarted = value;
            }

        }
    }
    public bool CanChangeState;
    public float currentTimer = 0;
    public float TimerDelay;

    private Dictionary<float, System.Action> actionsAtTimes;
    private Dictionary<float, System.Action> playedActions;
    public static ActivityTimer _instance;
    public bool FakeStartingAction;
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            this.TimerDelay = UniversalConfigParser.GetFloatParam(UniversalConfigParser.GetNodesByTag("appParams"), "activity_timer_delay");
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if (IsStarted)
        {
            //if (!IsPaused)
            //{
            currentTimer += Time.deltaTime;
            if (currentTimer >= TimerDelay || VideoPlayerController.VideoEnded)
            {
                if (CanChangeState)
                {
                    Debug.Log("video ended");
                    IsStarted = false;
                    currentTimer = 0;
                    VideoPlayerController.VideoEnded = false;
                }
            }

            if (actionsAtTimes?.Count > 0)
            {
                foreach (var action in actionsAtTimes)
                {
                    if (!playedActions.ContainsKey(action.Key))
                    {
                        if (currentTimer >= action.Key)
                        {
                            if (canPlayAction)
                            {
                                playAction(action);
                            }
                        }
                    }
                }
            }
            //}
        }
        else
        {
            if (Input.gyro.rotationRate.x > UniversalConfigParser.GetFloatParam(UniversalConfigParser.GetNodesByTag("appParams"), "rotationRate_x") ||
            Input.gyro.rotationRate.y > UniversalConfigParser.GetFloatParam(UniversalConfigParser.GetNodesByTag("appParams"), "rotationRate_y") ||
            Input.gyro.rotationRate.z > UniversalConfigParser.GetFloatParam(UniversalConfigParser.GetNodesByTag("appParams"), "rotationRate_z") ||
            FakeStartingAction)
            {
                if (CanChangeState)
                {
                    IsStarted = true;
                    currentTimer = 0;
                    FakeStartingAction = false;
                }
            }
        }
    }

    public void SetActionsDictionary(Dictionary<float, System.Action> _actionsAtTimes)
    {
        foreach (var action in _actionsAtTimes)
        {
            actionsAtTimes.Add(action.Key, action.Value);
        }
    }

    public void RefreshActionsDictionary()
    {
        playedActions.Clear();
    }

    public void ForcedTimerSetup(float time)
    {
        currentTimer = time;
    }

    private void playAction(KeyValuePair<float, System.Action> action)
    {
        canPlayAction = false;
        playedActions.Add(action.Key, action.Value);
        action.Value.Invoke();
        canPlayAction = true;
    }
}
