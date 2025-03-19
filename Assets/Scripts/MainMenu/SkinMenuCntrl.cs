using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinMenuCntrl : MonoBehaviour
{
    public MainMenuScript mainMenuScript;
    public int selectedSkinID;
    public bool selectedSkinIsToruk;
    public bool selectedSkinIsByAchievement;
    public int selectedSkinCostSumm;
    public Text skinNameText;
    public Text skinOwnerText;
    public Text skinDescriptionText;
    public GameObject applyButton;
    public Button applyButtonComponent;
    public ApplyButtonScript applyButtonScript;
    public GameObject buyButton;
    public Button buyButtonComponent;
    public BuyButtonScript buyButtonScript;
    
    void Start()
    {
        buyButtonComponent = buyButton.GetComponent<Button>();
        applyButtonComponent = applyButton.GetComponent<Button>();
    }
    void OnEnable()
    {
        applyButton.SetActive(false);
        buyButton.SetActive(false);
        skinNameText.text = "";
        skinDescriptionText.text = "";
        skinOwnerText.text = "";
    }
    public void SkinPreView(int skinID, bool isToruk, string skinName, string skinDescription, bool isByAchievement, int costSumm)
    {
        
        selectedSkinID = skinID;
        selectedSkinIsToruk = isToruk;
        selectedSkinIsByAchievement = isByAchievement;
        selectedSkinCostSumm = costSumm;


        mainMenuScript.ChangeSkin(skinID,isToruk);

         skinNameText.text = skinName;
         skinDescriptionText.text = skinDescription;
         if(isToruk)
         skinOwnerText.text = "Облик Торука";
         else
         skinOwnerText.text = "Облик Наездника";

        
         if(isByAchievement)
         {
            buyButtonComponent.interactable = false;
            buyButtonScript.priceText.text = $"Необходимо выполнить достижение \n'{mainMenuScript.achievements.allAchievments[costSumm].achievementName}'";
         }
         else if(!isByAchievement && selectedSkinCostSumm <= mainMenuScript.progress.playerData.playerCoins)
         {
            buyButtonScript.price = selectedSkinCostSumm;
            buyButtonScript.priceText.text = $"{costSumm.ToString()} семян";
            buyButtonComponent.interactable = true;
         }
         else if(!isByAchievement && selectedSkinCostSumm > mainMenuScript.progress.playerData.playerCoins)
         {
            buyButtonScript.price = selectedSkinCostSumm;
            buyButtonScript.priceText.text = $"{costSumm.ToString()} семян";
            buyButtonComponent.interactable = false;
         }


        
         if(isToruk)
        {
            if(!mainMenuScript.progress.playerData.unlockedTorukSkins[skinID])
            {
                applyButton.SetActive(false);
                buyButton.SetActive(true);
            }
            else
            {
                if (skinID == mainMenuScript.progress.playerData.chosenTorukSkin)
                applyButtonComponent.interactable = false;
                else
                applyButtonComponent.interactable = true;

                applyButton.SetActive(true);
                buyButton.SetActive(false);
            }
        }
        else
        {            
            if(!mainMenuScript.progress.playerData.unlockedMaktoSkins[skinID])
            {
                applyButton.SetActive(false);
                buyButton.SetActive(true);
            }
            else
            {
                if (skinID == mainMenuScript.progress.playerData.chosenMaktoSkin)
                applyButtonComponent.interactable = false;
                else
                applyButtonComponent.interactable = true;

                applyButton.SetActive(true);
                buyButton.SetActive(false);
            }
        }
    }

    public void SkinAdd()
    {
        if(selectedSkinIsToruk)
        {
            mainMenuScript.progress.playerData.unlockedTorukSkins[selectedSkinID] = true;
        }
        else
        {
            mainMenuScript.progress.playerData.unlockedMaktoSkins[selectedSkinID] = true;
        }

        buyButton.SetActive(false);
        applyButton.SetActive(true);
        applyButtonComponent.interactable = true;
        
    }

    public void SkinApply()
    {
        if(selectedSkinIsToruk)
        {
            mainMenuScript.progress.playerData.chosenTorukSkin = selectedSkinID;
        }
        else
        {
            mainMenuScript.progress.playerData.chosenMaktoSkin = selectedSkinID;
        }
        mainMenuScript.progress.DataSave("SkinSaver");
    }

}

