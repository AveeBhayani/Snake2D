using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject Food;
    public GameObject Burner;
    private Vector3 foodGridPosition;
    private int WhatToSpawn;

    private void Awake()
    {
        Food = GameObject.Find("FoodG");
        Burner = GameObject.Find("FoodB");
    }

    private void Start()
    {
        SpawnObject();
    }


    public void SpawnObject()
    {
        WhatToSpawn = Random.Range(1, 2);


        switch (WhatToSpawn)
        {
            case 1:
                foodGridPosition = new Vector3(Random.Range(1, 19), Random.Range(1, 19));
                Instantiate(Food, foodGridPosition, Quaternion.identity);
                break;

            case 2:
                foodGridPosition = new Vector3(Random.Range(1, 19), Random.Range(1, 19));
                Instantiate(Burner, foodGridPosition, Quaternion.identity);
                break;

        }
    }
}
