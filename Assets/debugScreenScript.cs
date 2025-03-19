using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class debugScreenScript : MonoBehaviour
{
    public MainMenuScript mainMenuScript;
    public Text debugtext;
    void Update()
    {
        debugtext.text = JsonUtility.ToJson(mainMenuScript.progress.playerData);
    }
}
