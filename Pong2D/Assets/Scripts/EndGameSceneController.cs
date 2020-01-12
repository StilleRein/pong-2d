    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGameSceneController : MonoBehaviour
{
    AudioSource audio;
    public AudioClip hitButtonSound;
    GameObject soundManager, replayBtn;

    // Start is called before the first frame update
    void Start()
    {
        soundManager = GameObject.Find("SoundManager");
        replayBtn = GameObject.Find("ReplayBtn");
        audio = GetComponent<AudioSource>();

        if(!SceneController.isLocal)
        {
            replayBtn.SetActive(false);
        }
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

        if(!SceneController.isLocal)
        {
            UINetwork.network.StopHost();
            UINetwork.network.StopClient();
        }

        Destroy(soundManager);
        SceneManager.LoadScene("Menu");
    }
}
