using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyButtonScript : MonoBehaviour
{
    public int price;
    public Text priceText;
    public Button buttonComponent;
    public MainMenuScript mms;
    public SkinMenuCntrl smc;
    public void OnPress()
    {
        mms.Transaction(price);
        smc.SkinAdd();

    }
}
