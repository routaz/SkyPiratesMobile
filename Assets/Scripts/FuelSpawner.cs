﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelSpawner: MonoBehaviour
{
    [SerializeField] GameObject[] fuelCans;
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

        GameObject fuelCan = Instantiate(fuelCans[Random.Range(0, fuelCans.Length)], spawnPos, Quaternion.identity);
    }

}



