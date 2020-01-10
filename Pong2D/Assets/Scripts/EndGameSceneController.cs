    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGameSceneController : MonoBehaviour
{
    AudioSource audio;
    public AudioClip hitButtonSound;
    GameObject soundManager;

    // Start is called before the first frame update
    void Start()
    {
        soundManager = GameObject.Find("SoundManager");
        audio = GetComponent<AudioSource>();
    }

     public void ReplayGame()
    {
        audio.PlayOneShot(hitButtonSound);
        Destroy(soundManager);
        SceneManager.LoadScene("Main");
    }

    public void BackToMenu()
    {
        audio.PlayOneShot(hitButtonSound);
        Destroy(soundManager);
        SceneManager.LoadScene("Menu");
    }
}
