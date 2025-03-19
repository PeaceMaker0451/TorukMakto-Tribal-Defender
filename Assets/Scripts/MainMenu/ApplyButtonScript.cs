using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ApplyButtonScript : MonoBehaviour
{
    public SkinMenuCntrl smc;
    public Button buttonComponent;
    
    public void OnPress()
    {
        smc.SkinApply();
        buttonComponent.interactable = false;
    }
}
