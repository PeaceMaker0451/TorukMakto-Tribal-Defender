using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public Progress progress;
    public PluginCntrl pluginCntrl;
    public Achievements achievements;

    //[System.Runtime.InteropServices.DllImport("__Internal")]
    //private static extern void playerInfoExt();

    // [System.Runtime.InteropServices.DllImport("__Internal")]
    // public static extern void RateGameExt();

    // [System.Runtime.InteropServices.DllImport("__Internal")]
    // public static extern void StickyBannerOff();

    // [System.Runtime.InteropServices.DllImport("__Internal")]
    // public static extern void SignUpExt();
    public Text maxDistanceText;
    public Text allCoinsText;
    public Text playerCoinsText;
    public GameObject tutorialScreen;
    [SerializeField] private GameObject feedBackScreen;
    [SerializeField] private GameObject feedBackButton;
    [SerializeField] private GameObject veryHelp;
    [SerializeField] private GameObject SignUpButton;
    public AudioSource sfxSource;
    public AudioSource achivementSource;
    public AudioSource menuSource;
    public AudioSource musicSource;

    // public RawImage playerPicImage;
    // public Text playerNameText;

    public GameObject newAchivementsImage1;
    public GameObject newAchivementsImage2;


    public SkinnedMeshRenderer TorukSmr;
    public SkinnedMeshRenderer MaktoSmr;
    public SkinnedMeshRenderer MaktoSubSmr;
    void Start()
    {
        
        GameObject progressGameObject;
        progressGameObject = GameObject.Find("Progress");
        achievements = progressGameObject.GetComponent<Achievements>();
        progress = progressGameObject.GetComponent<Progress>();
        pluginCntrl = progressGameObject.GetComponent<PluginCntrl>();
        //pluginCntrl.mainMenuScript

        // if(progress.isSignedIn == false)
        // {
        //     SignUpButton.SetActive(true);
        //     feedBackButton.SetActive(false);
        // }
        // else
        // {
        //     SignUpButton.SetActive(false);
        //     feedBackButton.SetActive(true);
        // }

        // if(progress.playerData.isFeedBacked)
        // feedBackButton.SetActive(false);

        // if(progress.OffFeedBackInSession)
        // feedBackButton.SetActive(false);
        
        // playerPicImage.texture = progress.yaPlayerImage;
        // playerNameText.text = progress.yaPlayerName;
        
        sfxSource.volume = progress.playerData.SFXVolume;
        achivementSource.volume = progress.playerData.SFXVolume;
        menuSource.volume = progress.playerData.SFXVolume;

        
        foreach (int i in progress.playerData.achievementsStatus)
        {
            if (i == 1)
            {
                newAchivementsImage1.SetActive(true);
                newAchivementsImage2.SetActive(true);
                break;
            }
        
        }
        TorukSmr.sharedMesh = progress.TorukSkinMeshes[progress.playerData.chosenTorukSkin];
        TorukSmr.material = progress.torukMaktoSkinMaterials[progress.playerData.chosenTorukSkin];
        MaktoSmr.sharedMesh = progress.MaktoSkinMeshes[progress.playerData.chosenMaktoSkin];
        MaktoSmr.material = progress.torukMaktoSkinMaterials[progress.playerData.chosenMaktoSkin];
        MaktoSubSmr.sharedMesh = progress.MaktoSkinSubMeshes[progress.playerData.chosenMaktoSkin];
        MaktoSubSmr.material = progress.torukMaktoSkinMaterials[progress.playerData.chosenMaktoSkin];

    }  
    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }  
    void Update()
    {
        playerCoinsText.text = $"У вас есть {progress.playerData.playerCoins.ToString()}";
        allCoinsText.text = $"Всего семян заработано - {progress.playerData.allCoins.ToString()}";
        maxDistanceText.text = $"Рекорд расстояния - {progress.playerData.maxDistance.ToString()}";
        musicSource.volume = progress.playerData.musicVolume;
    }
    
    public void PlayButton()
    {
       if(progress.playerData.progressBar == 0)
       {
            tutorialScreen.SetActive(true);
            progress.playerData.progressBar = 1;
       }
       else if(progress.playerData.progressBar == 3)
        {
            progress.playerData.progressBar = 4;
            achievements.allAchievments[2].achievementStatus = 1;
            achievements.AchievementsSave();
            Time.timeScale = 1;
            SceneManager.LoadSceneAsync("MotrixBird");          
        }
        else
       {
            Time.timeScale = 1;
            SceneManager.LoadSceneAsync("MotrixBird");
       }
       
    }

    public void Transaction(int price)
    {
         int currentCoins;
         currentCoins = progress.playerData.playerCoins; 
         currentCoins -= price;
         progress.playerData.playerCoins = currentCoins;
         progress.DataSave("TransactionSaver");
    }

    public void SaveProgress()
    {
        //progress.playerData.allCoins = allCoins;
        //progress.playerData.playerCoins = playerCoins;
        achievements.AchievementsSave();
        progress.DataSave("MainMenuProgressSaver");
    }

    public void ChangeSkin(int skinID, bool isToruk)
    {
        if(isToruk)
        {
            TorukSmr.sharedMesh = progress.TorukSkinMeshes[skinID];
            TorukSmr.material = progress.torukMaktoSkinMaterials[skinID];

            MaktoSmr.sharedMesh = progress.MaktoSkinMeshes[progress.playerData.chosenMaktoSkin];
            MaktoSmr.material = progress.torukMaktoSkinMaterials[progress.playerData.chosenMaktoSkin];
            MaktoSubSmr.sharedMesh = progress.MaktoSkinSubMeshes[progress.playerData.chosenMaktoSkin];
            MaktoSubSmr.material = progress.torukMaktoSkinMaterials[progress.playerData.chosenMaktoSkin];
        }
        else
        {
            MaktoSmr.sharedMesh = progress.MaktoSkinMeshes[skinID];
            MaktoSmr.material = progress.torukMaktoSkinMaterials[skinID];
            MaktoSubSmr.sharedMesh = progress.MaktoSkinSubMeshes[skinID];
            MaktoSubSmr.material = progress.torukMaktoSkinMaterials[skinID];

            TorukSmr.sharedMesh = progress.TorukSkinMeshes[progress.playerData.chosenTorukSkin];
            TorukSmr.material = progress.torukMaktoSkinMaterials[progress.playerData.chosenTorukSkin];
        }
    }

    public void InputTypeChoose(int InputType)
    {
        progress.playerData.inputType = InputType;
    }

    public void Quit()
    {
        if (progress.playerData.progressBar == 1)
        progress.playerData.progressBar = 2;
        SaveProgress();
        Application.Quit();
    }
}
