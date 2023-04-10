using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.Networking;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Linq;

public class ConfigChecker : MonoBehaviour
{
    public static ConfigChecker Instance;
    string configPath;
    float timer;
    float delay = 5;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > delay)
        {
            timer = 0;
            CheckConfig();
        }
    }

    public void CheckConfig()
    {
        GetConfig();
    }
    private async void GetConfig()
    {
        // github link config "https://raw.githubusercontent.com/emsmirnov/bodyTracking/main/appParams.xml"
        var path = "https://raw.githubusercontent.com/emsmirnov/bodyTracking/main/appParams.xml";//Path.Combine(Application.streamingAssetsPath, $"appParams.xml");
        UnityWebRequest www = UnityWebRequest.Get(path);
        var result = www.SendWebRequest();
        while (!result.isDone)
        {
            await Task.Yield();
        }
        if (result.webRequest.result == UnityWebRequest.Result.Success)
        {
            var data = www.downloadHandler.data;
            Debug.Log(data[0] + data[1] + data[2] + data.Length + " data");
            var dataToCompare = File.ReadAllBytes(Application.persistentDataPath + "/appParams.xml");
            Debug.Log(dataToCompare[0] + dataToCompare[1] + dataToCompare[2] + dataToCompare.Length + " dataToCompare");
            if (!data.SequenceEqual(dataToCompare))
            {
                FileStream file = File.Create(Application.persistentDataPath + "/appParams.xml");
                file.Write(data);

                file.Close();
                Debug.Log("Config updated");
            }
            else
                Debug.Log("Actual Config");

            delay = UniversalConfigParser.GetFloatParam(UniversalConfigParser.GetNodesByTag("appParams"), "config_timer_delay");
        }
        else
        {
            Debug.Log("Config not updated");
        }
    }
}
