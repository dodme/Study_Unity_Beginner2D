using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    float minPos = 0;

    [SerializeField]
    float maxPos = 0;

    GameObject player = null;
    
    public GameObject Player { get { return player; } set { player = value; } }

    float playerXPos = 0;

    private void Update()
    {
        if (player != null)
        {
            playerXPos = Mathf.Clamp(player.transform.position.x, minPos, maxPos);
            transform.position = new Vector3(playerXPos, transform.position.y, transform.position.z);
        }
    }
}
