/*****************************************************************************
// File Name : EnemySpawner.cs
// Author : Isa Luluquisin
// Creation Date : November 21, 2023
//
// Brief Description : This controls the behavior of enemy spawners.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Tooltip("Array of the 3 different enemy game objects that can be spawned. Each should have an enemycontroller script attached")]
    [SerializeField] private EnemyController[] enemies;
    [Tooltip("Tank gameobject")]
    [SerializeField] PlayerController playerInstance;

    [Header("Varying time range at which enemies can spawn")]
    [SerializeField] private float maxSpawnRate = 5f;
    [SerializeField] private float minSpawnRate = 3f;

    public Coroutine EnemyRef;

    void Update()
    {
        //stops coroutine if game is no longer running. if game is running, enemies are continuously spawning
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

    /// <summary>
    /// Calls on SpawnEnemy() every few seconds.
    /// The amount of time it waits for calling SpawnEnemy() is
    /// randomized between the min and max spawnrates.
    /// </summary>
    /// <returns> time between spawns </returns>
    public IEnumerator StartEnemySpawns()
    {
        float spawnRate = Random.Range(minSpawnRate, maxSpawnRate);
        yield return new WaitForSeconds(spawnRate);
        SpawnEnemy();
        EnemyRef = null;
    }

    /// <summary>
    /// Chooses a random index, which determines which index of the enemy array is spawned.
    /// </summary>
    private void SpawnEnemy()
    {
        //0 is the fast enemy
        //1 is the medium enemy
        //2 is the slow enemy
        int enemyIndex = Random.Range(0, 3);
        Instantiate(enemies[enemyIndex], this.transform.position, this.transform.rotation);

    }

}
