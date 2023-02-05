using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //change based on what enemy we use place in prefab
    [SerializeField]
    private GameObject CubePrefabs;

    public Transform[] spawnPoints;
    //spawn interval for enemy
    [SerializeField]
    private float CubeInterval = 3.5f;
    void Start()
    {
        StartCoroutine(spawnEnemy(CubeInterval, CubePrefabs));
    }

    
    void Update()
    {
        //int randSpawnPoint = Random.Range(0, spawnPoints.Length);
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        int randSpawnPoint = Random.Range(0, spawnPoints.Length);
        yield return new WaitForSeconds(interval);
        //creates game objec and chooses spawn range of enemys
        GameObject newEnemy = Instantiate(enemy, spawnPoints[randSpawnPoint].position,Quaternion.identity);
        Debug.Log(randSpawnPoint);
        StartCoroutine(spawnEnemy(interval, enemy));
    }

}
