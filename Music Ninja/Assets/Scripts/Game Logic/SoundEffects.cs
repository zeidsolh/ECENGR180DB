using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundEffects : MonoBehaviour
{

    public static SoundEffects instance;
    public AudioSource audioEffect;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    public void PlayEffect()
    {
        audioEffect.Play();
    }
}
