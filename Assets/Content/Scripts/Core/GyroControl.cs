using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using TMPro;
public class GyroControl : MonoBehaviour
{
    public bool gyroEnabled, fakegyro;
    private Gyroscope gyro;
    [SerializeField] private GameObject cameraContainer, planeParent;
    private Quaternion rot;
    public VideoPlayer vp;
    private bool isPlaying;
    public GameObject waitingPlane;
    public TextMeshProUGUI text;
    void Start()
    {
        // cameraContainer = new GameObject("Camera Container");
        // cameraContainer.transform.position = transform.position;
        // transform.SetParent(cameraContainer.transform);
        //transform.Rotate(90, 0, 0);
        gyroEnabled = EnableGyro();
    }

    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
            //cameraContainer.transform.rotation = Quaternion.Euler(90, 90, 0);
            //rot = new Quaternion(0, 0, 1, 0);

            return true;
        }
        else
        {
            return false;
        }
    }

    private void Update()
    {
        // if (gyroEnabled)
        // {
        //     text.text = (gyro.attitude).eulerAngles.x.ToString() + "  x " + (gyro.attitude).eulerAngles.y.ToString() + "  y " + (gyro.attitude).eulerAngles.z.ToString() + "  z " + rotY((gyro.attitude).eulerAngles.y).ToString() + "  rotY";
        //     transform.rotation = GyroToUnity(gyro.attitude);
        //     // Quaternion.Euler(
        //     //     0,
        //     //     rotY((GyroToUnity(gyro.attitude)).eulerAngles.y),
        //     //     0
        //     // );
             if (gyro.rotationRate.y > 0.1f)
             {
                 if (!isPlaying)
                {
                    planeParent.transform.rotation = transform.rotation;
                    playVideo();
                }
            }
        // }
        // else
        // {
        //     // transform.localRotation = Quaternion.Euler(
        //     //     0,
        //     //     rotY(transform.localRotation.eulerAngles.y),
        //     //     0
        //     // );
        //     //transform.localRotation *= rot;
        // }

        // if (fakegyro)
        // {
        //     if (!isPlaying)
        //     {
        //         planeParent.transform.rotation = transform.rotation;
        //         playVideo();
        //     }
        // }
    }

    Quaternion GyroToUnity(Quaternion quat)
    {
        return new Quaternion(quat.x, quat.z, quat.y, -quat.w);
    }

    void playVideo()
    {
        isPlaying = true;
        vp.Prepare();
        vp.Play();
        waitingPlane.SetActive(false);
        vp.loopPointReached += (s) =>
        {
            fakegyro = false;
            isPlaying = false;
            waitingPlane.SetActive(true);
        };
    }

    float rotY(float rot)
    {

        // if (rot > 28 && rot <= 152)
        //     return 28;
        // if (rot > 152 && rot <= 180)
        //     return 360 - (180 - rot);
        // if (rot > 180 && rot <= 208)
        //     return 180 - rot;
        // if (rot > 208 && rot < 332)
        //     return 28;
        // else
        return rot;
    }
}
