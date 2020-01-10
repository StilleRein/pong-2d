using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Controller : MonoBehaviour
{
    public float speed, topLine, botLine;
    public string axis, isPVP;
    Transform pongBall;

    // Start is called before the first frame update
    void Start()
    {
        isPVP = PlayerPrefs.GetString("isPVP");
        pongBall = GameObject.Find("PongBall").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(isPVP == "true")
            PVPControl();

        else
            CPUControl();
    }

    // The way Player2 move on PVP mode
    void PVPControl()
    {
        float move = Input.GetAxis(axis) * speed * Time.deltaTime;
        float nextPos = transform.position.y + move;

        if (nextPos > topLine || nextPos < botLine)
            move = 0;

        transform.Translate(0, move, 0);
    }

    // The way Player2 move on CPU mode
    void CPUControl()
    {
        if (pongBall.position.x < 0)
        {
            if (transform.position.y != pongBall.position.y)
            {
                float move = 0.35f * speed * Time.deltaTime;

                // check whether the next position would hit the walls or not
                Vector2 nextPos = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, pongBall.position.y), move);

                if (nextPos.y > topLine || nextPos.y < botLine)
                    move = 0;

                //move the object to hit the PongBall
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, pongBall.position.y), move); ;
            }
        }
    }
}
