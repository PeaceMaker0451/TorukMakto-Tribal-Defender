using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCntrl : MonoBehaviour
{
    public AudioSource sfx;
    public GameManager gameManager;
    void Start()
    {
        GameObject progressGameObject;
        progressGameObject = GameObject.Find("EventSystem");
        gameManager = progressGameObject.GetComponent<GameManager>();
        sfx.volume = gameManager.progress.playerData.SFXVolume * 0.4f;
        Invoke("Play",(gameManager.SpawnSpeed * 1.25f));
    }
    void Play()
    {
        sfx.Play();
    }
}
