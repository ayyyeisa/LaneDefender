/*****************************************************************************
// File Name : BulletController.cs
// Author : Isa Luluquisin
// Creation Date : November 21, 2023
//
// Brief Description : This controls the behavior bullet gameobjects.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BulletController : MonoBehaviour
{
    #region Variables
    [Tooltip("Array of sprites that will be used")]
    [SerializeField] private Sprite[] sprites;
    private SpriteRenderer bulletSprite;
    [Tooltip("Rigidbody2D of the bullet")]
    [SerializeField] Rigidbody2D bullet;

    [Header("Variables affecting bullet behavior")]
    [Tooltip("Speed at which bullet moves across screen")]
    [SerializeField] private float speed = 10f;
    [Tooltip("Whether the bullet is heavy or not. Should affect speed as well")]
    [SerializeField] private float mass;

    [Header("GameObject references")]
    [SerializeField] private PlayerController playerInstance;
    [SerializeField] private GameManager gM;

    #endregion

    private void Start()
    {
        playerInstance = GetComponent<PlayerController>();
        gM = FindObjectOfType<GameManager>();
    }

    /// <summary>
    /// Shoots out one heavy bullet when space is tapped. When called, the tag changes to
    /// "heavy bullet" so the system knows that it takes away 3 lives instead of one. 
    /// The appearance, mass, and speed should all resemble the heavy bullet.
    /// </summary>
    public void ShootOnce()
    {
        bulletSprite = GetComponent<SpriteRenderer>();
        bullet = GetComponent<Rigidbody2D>();
        this.gameObject.tag = "HeavyBullet";
        print(this.tag);
        bulletSprite.sprite = sprites[1];
        mass = bullet.mass += 1;
        speed *= 1/bullet.mass;
        SetTrajectory();
    }

    /// <summary>
    /// Rapid fires lighter bullets when space is held down. When called, the bullet remains at
    /// a weight of one. The appearance, mass, and speed should all resemble a lighter bullet.
    /// </summary>
    public void ShootHold()
    {
         bulletSprite = GetComponent<SpriteRenderer>();
         bullet = GetComponent<Rigidbody2D>();
         print(this.tag);
         bulletSprite.sprite = sprites[0];
         mass = bullet.mass;
         speed *= 1/bullet.mass;
         SetTrajectory();
    }

    /// <summary>
    /// Sets the velocity of the bullet to the right * its designated speed
    /// </summary>
    private void SetTrajectory()
    {
        this.bullet.velocity = new Vector2(this.speed, 0);
    }

    /// <summary>
    /// Handles collisions between a bullet and other game objects.
    /// When a bullet hits the right side of the screen or any enemy, it is destroyed.
    /// An explosion effect is also called on when it hits the enemy
    /// </summary>
    /// <param name="collision">the object that collided with the bullet</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "RightBox")
        {
            Destroy(gameObject);
        }
        else if(collision.transform.tag == "Enemy")
        {
            Destroy(gameObject);
            //add explosion effect
        }
    }
}
