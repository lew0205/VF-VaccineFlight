
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCell : NPC
{
    private int moveDir = 0;

    private void Start()
    {
        while (moveDir == 0)
            moveDir = Random.Range(-1, 2);
    }

    private void Update()
    {
        transform.Translate(Vector2.right * moveDir * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("CovidProjectile") || collision.CompareTag("PlayerProjectile"))
        {
            GameManager.Instance.PainUp(20);
            GameManager.Instance.deadRedCells++;
            Destroy(gameObject);
        }
    }
}
