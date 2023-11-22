using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController: MonoBehaviour
{
    [SerializeField] private Rigidbody2D enemy;
    [SerializeField] private float speed;
    [SerializeField] private int lives;

    [SerializeField] private GameManager gM;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Rigidbody2D>();
        gM = GetComponent<GameManager>();
    }
    
    void Update()
    {
        enemy.velocity = Vector2.left * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            Destroy(enemy.gameObject);
            gM.PlayerDied();
        }
        else if (collision.transform.tag == "LeftBox")
        {
            Destroy(gameObject);
            gM.PlayerDied();
        }

        else if (collision.transform.tag == "Bullet")
        {
            lives--;
            if (lives <= 0)
            {
                Destroy(enemy.gameObject);
                gM.UpdateScore();
            }
        }
        else if(collision.transform.tag == "HeavyBullet")
        {
            lives -= 3;
            if (lives <= 0)
            {
                Destroy(enemy.gameObject);
                gM.UpdateScore();
            }
        }
    }
}
