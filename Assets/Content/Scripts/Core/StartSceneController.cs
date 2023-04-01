using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.Networking;
using System.Runtime.Serialization.Formatters.Binary;

public class StartSceneController : MonoBehaviour
{
    [SerializeField] private Button staticSceneButton, dynamicSceneButton;
    public bool IsStatic, IsDynamic;

    private void Awake()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        staticSceneButton.onClick.RemoveAllListeners();
        staticSceneButton.onClick.AddListener(() => SceneManager.LoadSceneAsync("StaticScene"));
        dynamicSceneButton.onClick.RemoveAllListeners();
        dynamicSceneButton.onClick.AddListener(() => SceneManager.LoadSceneAsync("DynamicScene"));

        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
        }
    }

    private IEnumerator Start()
    {

        var path = Path.Combine(Application.streamingAssetsPath, $"appParams.xml");
        UnityWebRequest www = UnityWebRequest.Get(path);
        yield return www.SendWebRequest();
//        if (string.IsNullOrEmpty(www.error)) Debug.Log(www.error);
        var data = www.downloadHandler.data;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/appParams.xml");
        file.Write(data);

        file.Close();

        if (IsStatic)
        {
            SceneManager.LoadSceneAsync("StaticScene");
        }
        if (IsDynamic)
        {
            SceneManager.LoadSceneAsync("DynamicScene");
        }
    }


    IEnumerator loadXML()
    {
        var path = Path.Combine(Application.streamingAssetsPath, $"appParams.xml");
        UnityWebRequest www = UnityWebRequest.Get(path);
        yield return www.SendWebRequest();
        if (string.IsNullOrEmpty(www.error)) Debug.Log(www.error);
        var data = www.downloadHandler.data;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/appParams.xml");
        file.Write(data);

        file.Close();
    }
}
