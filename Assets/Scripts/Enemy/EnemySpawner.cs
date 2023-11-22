using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //[SerializeField] private EnemyController enemyPrefab;
    [SerializeField] private EnemyController[] enemies;

    [SerializeField] PlayerController playerInstance;

    [SerializeField] private float maxSpawnRate = 5f;
    [SerializeField] private float minSpawnRate = 3f;

    public Coroutine EnemyRef;

    void Update()
    {
        if(playerInstance.gameIsRunning)
        {
            if(EnemyRef == null)
            {
                EnemyRef = StartCoroutine(StartEnemySpawns());
            }
        }
        if(!playerInstance.gameIsRunning && playerInstance.spaceWasPressed)
        {
            StopAllCoroutines();
        }
    }

    public IEnumerator StartEnemySpawns()
    {
        float spawnRate = Random.Range(minSpawnRate, maxSpawnRate);
        yield return new WaitForSeconds(spawnRate);
        SpawnEnemy();
        EnemyRef = null;
    }

    private void SpawnEnemy()
    {
        //0 is the fast enemy
        //1 is the medium enemy
        //2 is the slow enemy
        int enemyIndex = Random.Range(0, 3);
        Instantiate(enemies[enemyIndex], this.transform.position, this.transform.rotation);

    }

}
