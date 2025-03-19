using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerData
{
    public int playerCoins;
    public int allCoins;
    public float maxDistance;
    public int progressBar;
    public bool[] unlockedTorukSkins;
    public int chosenTorukSkin;
    public bool[] unlockedMaktoSkins;
    public int chosenMaktoSkin;
    public int[] achievementsStatus;
    public float musicVolume;
    public float SFXVolume;
    public int inputType;
    public bool newAchivements;
    public bool isFeedBacked;
}

public class Progress : MonoBehaviour
{
    public static Progress instance;

    // [DllImport("__Internal")]
    // private static extern void SaveExt(string data);

    // [DllImport("__Internal")]
    // public static extern void LoadExt();

    // [DllImport("__Internal")]
    // public static extern void DeviceCheck();

    // [DllImport("__Internal")]
    // public static extern void SignInCheck();

    // [DllImport("__Internal")]
    // private static extern void SetToLeaderBoardDistance(int value);

    // [DllImport("__Internal")]
    // private static extern void SetToLeaderBoardTotalCoins(int value);
    public bool isSignedIn = true;
    public bool OffFeedBackInSession;
    public PlayerData playerData;
    public Material[] torukMaktoSkinMaterials;
    public Mesh[] TorukSkinMeshes;
    public Mesh[] MaktoSkinMeshes;
    public Mesh[] MaktoSkinSubMeshes;

    public float gameSpeedMultiplier;

    // public Texture yaPlayerImage;
    // public string yaPlayerName;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
        //LoadExt();


    }
    
    void Start()
    {
        //SignInCheck();

        //Invoke("ProgressCheck", 1.5f);
        //DeviceCheck();
    }

    public void ProgressCheck()
    {
        if(playerData.progressBar == 1)
        {
            playerData.progressBar = 3;
            this.gameObject.GetComponent<Achievements>().allAchievments[2].achievementStatus = 1;
            this.gameObject.GetComponent<Achievements>().AchievementsSave();
        }
    }
    // public void isSignedInWriteFalse()
    // {
    //     isSignedIn = false;
    //     //SceneManager.LoadScene("MainMenu");
    //     //Debug.Log("Вход не выполнен");
    //     GameObject.Find("EventSystem").SendMessage("LoginCheck");

    // }
    public void DataSave(string owner)
    {
        string dataInJson = JsonUtility.ToJson(playerData);
        //SaveExt(dataInJson);
        //SetToLeaderBoardDistance((int)playerData.maxDistance);
        //SetToLeaderBoardTotalCoins(playerData.allCoins);
    }

    public void DataLoad(string data)
    {
        if (data != "{}" && data != "{ }" )
        playerData = JsonUtility.FromJson<PlayerData>(data);
        this.gameObject.GetComponent<Achievements>().AchievementsLoad();
    }

    public void DeviceWriteDesktop()
    {
        playerData.inputType = 2;
        Debug.Log("InputType is 2");
    }

    public void DeviceWriteMobile()
    {
        playerData.inputType = 0;
        Debug.Log("InputType is 0");
    }

    
}
