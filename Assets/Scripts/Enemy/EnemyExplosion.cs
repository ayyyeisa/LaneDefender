/*****************************************************************************
// File Name : EnemyExplosion.cs
// Author : Isa Luluquisin
// Creation Date : November 22, 2023
//
// Brief Description : This script handles any behaviors for enemy explosions.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplosion : MonoBehaviour
{
    [Tooltip("Explosion gameobject under the enemy")]
    [SerializeField] private GameObject explosion;
    [Tooltip("Enemy being affected")]
    [SerializeField] private EnemyController enemy;

    public Coroutine ExpEffect;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Bullet")
        {
            if (ExpEffect == null)
            {
                ExpEffect = StartCoroutine(Explode());
            }
            else
            {
                StopCoroutine(Explode());
            }
        }
        else if (collision.transform.tag == "HeavyBullet")
        {
            if (ExpEffect == null)
            {
                ExpEffect = StartCoroutine(Explode());
            }
            else
            {
                StopCoroutine(Explode());
            }
        }
    }

    public IEnumerator Explode()
    {
        explosion.SetActive(true);
        enemy.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        yield return new WaitForSeconds(3f);
        explosion.SetActive(false);
        enemy.GetComponent<Rigidbody2D>().velocity = Vector2.left * enemy.speed;
        ExpEffect = null;
    }
}
