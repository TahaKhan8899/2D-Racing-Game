using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scroll2 : MonoBehaviour
{
    public float scrollSpeed;
    private Vector2 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float newPos = Mathf.Repeat(Time.time * scrollSpeed, 10);
        transform.position = startPos + Vector2.down * newPos;

    }
}
