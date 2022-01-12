using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public bool spawn = true;

    public int firstOffSet;

    public float minSpawnTime;
    public float maxSpawnTime;

    public GameObject sawblade;

    void SawBladeSpawner()
    {
        if (spawn == true)
        {
            InvokeRepeating("SpawnSawBlade", firstOffSet + Random.Range(0f, 2f), Random.Range(minSpawnTime, maxSpawnTime));
        }
    }



    void SpawnSawBlade()
    {
        Instantiate(sawblade, transform.position, transform.rotation);
    }
}
