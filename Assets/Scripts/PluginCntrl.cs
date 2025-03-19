using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;

public class PluginCntrl : MonoBehaviour
{
    
    public MainMenuScript mainMenuScript;
    public Progress progress;

    [DllImport("__Internal")]
    public static extern void HelloStringExt(string str);


    public void TimeScaleZero()
    {
        AudioListener.pause = true;
    }

    public void TimeScaleDefault()
    {
        AudioListener.pause = false;
    }
}
