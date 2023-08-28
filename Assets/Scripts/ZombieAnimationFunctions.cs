using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimationFunctions : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource _attackSound;
    void Start()
    {
        _attackSound = GetComponent<AudioSource>();
    }

    public void PlayAttackSound()
    {
        _attackSound.PlayOneShot(_attackSound.clip);
    }

}
