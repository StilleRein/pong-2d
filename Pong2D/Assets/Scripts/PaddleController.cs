using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PaddleController : NetworkBehaviour
{
    public float speed, topLine, botLine;
    public string axis;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
            return;

        float move = Input.GetAxis(axis) * speed * Time.deltaTime;
        float nextPos = transform.position.y + move;

        if (nextPos > topLine || nextPos < botLine)
            move = 0;

        transform.Translate(0, move, 0);
    }

    private void Awake()
    {
        if (transform.position.x > 0)
        {
             gameObject.tag = "Player";
             transform.GetComponent<SpriteRenderer>().color = new Color(0f, 0.6509804f, 0.8509804f);
        }

        else
        {
            gameObject.tag = "Player";
            transform.GetComponent<SpriteRenderer>().color = new Color(0.9607843f, 0.4862745f, 0.509804f);
        }
    }
}
