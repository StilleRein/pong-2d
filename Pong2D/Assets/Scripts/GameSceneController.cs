﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameSceneController : MonoBehaviour
{
    Text player1Txt, player2Txt, countdownTxt;
    public static Text scoreTxtP1, scoreTxtP2;
    public static GameObject endPanel;
    GameObject countdownPanel, players, pongBall;
    public static bool starting;

    // Start is called before the first frame update
    void Start()
    {
        player1Txt = GameObject.Find("Player1Text").GetComponent<Text>();
        player2Txt = GameObject.Find("Player2Text").GetComponent<Text>();
        countdownTxt = GameObject.Find("CountdownText").GetComponent<Text>();

        player1Txt.text = PlayerPrefs.GetString("player1Name");
        player2Txt.text = PlayerPrefs.GetString("player2Name");
        countdownPanel = GameObject.Find("CountdownPanel");

        players = GameObject.Find("Players");
        pongBall = GameObject.Find("PongBall");

        scoreTxtP1 = GameObject.Find("Score1").GetComponent<Text>();
        scoreTxtP2 = GameObject.Find("Score2").GetComponent<Text>();

        endPanel = GameObject.Find("EndGamePanel");
        endPanel.SetActive(false);

        if(!SceneController.isLocal)
        {
            players.SetActive(false);
            pongBall.SetActive(false);
        }

        scoreTxtP1.text = scoreTxtP2.text = "0";

        StartCoroutine(GameCountdown(3));
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
