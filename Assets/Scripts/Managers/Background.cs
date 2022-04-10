using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public float moveSpeed;

    void Update()
    {
        transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
        if (transform.position.y < -6)
        {
            transform.position=new Vector2(transform.position.x, 6f);
        }
    }
}
