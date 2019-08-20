using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class P_Collide : MonoBehaviour
{
    public int tokens;
    public static int health = 3;
    public AudioClip collectToken;
    public AudioClip pain;
    public GameObject tokenUI;

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            StartCoroutine(GameOver());
        }
    }

    void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.tag == "token")
        {
            GetComponent<AudioSource>().PlayOneShot(collectToken, 0.5f);
            tokens++;
            tokenUI.GetComponent<Text>().text = tokens.ToString();
            Destroy(trig.gameObject);
        }
        if (trig.gameObject.tag == "log")
        {
            TakeDamage();
        }
    }

    void TakeDamage()
    {
        GetComponent<AudioSource>().PlayOneShot(pain, 0.5f);
        health--;
    }

    IEnumerator GameOver()
    {
        // Reloads the level
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("main");
        yield return null;
    }
}
