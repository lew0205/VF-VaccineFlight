using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Germ : Covid
{
    private enum AttackType { TargetShot, Sprint };
    [SerializeField]
    private AttackType attackType;

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
        switch (attackType)
        {
            case AttackType.TargetShot:
                TargetShot();
                attackType = AttackType.Sprint;
                break;
            case AttackType.Sprint:
                StartCoroutine(Sprint());
                attackType = AttackType.TargetShot;
                break;
        }
        StartCoroutine(Attack());
    }

    void TargetShot()
    {
        CovidProjectile prj = Instantiate(Projectiles[1], transform.position, Quaternion.identity).GetComponent<CovidProjectile>();
        prj.atk = atk;
        prj.dir = GameManager.Instance.Player.transform.position - transform.position;
    }

    IEnumerator Sprint()
    {
        moveSpeed *= 3;
        yield return new WaitForSeconds(.5f);
        moveSpeed /= 3;
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
