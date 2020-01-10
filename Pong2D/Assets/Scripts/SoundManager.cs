using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioTheme;
    private AudioSource[] audio;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponents<AudioSource>();

        audioTheme.Play();
        DontDestroyOnLoad(this.gameObject);
    }
}
