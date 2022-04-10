using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DividedCancer : Covid
{
    private enum AttackType { AroundShot};
    [SerializeField]
    private AttackType attackType;
    [SerializeField]

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
        transform.localScale = new Vector3(2, 2, 2);
        moveSpeed = temp;
        StartCoroutine(Attack());
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
