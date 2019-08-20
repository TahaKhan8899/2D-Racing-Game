using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearts : MonoBehaviour
{
    public GameObject[] hearts;
    public Sprite heart;

    // Update is called once per frame
    void Update()
    {
        if (P_Collide.health == 3)
        {
            hearts[0].GetComponent<SpriteRenderer>().sprite = heart;
            hearts[1].GetComponent<SpriteRenderer>().sprite = heart;
            hearts[2].GetComponent<SpriteRenderer>().sprite = heart;
        }
        if (P_Collide.health == 2)
        {
            hearts[0].GetComponent<SpriteRenderer>().sprite = heart;
            hearts[1].GetComponent<SpriteRenderer>().sprite = heart;
            hearts[2].SetActive(false);
        }
        if (P_Collide.health == 1)
        {
            hearts[0].GetComponent<SpriteRenderer>().sprite = heart;
            hearts[1].SetActive(false);
            hearts[2].SetActive(false);
        }
        if (P_Collide.health == 0)
        {
            hearts[0].SetActive(false);
            hearts[1].SetActive(false);
            hearts[2].SetActive(false);
        }
    }
}
