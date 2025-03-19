using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;

public class GameManager : MonoBehaviour
{
    public Progress progress;
    public Achievements achievements;
    public PlayerCntlr player;
    public GameObject inputType0;
    public GameObject inputType1;
    private bool isDed;
    public float distance;
    private float maxDistance;
    public int coins; 

    public Text coinText;
    public Text distanceText;
    public Text maxDistanceText;
    public Text endScores;
    public GameObject NEWRECORD;
    public GameObject X2;

    public bool AdIsView = true;
    private bool isPaused;

    public GameObject FailScreen;
    public GameObject pauseMenu;
    public GameObject playerCollider;

    public GameObject[] ObstacleBlockSpawn;
    public GameObject[] cloudsArray;

    public float ObstacleSpeed;
    public float SpawnSpeed;
    public float SpawnSpeedUpMultiple;
    public AudioSource coinSource;
    public AudioSource menuSource;
    public AudioSource sfxSource;
    public AudioSource dedSource;
    public AudioSource musicSource;
    public AudioClip startClip;
    public AudioClip cycleClip;

    private Vector3 MainVector3;    

    public Text spawnSpeedDebugText;
    public Text flySpeedDebugText;


    void Start()
    {
        isPaused = false;
        
        GameObject progressGameObject;
        progressGameObject = GameObject.Find("Progress");
        achievements = progressGameObject.GetComponent<Achievements>();
        progress = progressGameObject.GetComponent<Progress>();
        maxDistance = progress.playerData.maxDistance;
        
        coinSource.volume = progress.playerData.SFXVolume;
        menuSource.volume = progress.playerData.SFXVolume;
        coinText.text = coins.ToString();

        musicSource.clip = startClip;
        musicSource.Play();
        
        switch(progress.playerData.inputType)
        {
            case 0:
                inputType0.SetActive(true);
                inputType1.SetActive(false);
                break;
            // case 1:
            //     inputType1.SetActive(true);
            //     inputType0.SetActive(false);
            //     break;   
            case 2:
                inputType1.SetActive(false);
                inputType0.SetActive(false);
                break;
        }


        StartCoroutine(SpawnObstacles());
        StartCoroutine(SpawnBackgrounds());
        
        sfxSource.volume = progress.playerData.SFXVolume;
        menuSource.volume = progress.playerData.SFXVolume;
        
    }
    

   
    void Update()
    {        
        if(!musicSource.isPlaying)
        {
            musicSource.clip = cycleClip;
            musicSource.Play();
        }
        if(!isDed)
        {
            distance += Time.deltaTime * ObstacleSpeed;
            musicSource.volume = progress.playerData.musicVolume;   
        }
        else
        {
            musicSource.volume = 0;
        }

        distanceText.text = $"Пройденное расстояние - {Mathf.Round(distance).ToString()} М ";
        
        if(maxDistance >= distance)
        maxDistanceText.text = $"До Рекорда - {Mathf.Round(maxDistance - distance).ToString()} М";
        else
        maxDistanceText.text = $"Новый Рекорд! - +{Mathf.Round(distance - maxDistance).ToString()} М";

        if(Input.GetButtonDown("Cancel"))
        {
            isPaused = !isPaused;

            if(isPaused)
            Pause();
            else
            UnPause();
        }

        spawnSpeedDebugText.text = $"Скорость спавна - {SpawnSpeed}";
        flySpeedDebugText.text = $"Скорость полета - {ObstacleSpeed}";
    }

     IEnumerator SpawnObstacles()
    {
       
        Instantiate(ObstacleBlockSpawn[Random.Range(0,9)], MainVector3 = new Vector3(100, 0, 0), Quaternion.Euler(0f, 0f, 0f));
        ObstacleSpeed =  Mathf.Clamp(ObstacleSpeed / (progress.gameSpeedMultiplier), 00.9f, 2900) ; 
        SpawnSpeed = Mathf.Clamp(SpawnSpeed * (progress.gameSpeedMultiplier), 00.3f, 1000);
         yield return new WaitForSeconds(SpawnSpeed);
        Return();
    }
    IEnumerator SpawnBackgrounds()
    {
       
        Instantiate(cloudsArray[Random.Range(0,2)], MainVector3 = new Vector3(500, 0, 0), Quaternion.Euler(0f, 0f, 0f));
        ObstacleSpeed = Mathf.Clamp(ObstacleSpeed / (progress.gameSpeedMultiplier), 9, 29) ; 
        SpawnSpeed = Mathf.Clamp(SpawnSpeed * (progress.gameSpeedMultiplier), 3, 10);
         yield return new WaitForSeconds(SpawnSpeed * 3);
        ReturnBack();
    }

