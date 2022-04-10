using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DividedCovid19Virus : Covid
{
    private enum AttackType { AroundShot, TargetSprayShot, GateOfBabylon, Sprint };
    [SerializeField]
    private AttackType attackType;

    private Slider HPSlider;
    private TextMeshProUGUI HPText;


    private void Start()
    {
        HPSlider = GetComponentInChildren<Slider>();
        HPText = GetComponentInChildren<TextMeshProUGUI>();
        HPSlider.value = hp/maxhp;
        HPText.text = hp.ToString();
        StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(1 / atkSpeed);
        switch (attackType)
        {
            case AttackType.AroundShot:
                StartCoroutine(AroundShot());
                break;
            case AttackType.TargetSprayShot:
                StartCoroutine(TargetSprayShot());
                break;
            case AttackType.GateOfBabylon:
                StartCoroutine(GateOfBabylon());
                break;
            case AttackType.Sprint:
                StartCoroutine(Sprint());
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerProjectile"))
        {
            GameManager.Instance.bossManager.GetDamaged(collision.GetComponent<PlayerProjectile>().atk, gameObject.GetComponent<DividedCovid19Virus>());
        }
    }


    private IEnumerator AroundShot()
    {
        int lr = Random.Range(-1, 2);
        while (lr == 0)
        {
            lr = Random.Range(-1, 2);
        }
        while (transform.position.y > 0)
        {
            transform.Translate(Vector2.down * 5 * Time.deltaTime);
            yield return new WaitForSeconds(.01f);
        }

        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(.5f/*Random.Range(.5f, 1f)*/);
            for (int j = 0; j < 10; j++)
            {
                CovidProjectile prj = Instantiate(Projectiles[0], transform.position, Quaternion.identity).GetComponent<CovidProjectile>();
                prj.atk = atk;
                prj.dir = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / 10 + j * lr * .1f), Mathf.Sin(Mathf.PI * 2 * i / 10 + j * lr * .1f));
            }
        }

        while (transform.position.y < 2.5f)
        {
            transform.Translate(Vector2.up * 5 * Time.deltaTime);
            yield return new WaitForSeconds(.01f);

        }
        int at = Random.Range(0, 4);
        attackType = (AttackType)at;
        StartCoroutine(Attack());
    }


    private IEnumerator TargetSprayShot()
    {
        for (int j = 0; j < 50; j++)
        {
            yield return new WaitForSeconds(.05f);
            CovidProjectile prj = Instantiate(Projectiles[0], transform.position, Quaternion.identity).GetComponent<CovidProjectile>();
            prj.dir = (Vector2)(GameManager.Instance.Player.transform.position - transform.position) + new Vector2(Random.Range(-3f, 4f), 0);
            prj.atk = atk;
        }
        int at = Random.Range(0, 4);
        attackType = (AttackType)at;
        StartCoroutine(Attack());
    }

    IEnumerator GateOfBabylon()
    {
        for (int i = 0; i < 30; i++)
        {
            yield return new WaitForSeconds(.1f);
            CovidProjectile prj = Instantiate(Projectiles[0], new Vector2(Random.Range(-8f, 8.1f), Random.Range(1f, 4.1f)), Quaternion.identity).GetComponent<CovidProjectile>();
            prj.atk = atk;
            StartCoroutine(GOBCor(prj, i));
        }
        int at = Random.Range(0, 4);
        attackType = (AttackType)at;
        StartCoroutine(Attack());
    }

    IEnumerator GOBCor(CovidProjectile prj, int cnt)
    {
        yield return new WaitForSeconds(3f - (.1f * (cnt + 1)));
        if (prj != null)
        {
            prj.dir = (Vector2)(GameManager.Instance.Player.transform.position - prj.transform.position) + new Vector2(Random.Range(-3f, 4f), 0);
            prj.moveSpeed *= 4f;
        }
    }

    IEnumerator Sprint()
    {
        int cnt = 0;
        while (cnt < 2)
        {
            if (isDownExited())
            {
                transform.position = new Vector2(transform.position.x, 7);
                cnt++;
            }
            transform.Translate(Vector2.down * 5 * Time.deltaTime);
            yield return new WaitForSeconds(.001f);
        }
        while (transform.position.y > 2.5f)
        {
            transform.Translate(Vector2.down * 5 * Time.deltaTime);
            yield return new WaitForSeconds(.001f);
        }
        attackType = AttackType.AroundShot;
        StartCoroutine(Attack());
    }

}
