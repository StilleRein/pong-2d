using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Controller : MonoBehaviour
{
    public float topLine = 3.44f, botLine = -3.44f;
    bool dragging = false;
    public string isPVP;
    float distance;

    // Start is called before the first frame update
    void Start()
    {
        isPVP = PlayerPrefs.GetString("isPVP");
    }

    // Update is called once per frame
    void Update()
    {
        if(isPVP == "false")
        {
            if (Input.mousePosition.x > Screen.width / 2) {
                if (Input.GetMouseButtonDown(0))
                {
                    distance = Vector3.Distance(transform.position, Camera.main.transform.position);
                    dragging = true;
                }

                if(Input.GetMouseButtonUp(0))
                {
                    dragging = false;
                }

                if (dragging)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    Vector3 rayPoint = ray.GetPoint(distance);

                    if(rayPoint.y > topLine || rayPoint.y < botLine){
                        if(rayPoint.y > topLine)
                            rayPoint.y = topLine;

                        if(rayPoint.y < botLine)
                            rayPoint.y = botLine;
                    }

                    transform.position = new Vector3(transform.position.x, rayPoint.y, transform.position.z);
                }
            }
        }
    }
}
