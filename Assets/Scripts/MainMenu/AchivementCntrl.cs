using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchivementCntrl : MonoBehaviour
{
    public int achievementID;
    public GameObject HelpScreen;
    public MainMenuScript mainMenuScript;
    public Achievements achievements;
    public Progress progress;
    public Image thisImage;
    public Text achivementNameText;
    public Text achivementRewardText;
    public Text achivementDescriptionText;
    public Button applyRewardButton;
    public Image applyRewardButtonImage;
    public Text applyButtonText;
    

    void OnEnable()
    {
        achivementNameText.text = mainMenuScript.achievements.allAchievments[achievementID].achievementName;
        achivementDescriptionText.text = mainMenuScript.achievements.allAchievments[achievementID].achievementDescription;
        achivementRewardText.text = $"Награда: {mainMenuScript.achievements.allAchievments[achievementID].achievementRewardDescription}";

        switch(mainMenuScript.achievements.allAchievments[achievementID].achievementStatus)
        {
            case 0:
                applyRewardButton.interactable = false;
                applyButtonText.text = "Награда недоступна";
                applyButtonText.color = new Color(1,1,1,0.5f);
                thisImage.color = new Color(0.48f,0.48f,0.48f,1);
                applyRewardButtonImage.color = new Color(0.48f,0.48f,0.48f,1);
                break;
            case 1:
                applyRewardButton.interactable = true;
                applyButtonText.text = "Получить награду";
                applyButtonText.color = new Color(0.2f,0.2f,0.2f,1);
                thisImage.color = new Color(0.48f,0.48f,0.48f,1);
                applyRewardButtonImage.color = new Color(1f,0.7f,0f,1);
                break;
            case 2:
                applyRewardButton.interactable = false;
                applyButtonText.text = "Награда получена";
                applyButtonText.color = new Color(1,1,1,1);
                thisImage.color = new Color(0.3f,0.4f,0.25f,1);
                applyRewardButtonImage.color = new Color(0.48f,0.48f,0.48f,1);               
                break;

        }
    }
    public void GetReward()
    {
        switch (mainMenuScript.achievements.allAchievments[achievementID].achievementRewardType)
        {
            case 0:
                mainMenuScript.progress.playerData.allCoins += achievements.allAchievments[achievementID].rewardID;
                mainMenuScript.progress.playerData.playerCoins += achievements.allAchievments[achievementID].rewardID;
                mainMenuScript.achievements.allAchievments[achievementID].achievementStatus = 2;                
                break;
            case 1:
                mainMenuScript.progress.playerData.unlockedMaktoSkins[achievements.allAchievments[achievementID].rewardID] = true;
                mainMenuScript.achievements.allAchievments[achievementID].achievementStatus = 2;
                break;
            case 2:
                mainMenuScript.progress.playerData.unlockedTorukSkins[achievements.allAchievments[achievementID].rewardID] = true;
                mainMenuScript.achievements.allAchievments[achievementID].achievementStatus = 2;
                break;
            case 5:
                HelpScreen.SetActive(true);
                mainMenuScript.achievements.allAchievments[achievementID].achievementStatus = 2;
                break;

        }
        OnEnable();
        //progress.DataSave("AchivementSaver");

    }
}
