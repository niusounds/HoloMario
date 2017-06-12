using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    private AudioSource jumpSound;
    private AudioClip die;
    void Start()
    {
        jumpSound = GetComponent<AudioSource>();
        die = Resources.Load<AudioClip>("die");
    }

    void OnJump()
    {
        jumpSound.Play();
    }

    void OnDie()
    {
        jumpSound.PlayOneShot(die, 1);
    }
}
