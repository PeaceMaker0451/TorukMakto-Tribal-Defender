using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenuInGameCntrl : MonoBehaviour
{
    public GameManager mainMenuScript;
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;
    public InputField gameSpeedMulty;
    
    void OnEnable()
    {
       musicVolumeSlider.value = mainMenuScript.progress.playerData.musicVolume;
       sfxVolumeSlider.value = mainMenuScript.progress.playerData.SFXVolume;
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
        mainMenuScript.coinSource.volume = sfxVolumeSlider.value;
        mainMenuScript.menuSource.volume = sfxVolumeSlider.value;
        mainMenuScript.player.playerSource.volume = sfxVolumeSlider.value;

    }
}
