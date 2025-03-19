using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SkinInfo
{
    public string skinName;
    public string skinDescription;
    public bool isToruk;
    public int skinID;
    public bool isByAchievement;
    public int costSumm;
}
public class ChangeSkinButtonScript : MonoBehaviour
{
    
    public SkinInfo currentSkin;
    public SkinMenuCntrl skinMenuCntrl;

    
    void Awake()
    {

    }

    public void OnPress()
    {
         skinMenuCntrl.SkinPreView(currentSkin.skinID,currentSkin.isToruk,currentSkin.skinName,currentSkin.skinDescription,currentSkin.isByAchievement,currentSkin.costSumm);
    }
}
