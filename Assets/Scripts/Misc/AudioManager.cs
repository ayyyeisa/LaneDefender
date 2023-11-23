/*****************************************************************************
// File Name : AudioManager.cs
// Author : Isa Luluquisin
// Creation Date : November 22, 2023
//
// Brief Description : This manages the audio sources and clips that will be played.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("--------- Audio Sources ----------")]
    [Tooltip("Background music")]
    [SerializeField] private AudioSource musicSource;
    [Tooltip("SFX object")]
    [SerializeField] private AudioSource sfxSource;

    [Header("--------- SFX Clips ----------")]
    [Tooltip("Played when player shoots a bullet")]
    public AudioClip TankShoot;
    [Tooltip("Played when a player or left side gets hit by enemy")]
    public AudioClip LifeLost;
    [Tooltip("Played when an enemy gets hit")]
    public AudioClip EnemyHit;
    [Tooltip("Played when an enemy dies")]
    public AudioClip EnemyDeath;

    // Start is called before the first frame update
    void Start()
    {
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
