using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //[SerializeField] private EnemyController enemyPrefab;
    [SerializeField] private EnemyController[] enemies;
    public int[] enemyCounter = { 0, 0, 0 };
    [SerializeField] private float maxEnemyType = 5;

    [SerializeField] PlayerController playerInstance;

    [SerializeField] private float maxSpawnRate = 3f;
    [SerializeField] private float minSpawnRate = 1f;

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
            StopCoroutine(StartEnemySpawns());
        }
    }

    public IEnumerator StartEnemySpawns()
    {
        yield return new WaitForSeconds(Random.Range(minSpawnRate, maxSpawnRate));
        SpawnEnemy();
        EnemyRef = null;
    }

    private void SpawnEnemy()
    {
        //0 is the fast enemy
        //1 is the medium enemy
        //2 is the slow enemy
        int enemyIndex = Random.Range(0, 3);
        if(enemyIndex == 0)
        {
            enemyCounter[0]++;
        }
        else if(enemyIndex == 1)
        {
            enemyCounter[1]++;
        }
        else if(enemyIndex == 2)
        {
            enemyCounter[2]++;
        }
        for(int i = 0; i < enemyCounter.Length; i++)
        {
            if(enemyCounter[i] >= maxEnemyType)
            {
                break;
            }
            else
            {
                Instantiate(enemies[enemyIndex], this.transform.position, this.transform.rotation);
            }
        }

    }

}
