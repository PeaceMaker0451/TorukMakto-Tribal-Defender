using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCntlr : MonoBehaviour
{
    public Progress progress;


    public Rigidbody playerRb;
    public SkinnedMeshRenderer TorukSmr;
    public SkinnedMeshRenderer MaktoSmr;
    public SkinnedMeshRenderer MaktoSubSmr;
    public Animator playerAnimator;
    public Animator playerSitterAnimator;
    public AudioSource playerSource;
    public AudioSource dedSource; 
    public AudioClip upperJumpClip;
    public AudioClip downJumpClip;

    public float upForce;
    public float upVelocity;
    public float delayToJump;
    float delay;
    public bool isDed;
    public GameManager GameManager;

    void Start()
    {
        
        isDed = false;
        progress = GameObject.Find("Progress").GetComponent<Progress>();
        TorukSmr.sharedMesh = progress.TorukSkinMeshes[progress.playerData.chosenTorukSkin];
        TorukSmr.material = progress.torukMaktoSkinMaterials[progress.playerData.chosenTorukSkin];
        MaktoSmr.sharedMesh = progress.MaktoSkinMeshes[progress.playerData.chosenMaktoSkin];
        MaktoSmr.material = progress.torukMaktoSkinMaterials[progress.playerData.chosenMaktoSkin];
        MaktoSubSmr.sharedMesh = progress.MaktoSkinSubMeshes[progress.playerData.chosenMaktoSkin];
        MaktoSubSmr.material = progress.torukMaktoSkinMaterials[progress.playerData.chosenMaktoSkin];
        
        playerSource.volume = progress.playerData.SFXVolume;


        delay = delayToJump;
        playerRb.velocity = new Vector3(0,upVelocity*2,0);
    }

    
    void Update()
    {
        if(!isDed)
        {    
           if(delay > 0)
            delay -= Time.deltaTime;
            if(Input.GetButtonDown("Vertical"))
            {
                if(Input.GetAxis("Vertical")> 0)
                UpperJump();
                else if(Input.GetAxis("Vertical")< 0)
                LowerJump();
            }
            
            
        
            
        }    


    }

    public void UpperJump()
    {
         
            if(delay <= 0)
            {
                if(playerRb.velocity.y < 0)
                playerRb.velocity = new Vector3(0,upVelocity,0);

                playerRb.AddForce(transform.up * upForce, ForceMode.Impulse);
                playerSitterAnimator.SetTrigger("Jump");
                playerAnimator.SetTrigger("Jump");
                delay = delayToJump;
                playerSource.clip = upperJumpClip;
                playerSource.Play();
            }
            else if(delay <= (delayToJump / 1.2))
            {
                playerRb.AddForce(transform.up * upForce * 1.5f, ForceMode.Impulse);
                playerSource.clip = upperJumpClip;
                playerSource.Play();
            }
    }
    
    public void LowerJump()
    {
        if (delay <= 0)
            {
                playerRb.AddForce(transform.up * upForce * -1, ForceMode.Impulse);
                playerAnimator.SetTrigger("DownJump");
                delay = delayToJump;
                playerSource.clip = downJumpClip;
                playerSource.Play();
            }
    }
    public void Dead()
    {
        isDed = true;
        playerRb.constraints = RigidbodyConstraints.None;
        playerRb.velocity = new Vector3(Random.Range(-20,0),Random.Range(-20,20),Random.Range(-20,20));
        playerRb.angularVelocity = new Vector3(Random.Range(-20,0),Random.Range(-20,20),Random.Range(-20,20));
        dedSource.volume = progress.playerData.SFXVolume;
        dedSource.Play();

    }
}
