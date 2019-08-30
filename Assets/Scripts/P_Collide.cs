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
    // TODO: NEW SOUND. public AudioClip bigPain;
    public GameObject tokenUI;
    public GameObject cam;

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
        if (trig.gameObject.tag == "diamond")
        {
            GetComponent<AudioSource>().PlayOneShot(collectToken, 0.5f);
            tokens = tokens + 10;
            tokenUI.GetComponent<Text>().text = tokens.ToString();
            Destroy(trig.gameObject);
        }
        if (trig.gameObject.tag == "badGuy")
        {
            TakeDamage();
        }
        if (trig.gameObject.tag == "BigBadGuy")
        {
            TakeBigDamage();
        }
    }

    void TakeBigDamage()
    {
        GetComponent<AudioSource>().PlayOneShot(pain, 0.5f);
        health = health - 3;
        iTween.ShakePosition(cam, new Vector3(0.5f, 0.5f, 0.5f), 1);
    }

    void TakeDamage()
    {
        GetComponent<AudioSource>().PlayOneShot(pain, 0.5f);
        health--;
        iTween.ShakePosition(cam, new Vector3(0.2f, 0.2f, 0.2f), 1);
    }

    IEnumerator GameOver()
    {
        // Reloads the level
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("main");
        yield return null;
    }
}
