using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] pirateCoins;
    [SerializeField] Transform spawnPosX;
    
    [SerializeField] float spawnRateMin;
    [SerializeField] float spawnRateMax;
    [SerializeField] float spawnHeightMin;
    [SerializeField] float spawnHeightMax;
    private float spawnTimer;

    public bool isSpawning = false;

    public void Update()
    {
        if (!isSpawning) { return; }
        {
            float spawnRate = Random.Range(spawnRateMin, spawnRateMax);
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnRate)
            {
                Spawn();
                spawnTimer = 0;
            }
  
        }

    }

    public void Spawn()
    {
        Vector2 spawnPos = new Vector2(spawnPosX.transform.position.x, Random.Range(spawnHeightMin,spawnHeightMax));

        GameObject pirateCoin = Instantiate(pirateCoins[Random.Range(0, pirateCoins.Length)], spawnPos, Quaternion.identity);
    }

}



