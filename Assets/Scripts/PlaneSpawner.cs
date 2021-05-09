using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSpawner : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] Transform planeSpawnPos;
    private Vector3 startPos;


    public void Spawn()
    {
        startPos = planeSpawnPos.transform.position;
        GameObject player = Instantiate(playerPrefab, startPos, Quaternion.identity) as GameObject;
        player.transform.parent = planeSpawnPos.transform;
        //player.transform.parent = gameObject.transform;
    }
}
