using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleTouch : MonoBehaviour
{
    public GameObject player1, player2;
    float distance, player1CurrPos, player2CurrPos;
    int LeftFinId, RightFinId;
    Vector2 leftTouchPos, rightTouchPos;
    public string isPVP;

    // Start is called before the first frame update
    void Start()
    {
        LeftFinId = -1;
        RightFinId = -1;

        player1CurrPos = player1.transform.position.x;
        player2CurrPos = player2.transform.position.x;

        isPVP = PlayerPrefs.GetString("isPVP");
    }

    // Update is called once per frame
    void Update()
    {
        if(isPVP == "true")
        {
            if (Input.touchCount > 0)
            {
                foreach (Touch touch in Input.touches)
                {
                    //Right Screen (player 1 control)
                    if (touch.phase  == TouchPhase.Began && touch.position.x > Screen.width / 2 && RightFinId == -1)
                        RightFinId = touch.fingerId;

                    if (touch.fingerId == RightFinId && touch.position.x > Screen.width / 2)
                    {
                        rightTouchPos = touch.position;
                        if (rightTouchPos.x > Screen.width / 2)
                        {
                            if(touch.phase == TouchPhase.Moved)
                                player1.transform.position = getTouchPosition(rightTouchPos);
                        }
                    }

                    //Left Screen (Player 2 control)
                    if (touch.phase == TouchPhase.Began && touch.position.x < Screen.width / 2 && LeftFinId == -1)
                        LeftFinId = touch.fingerId;

                    if (touch.fingerId == LeftFinId && touch.position.x < Screen.width / 2)
                    {
                        leftTouchPos = touch.position;

                        if (leftTouchPos.x <= Screen.width / 2)
                        {
                            if(touch.phase == TouchPhase.Moved)
                                player2.transform.position = getTouchPosition(leftTouchPos);
                        }
                    }

                    if (touch.phase == TouchPhase.Ended)
                    {
                        if(touch.fingerId == LeftFinId)
                        LeftFinId = -1;

                        else if(touch.fingerId == RightFinId)
                            RightFinId = -1;
                    }
                }
            }
        }
    }

    protected void LateUpdate()
    {
        if(isPVP == "true")
        {
            Vector3 positionP1 = player1.transform.position;
            positionP1.y = Mathf.Clamp(positionP1.y, -3.44f, 3.44f);
            positionP1.x = Mathf.Clamp(positionP1.x, player1CurrPos, player1CurrPos);
            player1.transform.position = positionP1;

            Vector3 positionP2 = player2.transform.position;
            positionP2.y = Mathf.Clamp(positionP2.y, -3.44f, 3.44f);
            positionP2.x = Mathf.Clamp(positionP2.x, player2CurrPos, player2CurrPos);
            player2.transform.position = positionP2;
        }
    }

    Vector2 getTouchPosition(Vector2 touchPosition){
        return GetComponent<Camera>().ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, transform.position.z));;
    }
}
