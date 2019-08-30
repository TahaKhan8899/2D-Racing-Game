using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Init : MonoBehaviour
{

    public static bool gameStarted;
    public static bool firstFrame = true;
    public GameObject cam;

    // Start is called before the first frame update
    void Awake()
    {
        P_Collide.health = 3;
        gameStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            gameStarted = true;
        }
        if (!gameStarted)
        {
            cam.gameObject.transform.position = new Vector3(3.31f, 3.28f, -12.18f);
            cam.GetComponent<Camera>().orthographicSize = 2;
        }
        if (gameStarted)
        {
            cam.gameObject.transform.position = Vector3.Lerp(cam.gameObject.transform.position, new Vector3(-0.01f, 5.25f, -12.18f), 2 * Time.deltaTime);
            cam.GetComponent<Camera>().orthographicSize = Mathf.Lerp(cam.GetComponent<Camera>().orthographicSize, 5, 2 * Time.deltaTime);

        }
    }
}
