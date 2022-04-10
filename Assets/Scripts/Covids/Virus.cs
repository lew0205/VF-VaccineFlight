using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : Covid
{
    private enum AttackType { PentaShot, Explosive };
    [SerializeField]
    private AttackType attackType;

    Coroutine currentCor;
    bool isDead=false;

    private void Start()
    {
        if (Random.Range(0, 2) == 0)
            transform.rotation = Quaternion.Euler(0, 0, 90);
        else
            transform.rotation = Quaternion.Euler(0, 0, 270);
        vec = Vector2.down;
        currentCor = StartCoroutine(Attack());
    }

    private void Update()
    {
        transform.Translate(vec * moveSpeed * Time.deltaTime);
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x < -.1f)
        {
            pos.y -= .1f;
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        if (pos.x > 1.1f)
        {
            pos.y -= .1f;
            transform.rotation = Quaternion.Euler(0, 0, 270);
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
        PentaShot();
        currentCor = StartCoroutine(Attack());
    }

    void PentaShot()
    {
        for (int i = 0; i < 5; i++)
        {
            CovidProjectile prj = Instantiate(Projectiles[0], transform.position, Quaternion.identity).GetComponent<CovidProjectile>();
            prj.dir = new Vector2(Mathf.Sin((float)i / 5 - .35f), -1);
            prj.atk = atk;
        }
    }

    IEnumerator Explosive()
    {
        moveSpeed = 0;
        for (int j = 0; j < 3; j++)
        {
            for (int i = 0; i < 10; i++)
            {
                CovidProjectile prj = Instantiate(Projectiles[0], transform.position, Quaternion.identity).GetComponent<CovidProjectile>();
                prj.atk = atk;
                prj.dir = new Vector2(Mathf.Cos(Mathf.PI * i / 10 * 2 - j), Mathf.Sin(Mathf.PI * i / 10 * 2 - j));
            }
            yield return new WaitForSeconds(.3f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerProjectile"))
        {
            GetDamaged(collision.GetComponent<PlayerProjectile>().atk);
            StartCoroutine(CovidDeadCheck());
        }
    }

    IEnumerator CovidDeadCheck()
    {
        if (hp == 0&&!isDead)
        {
            isDead= true;
            StopCoroutine(currentCor);
            StartCoroutine(Explosive());
            yield return new WaitForSeconds(1.5f);
            GameManager.Instance.ScoreUp(score);
            GameManager.Instance.totalKill++;
            Destroy(gameObject);
        }
    }
}
