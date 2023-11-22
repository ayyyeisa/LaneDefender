using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController: MonoBehaviour
{
    [SerializeField] private Rigidbody2D enemy;
    [SerializeField] private float speed;
    [SerializeField] private int lives;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        enemy.velocity = Vector2.left * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            Destroy(enemy.gameObject);
            //player loses a life
        }
        else if(collision.transform.tag == "LeftBox")
        {
            Destroy(gameObject);
            //player loses a life
        }

        else if(collision.transform.tag == "Bullet")
        {
            lives--;
            if(lives <= 0)
            {
                Destroy(enemy.gameObject);
            }
            //player gets a point
        }
    }

}
