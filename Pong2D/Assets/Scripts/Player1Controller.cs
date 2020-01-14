using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Controller : MonoBehaviour
{
    public float speed, topLine, botLine;
    public string axis;

    // Update is called once per frame
    void Update()
    {
        float move = Input.GetAxis(axis) * speed * Time.deltaTime;
        float nextPos = transform.position.y + move;

        if (nextPos > topLine || nextPos < botLine)
            move = 0;

        transform.Translate(0, move, 0);
    }
}
