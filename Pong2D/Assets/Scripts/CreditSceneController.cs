using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditSceneController : MonoBehaviour
{
    AudioSource audio;
    public AudioClip hitButtonSound;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void BackToMenu()
    {
        audio.PlayOneShot(hitButtonSound);
        SceneManager.LoadScene("Menu");
    }
}
