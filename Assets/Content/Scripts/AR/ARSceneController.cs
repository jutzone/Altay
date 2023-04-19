using System.Collections;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;

public class ARSceneController : MonoBehaviour
{
    [SerializeField] private ConfigChecker configChecker;
    [SerializeField] private CharactersController charactersController;
    [SerializeField] private GameObject charactersParent;

    private void Awake()
    {
        Screen.autorotateToLandscapeLeft = false;
        Screen.autorotateToLandscapeRight = false;
        Screen.orientation = ScreenOrientation.Portrait;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        Input.compass.enabled = true;
    }

    private IEnumerator Start()
    {
        configChecker.CheckConfig();
        charactersParent.transform.position = Camera.main.transform.position;

        yield return new WaitUntil(() => configChecker.TimerStarted);
        charactersController.Initialize();
        charactersParent.transform.rotation = Quaternion.Euler(0,
         Camera.main.transform.rotation.eulerAngles.y + Input.compass.magneticHeading + UniversalConfigParser.GetFloatParam(UniversalConfigParser.GetNodesByTag("appParams"), "magnetic_heading_offset"),
          0);
    }
}
