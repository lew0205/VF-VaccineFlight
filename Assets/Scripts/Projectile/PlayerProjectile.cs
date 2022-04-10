using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : Projectile
{
    public GameObject Boom;
    private void Update()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
        if (pos.y > 1)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Covid") || collision.CompareTag("NPC"))
        {
            SoundManager.Instance.PlaySoundEffect("PlayerAttack", attackSound, .05f);
            StartCoroutine(boomEffect());
            moveSpeed = 0;
        }
    }

    IEnumerator boomEffect()
    {
        GameObject boom = Instantiate(Boom, new Vector3(transform.position.x, transform.position.y, -1), Quaternion.identity);
        yield return new WaitForSeconds(.1f);
        Destroy(boom);
        Destroy(gameObject);
    }
}
