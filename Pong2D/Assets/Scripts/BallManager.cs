using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BallManager : NetworkBehaviour
{
    public GameObject ballPrefabs;
    public static bool isBallSpawn = false;
    GameObject ball;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!isServer || isBallSpawn)
            return;

        if (NetworkServer.connections.Count == 2)
        {
            ball = (GameObject)Instantiate(ballPrefabs);
            NetworkServer.Spawn(ball);
            isBallSpawn = true;
        }
    }
}
