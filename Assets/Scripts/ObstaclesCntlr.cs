using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesCntlr : MonoBehaviour
{
   
    private float baseSpeed;
    public float obstacleSpeedMultyply;
    private GameObject ObjectWithGameManager;
    private GameManager gameManager;    
    
    void Start()
    {
       
       ObjectWithGameManager = GameObject.Find("EventSystem");
       gameManager = ObjectWithGameManager.GetComponent<GameManager>();
       baseSpeed = gameManager.ObstacleSpeed;
    }
    
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * baseSpeed * obstacleSpeedMultyply);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Killer")
        {
            Destroy(gameObject);
        }
        if(other.gameObject.tag == "Player" && gameObject.tag == "Obstacle")
        {
            gameManager.Fail();
        }
    }
}