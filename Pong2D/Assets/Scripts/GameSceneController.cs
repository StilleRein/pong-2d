using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSceneController : MonoBehaviour
{
    Text player1Txt, player2Txt, countdownTxt;
    AudioSource audio;
    public AudioClip hitButtonSound;
    GameObject countdownPanel;
    public static bool starting;

    // Start is called before the first frame update
    void Start()
    {
        player1Txt = GameObject.Find("Player1").GetComponent<Text>();
        player2Txt = GameObject.Find("Player2").GetComponent<Text>();
        countdownTxt = GameObject.Find("CountdownText").GetComponent<Text>();

        player1Txt.text = PlayerPrefs.GetString("player1Name");
        player2Txt.text = PlayerPrefs.GetString("player2Name");

        audio = GetComponent<AudioSource>();

        countdownPanel = GameObject.Find("CountdownPanel");

        StartCoroutine(GameCountdown(3));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReplayGame()
    {
        audio.PlayOneShot(hitButtonSound);
        SceneManager.LoadScene("Main");
    }

    public void BackToMenu()
    {
        audio.PlayOneShot(hitButtonSound);
        SceneManager.LoadScene("Menu");
    }

    IEnumerator GameCountdown(int seconds)
    { 
        int time = seconds;

        //display 3, 2, 1 count down
        while (time > 0)
        {
            countdownTxt.text = Mathf.Round(time) + "";

            yield return new WaitForSeconds(1);
            time--;
        }

        //display Start text
        while (time > -1)
        {
            countdownTxt.text = "Start!";

            yield return new WaitForSeconds(1);
            time--;
        }

        //hide countdown panel and start the game (moving the ball)
        while(time > -2)
        {
            countdownPanel.SetActive(false);
            BallController.StartGame();
            yield return new WaitForSeconds(1);
            time--;
        }
    }
}
