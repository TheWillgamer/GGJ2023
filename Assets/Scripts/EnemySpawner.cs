using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //change based on what enemy we use place in prefab
    [SerializeField]
    private GameObject enemy;

    public Transform[] spawnPoints;
    //spawn interval for enemy
    public float spawnRate;

    private float timer;

    void Start()
    {
        timer = 0;
    }

    
    void Update()
    {
        if (timer > spawnRate)
        {
            int randSpawnPoint = Random.Range(0, spawnPoints.Length);
            Instantiate(enemy, spawnPoints[randSpawnPoint].position, Quaternion.identity);
            timer = 0;
            if (spawnRate > 1)
            {
                spawnRate -= .01f;
                Debug.Log(spawnRate);
            }
        }
        timer += Time.deltaTime;
    }

}
