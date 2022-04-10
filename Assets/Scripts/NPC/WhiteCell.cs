using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteCell : NPC
{
    [SerializeField]
    private List<GameObject> Items = new List<GameObject>();

    void Update()
    {
        transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("PlayerProjectile"))
        {
            Instantiate(Items[Random.Range(0, Items.Count)], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
