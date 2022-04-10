using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bacteria : Covid
{
    private float h;

    void Start()
    {
        vec = Vector2.down;
        if (GameManager.Instance.stage == GameManager.Stage.stage2)
        {
            hp *= 2;
            atk *= 2;
        }
        StartCoroutine(Attack());
    }

    void Update()
    {
        transform.Translate(vec.normalized * moveSpeed * Time.deltaTime);
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x < .02f)
        {
            pos.x = .02f;
            h *= -1;
        }
        if (pos.x > .98f)
        {
            pos.x = .98f;
            h *= -1;
        }

        if (isDownExited())
        {
            GameManager.Instance.PainUp(atk * .5f);
            Destroy(gameObject);
        }

        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(1 / atkSpeed);
        StartCoroutine(randomMove());
        StartCoroutine(Attack());
    }

    IEnumerator randomMove()
    {
        vec = new Vector2(Random.Range(-3f, 3f), -1);
        moveSpeed *= 3;
        yield return new WaitForSeconds(1);
        moveSpeed /= 3;
        vec = Vector2.down;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerProjectile"))
        {
            GetDamaged(collision.GetComponent<PlayerProjectile>().atk);
            CovidDeadCheck();
        }
    }

    void CovidDeadCheck()
    {
        if (hp == 0)
        {
            GameManager.Instance.ScoreUp(score);
            GameManager.Instance.totalKill++;
            Destroy(gameObject);
        }
    }
}