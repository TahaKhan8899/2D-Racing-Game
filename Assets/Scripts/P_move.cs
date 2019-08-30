using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_move : MonoBehaviour
{
    private float xPos;
    // t is the wait time for movement, less t = faster movement
    private float t = 0.1f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Horizontal") && Game_Init.gameStarted)
        {
            if (Input.GetAxis("Horizontal") > 0 && t > 0.06f && xPos < 7)
            {
                xPos += 0.6f;
                t = 0.0f;
            }
            if (Input.GetAxis("Horizontal") < 0 && t > 0.06f && xPos > -7.1)
            {
                xPos -= 0.6f;
                t = 0.0f;
            }
            t += Time.deltaTime;
        }
        MovePlayer();
    }

    void MovePlayer()
    {
        Vector2 playerPos = gameObject.transform.position;
        playerPos.x = xPos;
        gameObject.transform.position = Vector2.Lerp(gameObject.transform.position, playerPos, 15 * Time.deltaTime);
    }

}
