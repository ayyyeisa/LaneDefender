using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController: MonoBehaviour
{
    [SerializeField] private Rigidbody2D enemy;
    [SerializeField] private float speed;

    [SerializeField] private EnemySpawner spawner;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Rigidbody2D>();
        spawner = GetComponent<EnemySpawner>();
    }

    void Update()
    {
        enemy.velocity = Vector2.left * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            CheckEnemyNum();
            Destroy(gameObject);
            //player loses a life
        }
        else if(collision.transform.tag == "LeftBox")
        {
            CheckEnemyNum();
            Destroy(gameObject);
            //player loses a life
        }

        else if(collision.transform.tag == "Bullet")
        {
            CheckEnemyNum();
            Destroy(gameObject);
            //player gets a point
        }
    }

    private void CheckEnemyNum()
    {
        if (enemy.gameObject.transform.tag == "FastEnemy")
        {
            spawner.enemyCounter[0] -= 1;
        }
        else if (enemy.gameObject.transform.tag == "MediumEnemy")
        {
            spawner.enemyCounter[1] -= 1;
        }
        else if (enemy.gameObject.transform.tag == "SlowEnemy")
        {
            spawner.enemyCounter[2] -= 1;
        }
    }

}
