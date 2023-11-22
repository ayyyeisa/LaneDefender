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
    [SerializeField] private Sprite[] sprites;
    private SpriteRenderer bulletSprite;
    [SerializeField] Rigidbody2D bullet;

    [SerializeField] private float speed = 10f;

    [SerializeField] private PlayerController playerInstance;

    #endregion

    private void Start()
    {
        playerInstance = GetComponent<PlayerController>();
    }

    public void shootOnce()
    {
        bulletSprite = GetComponent<SpriteRenderer>();
        bullet = GetComponent<Rigidbody2D>();
        bulletSprite.sprite = sprites[1];
        bullet.mass += 1;
        speed *= 1/bullet.mass;
        SetTrajectory();

    }

    public void shootHold()
    {
         bulletSprite = GetComponent<SpriteRenderer>();
         bullet = GetComponent<Rigidbody2D>();
         bulletSprite.sprite = sprites[0];
         speed *= 1/bullet.mass;
         SetTrajectory();
    }

    private void SetTrajectory()
    {
        this.bullet.velocity = new Vector2(this.speed, 0);
    }

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
            //get points
        }
    }
}
