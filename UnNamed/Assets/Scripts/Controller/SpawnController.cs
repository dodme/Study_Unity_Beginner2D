using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    GameObject player = null;

    private void OnEnable()
    {
        player = Resources.Load<GameObject>("Prefabs/Player");
        player = Instantiate(player, transform.position, Quaternion.identity);
    }
}
