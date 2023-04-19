using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.Networking;
using System.Threading.Tasks;
using System.Linq;
using System;

public class ConfigChecker : MonoBehaviour
{
    string configPath;
    float timer;
    float delay = 5;
    private bool timerStarted = false;
    public bool TimerStarted
    {
        get
        {
            return timerStarted;
        }
    }

    private void Update()
    {
        if (timerStarted)
        {
            timer += Time.deltaTime;
            if (timer > delay)
            {
                timer = 0;
                CheckConfig();
            }
        }
    }

    public void CheckConfig()
    {
        GetConfig();
    }
    private async void GetConfig()
    {
        // github link config "https://raw.githubusercontent.com/emsmirnov/bodyTracking/main/appParams.xml"
        var path = "https://uwalk.app/appParams.xml";
        UnityWebRequest www = UnityWebRequest.Get(path);
        var result = www.SendWebRequest();
        while (!result.isDone)
        {
            await Task.Yield();
        }
        if (result.webRequest.result == UnityWebRequest.Result.Success)
        {
            var data = www.downloadHandler.data;

            byte[] dataToCompare = new byte[0];
            try
            {
                dataToCompare = File.ReadAllBytes(Application.persistentDataPath + "/appParams.xml");
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
            if (!data.SequenceEqual(dataToCompare))
            {
                FileStream file = File.Create(Application.persistentDataPath + "/appParams.xml");
                file.Write(data);
                file.Close();

                Debug.Log("Config updated");
                SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
            }
            else
                Debug.Log("Actual Config");

            delay = UniversalConfigParser.GetFloatParam(UniversalConfigParser.GetNodesByTag("appParams"), "config_timer_delay");
        }
        else
        {
            Debug.Log("NO INTERNET. START LOCAL REQUEST");
            var path_local = Path.Combine(Application.streamingAssetsPath, $"appParams.xml");
            UnityWebRequest www_local = UnityWebRequest.Get(path_local);
            var result_local = www_local.SendWebRequest();
            while (!result_local.isDone)
            {
                await Task.Yield();
            }
            if (result.webRequest.result == UnityWebRequest.Result.Success)
            {
                var data = www_local.downloadHandler.data;
                var dataToCompare = File.ReadAllBytes(Application.persistentDataPath + "/appParams.xml");
                if (!data.SequenceEqual(dataToCompare))
                {
                    FileStream file = File.Create(Application.persistentDataPath + "/appParams.xml");
                    file.Write(data);
                    file.Close();

                    Debug.Log("Config local updated");
                }
                else
                    Debug.Log("Actual local Config");

                delay = UniversalConfigParser.GetFloatParam(UniversalConfigParser.GetNodesByTag("appParams"), "config_timer_delay");
            }
        }
        timerStarted = true;
    }
}
