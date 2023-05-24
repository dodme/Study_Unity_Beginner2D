using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReStartButton : MonoBehaviour
{
    GameObject player = null;
    GameObject spawnPos = null;

    private void Start()
    {
        player = Resources.Load<GameObject>("Prefabs/Player");
        spawnPos = GameObject.Find("@SpawnPos");   
    }

    public void ButtonEvent_ReStart()
    {
        Instantiate(player, spawnPos.transform.position, Quaternion.identity);
        Destroy(transform.parent.gameObject);
    }
}
