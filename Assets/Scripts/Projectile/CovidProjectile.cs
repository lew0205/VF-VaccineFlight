using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CovidProjectile : Projectile
{
    public Vector2 dir;

    private void Update()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        transform.Translate(dir.normalized * moveSpeed * Time.deltaTime);
        if (pos.x < -.1f || pos.x > 1.1f || pos.y > 1.1f || pos.y < -.1f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.GetDamaged(atk);
            Destroy(gameObject);
        }
    }
}
