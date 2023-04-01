using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeviceState : MonoBehaviour
{
    public float X
    {
        get
        {
            return x;
        }
        set
        {
            if (!onlyHorizontal)
                x = Mathf.Clamp(value,
            UniversalConfigParser.GetFloatParam(UniversalConfigParser.GetNodesByTag("appParams"), "min_x"),
            UniversalConfigParser.GetFloatParam(UniversalConfigParser.GetNodesByTag("appParams"), "max_x"));
        }
    }
    public float Y
    {
        get
        {
            return y;
        }
        set
        {
            y = Mathf.Clamp(value,
            UniversalConfigParser.GetFloatParam(UniversalConfigParser.GetNodesByTag("appParams"), "min_y"),
            UniversalConfigParser.GetFloatParam(UniversalConfigParser.GetNodesByTag("appParams"), "max_y"));
        }
    }
    public float Z
    {
        get
        {
            return z;
        }
        set
        {
            if (!onlyHorizontal)
                z = value;
        }
    }

    public float x = 0;
    public float y = 0;
    public float z = 0;


    public bool onlyHorizontal;
    public bool isStatic;

    private void Awake()
    {
        onlyHorizontal = UniversalConfigParser.GetStringParam(UniversalConfigParser.GetNodesByTag("appParams"), "only_horizontal") == "true" ? true : false;
        isStatic = UniversalConfigParser.GetStringParam(UniversalConfigParser.GetNodesByTag("appParams"), "is_static") == "true" ? true : false;
    }

    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Input.gyro.enabled = true;
        Input.gyro.updateInterval = 0.1f;
    }

    float[] xx = new float[10];
    float[] zz = new float[10];

    void Update()
    {
        if (!isStatic)
        {
#if UNITY_EDITOR

#else
        float accX = Input.acceleration.x;
        float accY = Input.acceleration.y;
        float accZ = Input.acceleration.z;

        float x = 180 / Mathf.PI * Mathf.Atan(accZ / accY);
        float z = 180 / Mathf.PI * Mathf.Atan(accX / accY);

        xx[0] = xx[1];
        xx[1] = xx[2];
        xx[2] = xx[3];
        xx[3] = xx[4];
        xx[4] = xx[5];
        xx[5] = xx[6];
        xx[6] = xx[7];
        xx[7] = xx[8];
        xx[8] = xx[9];
        xx[9] = x;

        x = (xx[0] + xx[1] + xx[2] + xx[3] + xx[4] + xx[5] + xx[6] + xx[7] + xx[8] + xx[9]) / 10;

        zz[0] = zz[1];
        zz[1] = zz[2];
        zz[2] = zz[3];
        zz[3] = zz[4];
        zz[4] = zz[5];
        zz[5] = zz[6];
        zz[6] = zz[7];
        zz[7] = zz[8];
        zz[8] = zz[9];
        zz[9] = z;

        z = (zz[0] + zz[1] + zz[2] + zz[3] + zz[4] + zz[5] + zz[6] + zz[7] + zz[8] + zz[9]) / 10;

        X += -Input.gyro.rotationRate.x;
        Y += -Input.gyro.rotationRate.y;
        Z += Input.gyro.rotationRate.z;

        X = (X + x) / 2;
        Z = (Z + z) / 2;
#endif
            transform.eulerAngles = new Vector3(X, Y, Z);
        }
    }
}