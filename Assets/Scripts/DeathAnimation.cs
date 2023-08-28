using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAnimation : MonoBehaviour
{
    public AudioSource _death;
    private void Start()
    {
        _death = GetComponent<AudioSource>();
    }

    public void DeathSound()
    {
        _death.Play();
    }
}
