using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCntrl : MonoBehaviour
{
    public GameManager gameManager;
    public AudioClip[] coinClips;

    void Start()
    {
       gameManager = GameObject.Find("EventSystem").GetComponent<GameManager>();
    }
    
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "CoinTrigger")
        {
            gameManager.coinSource.clip = coinClips[Random.Range(0,2)];
            gameManager.coinSource.Play();
            gameManager.CoinsAdd();
            Destroy(gameObject);
        }
    }
}
