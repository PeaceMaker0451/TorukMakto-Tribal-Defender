using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenuCntrl : MonoBehaviour
{
    public MainMenuScript mainMenuScript;
    public Dropdown inputType;
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;

    public InputField gameSpeedMulty;
    
    void OnEnable()
    {
       inputType.value = mainMenuScript.progress.playerData.inputType;
       musicVolumeSlider.value = mainMenuScript.progress.playerData.musicVolume;
       sfxVolumeSlider.value = mainMenuScript.progress.playerData.SFXVolume;
    }
    public void InputTypeChange()
    {
        mainMenuScript.progress.playerData.inputType = inputType.value;
    }
    public void OnVolumeMusicChange()
    {
        mainMenuScript.progress.playerData.musicVolume = musicVolumeSlider.value;
    }

    public void OnGameSpeedChange()
    {
        mainMenuScript.progress.gameSpeedMultiplier = float.Parse(gameSpeedMulty.text);
    }
    public void OnVolumesfxChange()
    {
        mainMenuScript.progress.playerData.SFXVolume = sfxVolumeSlider.value;
        mainMenuScript.sfxSource.volume = sfxVolumeSlider.value;
        mainMenuScript.achivementSource.volume = sfxVolumeSlider.value;
        mainMenuScript.menuSource.volume = sfxVolumeSlider.value;

    }
    public void ProgressRefresh()
    {
        mainMenuScript.progress.DataLoad("{\"playerCoins\":0,\"allCoins\":0,\"maxDistance\":0.0,\"progressBar\":0,\"unlockedTorukSkins\":[true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false],\"chosenTorukSkin\":0,\"unlockedMaktoSkins\":[true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false],\"chosenMaktoSkin\":0,\"achievementsStatus\":[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],\"musicVolume\":1.0,\"SFXVolume\":1.0,\"inputType\":0,\"newAchivements\":false,\"isFeedBacked\":false}");
        //Progress.DeviceCheck();
        SceneManager.LoadScene("MainMenu");
    }
}
