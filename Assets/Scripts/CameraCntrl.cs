using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCntrl : MonoBehaviour
{
    public GameObject gameCamera;
    public GameObject player;
    private Vector3 vectorResult;

    // Update is called once per frame
    void Update()
    {
        gameCamera.transform.position = new Vector3(-18, Mathf.Clamp(player.transform.position.y, -25f, 25f) + 5, -16);
    }
}
