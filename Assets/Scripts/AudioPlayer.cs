using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{

    private AudioSource Audio;

    void Start()
    {
        Audio = GetComponent<AudioSource>();
        Audio.loop = true;
        Audio.Play();
    }
}
