using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostSpawner : MonoBehaviour
{
    public GameObject ShieldBoost, SpeedBoost, ScoreBoost;
    float NextSpawn;
    int WhatToSpawn;
    float SpawnRate = 20f;
    private Vector3 foodGridPosition;

    private void Awake()
    {
        ShieldBoost = GameObject.Find("ShieldBoost");
        ScoreBoost = GameObject.Find("ScoreBoost");
        SpeedBoost = GameObject.Find("SpeedBoost");
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > NextSpawn)
        {
            WhatToSpawn = Random.Range(1, 4);


            switch (WhatToSpawn)
            {
                case 1:
                    foodGridPosition = new Vector3(Random.Range(0, 19), Random.Range(0, 19));
                    Instantiate(ShieldBoost, foodGridPosition, Quaternion.identity);
                    break;

                case 2:
                    foodGridPosition = new Vector3(Random.Range(0, 19), Random.Range(0, 19));
                    Instantiate(SpeedBoost, foodGridPosition, Quaternion.identity);
                    break;

                case 3:
                    foodGridPosition = new Vector3(Random.Range(0, 19), Random.Range(0, 19));
                    Instantiate(ScoreBoost, foodGridPosition, Quaternion.identity);
                    break;
            }

            NextSpawn = Time.time + SpawnRate;
        }
    }
}
