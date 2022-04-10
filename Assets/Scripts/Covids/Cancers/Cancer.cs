using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cancer : Covid
{
    private enum AttackType { AroundShot, Divide };
    [SerializeField]
    private AttackType attackType;
    [SerializeField]
    private GameObject dividedCancer;

    private void Start()
    {

        vec = Vector2.down;
        StartCoroutine(Attack());
    }

    void Update()
    {
        transform.Translate(vec.normalized * moveSpeed * Time.deltaTime);

        if (isDownExited())
        {
            GameManager.Instance.PainUp(atk * .5f);
            Destroy(gameObject);
        }
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(1 / atkSpeed);
        StartCoroutine(AroundShot());
    }

    IEnumerator AroundShot()
    {
        float temp = moveSpeed;
        moveSpeed = 0;
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(.1f);
            transform.localScale *= .9f;
        }
        for (int i = 0; i < 20; i++)
        {
            CovidProjectile prj = Instantiate(Projectiles[0], transform.position, Quaternion.identity).GetComponent<CovidProjectile>();
            prj.atk = atk;
            prj.dir = new Vector2(Mathf.Cos(Mathf.PI * i / 20 * 2), Mathf.Sin(Mathf.PI * i / 20 * 2));
        }
        transform.localScale = new Vector3(3, 3, 3);
        moveSpeed = temp;
        StartCoroutine(Attack());
    }

    void Divide()
    {
        Instantiate(dividedCancer, new Vector2(transform.position.x - 1, transform.position.y), Quaternion.identity);
        Instantiate(dividedCancer, new Vector2(transform.position.x + 1, transform.position.y), Quaternion.identity);
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
            Divide();
            GameManager.Instance.ScoreUp(score);
            GameManager.Instance.totalKill++;
            Destroy(gameObject);
        }
    }


}
