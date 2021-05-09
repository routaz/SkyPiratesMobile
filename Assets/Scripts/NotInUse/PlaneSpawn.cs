using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSpawn : MonoBehaviour
{
    public Vector3 startPos;
    public GameObject prefab;

    private void OnEnable()
    {
        Spawn();
    }

    public void Update()
    {

    }

    // Start is called before the first frame update

    public void Spawn()
    {
        GameObject player = Instantiate(prefab, startPos, Quaternion.identity);
        player.transform.parent = gameObject.transform;

    }

}

