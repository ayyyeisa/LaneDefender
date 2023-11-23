/*****************************************************************************
// File Name : BulletExplosion.cs
// Author : Isa Luluquisin
// Creation Date : November 22, 2023
//
// Brief Description : This script handles any behaviors for bullet explosions.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BulletExplosion : MonoBehaviour
{
    [Tooltip("Explosion gameobject under player")]
    [SerializeField] private GameObject explosion;

    private bool spaceWasPressed;
    private bool gameIsRunning;

    public Coroutine ExpEffect;

    private void Start()
    {
        gameIsRunning = false;
        spaceWasPressed = false;
    }

    public void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            if (!spaceWasPressed)
            {
                spaceWasPressed = true;
                gameIsRunning = true;
            }
            else if(spaceWasPressed && gameIsRunning)
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
    }
       

    public IEnumerator Explode()
    {
        explosion.SetActive(true);
        yield return new WaitForSeconds(1f);
        explosion.SetActive(false);
        ExpEffect = null;
    }
}
