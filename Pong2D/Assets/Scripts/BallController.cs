using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class BallController : NetworkBehaviour
{
    public int force;
    [SyncVar (hook = "OnChangeScore1")]
    public int scoreP1;
    [SyncVar (hook = "OnChangeScore2")]
    public int scoreP2;
    Text winnerTxt;
    AudioSource audio;
    public AudioClip hitPlayerSound, hitSidesSound, hitGoalSound;
    public string isPVP;
    public bool doneCounting;
    public static int forceStatic;
    public static Rigidbody2D rigid;
    public static Vector2 direction;
    public static bool isEndGame = false;
    GameObject soundManager;

    // Start is called before the first frame update
    void Start()
    {
        forceStatic = force;

        direction = new Vector2(0, 0).normalized;
        rigid = GetComponent<Rigidbody2D>();

        scoreP1 = scoreP2 = 0;

        audio = GetComponent<AudioSource>();

        isPVP = PlayerPrefs.GetString("isPVP");
    }

    // Update is called once per frame
    void Update()
    {
        if (doneCounting)
        {
            rigid.AddForce(direction * force);
            doneCounting = false;
        }

        if (GameObject.FindGameObjectsWithTag ("Player").Length == 1)
        {
            ClientDisconnect ();
        }
    }

    //Move the ball after countdown
    public static void StartGame()
    {
        direction = new Vector2(2, 0).normalized;
        rigid.AddForce(direction * forceStatic);
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.name == "RightSide" || coll.gameObject.name == "LeftSide")
        {
            audio.PlayOneShot(hitGoalSound);
            StartCoroutine("BallDelay");

            if (coll.gameObject.name == "RightSide")
            {
                scoreP2++;
                direction = new Vector2(2, 0).normalized;
            }
            else
            {
                scoreP1++;
                direction = new Vector2(-2, 0).normalized;
            }

            ResetBall();
        }

        if (coll.gameObject.tag == "Player")
        {
            float angle = (transform.position.y - coll.transform.position.y) * 5f;
            direction = new Vector2(rigid.velocity.x, angle).normalized;
            rigid.velocity = new Vector2(0, 0);
            rigid.AddForce(direction * force * 2);

            audio.PlayOneShot(hitPlayerSound);
        }

        if(coll.gameObject.name == "TopSide" || coll.gameObject.name == "BotSide")
            audio.PlayOneShot(hitSidesSound);

        if(SceneController.isLocal)
            updateScore();

        if(scoreP1 == 5 || scoreP2 == 5)
        {
            if(SceneController.isLocal){
                GameSceneController.endPanel.SetActive(true);
                winnerTxt = GameObject.Find("WinnerText").GetComponent<Text>();

                if (scoreP1 == 5)
                {
                    if(isPVP == "true")
                        winnerTxt.text = "Player 1 Win!";

                    else
                        winnerTxt.text = "You Win!";
                }

                else
                {
                    if (isPVP == "true")
                        winnerTxt.text = "Player 2 Win!";

                    else
                        winnerTxt.text = "You Lose!";
                }
            }

            else {
                if (scoreP1 == 5)
                {
                    RpcEndGame(1);
                }

                else
                {
                    RpcEndGame(2);
                }
            }

            direction = new Vector2(0, 0).normalized;
            SceneManager.LoadScene("EndGame");
            return;
        }
    }

    void ClientDisconnect ()
    {
        UINetwork.network.StopHost();

        if(UINetwork.isInGame)
        {
            soundManager = GameObject.Find("SoundManager");
            Destroy(soundManager);
        }

        SceneManager.LoadScene("Menu");
    }

    [ClientRpc]
    void RpcEndGame(int number)
    {
        GameSceneController.endPanel.SetActive(true);
        winnerTxt = GameObject.Find("WinnerText").GetComponent<Text>();
        winnerTxt.text = "Player " + number + " Win!";
        direction = new Vector2(0, 0).normalized;
        SceneManager.LoadScene("EndGame");
    }

    void ResetBall()
    {
        transform.localPosition = new Vector2(0, 0);
        rigid.velocity = new Vector2(0, 0);
    }

    void updateScore()
    {
        GameSceneController.scoreTxtP1.text = scoreP1 + "";
        GameSceneController.scoreTxtP2.text = scoreP2 + "";
    }

    void OnChangeScore1(int score)
    {
        if (GameSceneController.scoreTxtP1 != null)
            GameSceneController.scoreTxtP1.GetComponent<Text>().text = "" + score;
    }
    void OnChangeScore2(int score)
    {
        if (GameSceneController.scoreTxtP2 != null)
            GameSceneController.scoreTxtP2.GetComponent<Text>().text = "" + score;
    }

    //Delay re launching the ball after hitting the goal
    IEnumerator BallDelay()
    {
        yield return new WaitForSeconds(1);
        doneCounting = true;
    }
}