    void Return()
    {
        StartCoroutine(SpawnObstacles());
    }
    void ReturnBack()
    {
        StartCoroutine(SpawnBackgrounds());
    }

    public void Fail()
    {                    
        //achivement 0
        if(achievements.allAchievments[0].achievementStatus == 0)
        achievements.allAchievments[0].achievementStatus = 1;
        isDed = true;
        menuSource.Play();
        player.Dead();
        playerCollider.SetActive(true);
        Invoke("ZeroTime",3);
        FailScreen.SetActive(true);
        endScores.text = $"Расстояние: {Mathf.Round(distance)} \nПредыдущий рекорд: {Mathf.Round(maxDistance)} \nСемян собранно: {coins}";
        if(maxDistance < distance)
        NEWRECORD.SetActive(true);
        else
        NEWRECORD.SetActive(false);
        //ShowRandomAd();
        
    }

    public void ZeroTime()
    {
      Time.timeScale = 0;  
    }

    public void OnRestartButtonClicked()
    {
       
        Time.timeScale = 1;
        SaveGameProgress(); 
        SceneManager.LoadScene("MotrixBird");
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    
    public void UnPause()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        
    }
    public void LoadMenu()
    {
        Time.timeScale = 1;       
        SaveGameProgress(); 
        SceneManager.LoadScene("MainMenu");
        
    }

    public void CoinsAdd()
    {
        coins += 1;
        coinText.text = coins.ToString();
    }
    public void AchievementsCheck()
    {
        //achivement 1
        if(achievements.allAchievments[1].achievementStatus == 0 && progress.playerData.allCoins >= 100)
        achievements.allAchievments[1].achievementStatus = 1;
        //achivement 5
        if(achievements.allAchievments[5].achievementStatus == 0 && progress.playerData.allCoins >= 5000)
        achievements.allAchievments[5].achievementStatus = 1;
        //achivement 6
        if(achievements.allAchievments[6].achievementStatus == 0 && progress.playerData.allCoins >= 10000)
        achievements.allAchievments[6].achievementStatus = 1;
        //achivement 7
        if(achievements.allAchievments[7].achievementStatus == 0 && progress.playerData.allCoins >= 25000)
        achievements.allAchievments[7].achievementStatus = 1;
        //achivement 8
        if(achievements.allAchievments[8].achievementStatus == 0 && progress.playerData.allCoins >= 50000)
        achievements.allAchievments[8].achievementStatus = 1;
        //achivement 4
        if(achievements.allAchievments[4].achievementStatus == 0 && coins >= 451)
        achievements.allAchievments[4].achievementStatus = 1;
        //achivement 9
        if(achievements.allAchievments[9].achievementStatus == 0 && progress.playerData.maxDistance >= 2500)
        achievements.allAchievments[9].achievementStatus = 1;
        //achivement 10
        if(achievements.allAchievments[10].achievementStatus == 0 && progress.playerData.maxDistance >= 7000)
        achievements.allAchievments[10].achievementStatus = 1;
        //achivement 11
        if(achievements.allAchievments[11].achievementStatus == 0 && progress.playerData.maxDistance >= 15000)
        achievements.allAchievments[11].achievementStatus = 1;
        

    }
    public void SaveGameProgress()
    {
        progress.playerData.allCoins += coins;
        progress.playerData.playerCoins += coins;
            
        if(progress.playerData.maxDistance < distance)
        progress.playerData.maxDistance = Mathf.Round(distance);

        AchievementsCheck();
        achievements.AchievementsSave();
        progress.DataSave("GameSaver");

    }
}
