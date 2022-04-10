using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    protected float moveSpeed;
    public AudioClip clip;

    protected void Update()
    {
       transform.Translate(Vector2.down*moveSpeed*Time.deltaTime);
    }
}
