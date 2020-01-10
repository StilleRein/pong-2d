using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour
{
    public int force, scoreP1, scoreP2;
    Text scoreTxtP1, scoreTxtP2, winnerTxt;
    GameObject endPanel;
    AudioSource audio;
    public AudioClip hitPlayerSound, hitSidesSound, hitGoalSound;
    public string isPVP;
    public bool doneCounting;
    public static int forceStatic;
    public static Rigidbody2D rigid;
    public static Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        forceStatic = force;

        direction = new Vector2(0, 0).normalized;
        rigid = GetComponent<Rigidbody2D>();

        scoreP1 = scoreP2 = 0;
        scoreTxtP1 = GameObject.Find("Score1").GetComponent<Text>();
        scoreTxtP2 = GameObject.Find("Score2").GetComponent<Text>();

        endPanel = GameObject.Find("EndGamePanel");
        endPanel.SetActive(false);

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

        if (coll.gameObject.name == "Player1" || coll.gameObject.name == "Player2")
        {
            float angle = (transform.position.y - coll.transform.position.y) * 5f;
            direction = new Vector2(rigid.velocity.x, angle).normalized;
            rigid.velocity = new Vector2(0, 0);
            rigid.AddForce(direction * force * 2);

            audio.PlayOneShot(hitPlayerSound);
        }

        if(coll.gameObject.name == "TopSide" || coll.gameObject.name == "BotSide")
            audio.PlayOneShot(hitSidesSound);

        updateScore();

        if(scoreP1 == 5 || scoreP2 == 5)
        {
            endPanel.SetActive(true);
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

            direction = new Vector2(0, 0).normalized;


            SceneManager.LoadScene("EndGame");
            return;
        }
    }

    void ResetBall()
    {
        transform.localPosition = new Vector2(0, 0);
        rigid.velocity = new Vector2(0, 0);
    }

    void updateScore()
    {
        scoreTxtP1.text = scoreP1 + "";
        scoreTxtP2.text = scoreP2 + "";
    }

    //Delay re launching the ball after hitting the goal
    IEnumerator BallDelay()
    {
        yield return new WaitForSeconds(1);
        doneCounting = true;
    }
}
