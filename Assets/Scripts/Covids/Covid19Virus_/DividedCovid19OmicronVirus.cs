using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DividedCovid19OmicronVirus : Covid
{
    private enum AttackType { AroundShot, AroundShott, TargetSprayShot, GateOfBabylon, GateOfBabylon2, GateOfBabylon3 };
    [SerializeField]
    private AttackType attackType;

    TextMeshProUGUI HPText;
    Slider HPSlider;

    private void Start()
    {
        HPText = GetComponent<TextMeshProUGUI>();
        HPSlider = GetComponent<Slider>();
        HPSlider.value = hp / maxhp;
        HPText.text = hp.ToString();

        StartCoroutine(Attack());
    }

    IEnumerator Attack()
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
            case AttackType.GateOfBabylon2:
                StartCoroutine(GateOfBabylon2());
                break;
            case AttackType.GateOfBabylon3:
                StartCoroutine(GateOfBabylon3());
                break;
            case AttackType.AroundShott:
                StartCoroutine(AroundShott());
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerProjectile"))
        {
            GameManager.Instance.bossManager.GetDamaged(collision.GetComponent<PlayerProjectile>().atk, gameObject.GetComponent<DividedCovid19OmicronVirus>());
        }
    }

    IEnumerator AroundShot()
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
            yield return new WaitForSeconds(.4f/*Random.Range(.5f, 1f)*/);
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
        int at = Random.Range(0, 6);
        attackType = (AttackType)at;
        StartCoroutine(Attack());
    }

    IEnumerator AroundShott()
    {
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(.3f);
            for (int j = 0; j < 15; j++)
            {
                CovidProjectile prj = Instantiate(Projectiles[0], transform.position, Quaternion.identity).GetComponent<CovidProjectile>();
                prj.atk = atk;
                prj.dir = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / 10 + j), Mathf.Sin(Mathf.PI * 2 * i / 10 + j));
            }
        }
        int at = Random.Range(0, 6);
        attackType = (AttackType)at;
        StartCoroutine(Attack());
    }

    IEnumerator TargetSprayShot()
    {
        for (int j = 0; j < 60; j++)
        {
            yield return new WaitForSeconds(.05f);
            CovidProjectile prj = Instantiate(Projectiles[0], transform.position, Quaternion.identity).GetComponent<CovidProjectile>();
            prj.dir = (Vector2)(GameManager.Instance.Player.transform.position - transform.position) + new Vector2(Random.Range(-3f, 4f), 0);
            prj.atk = atk;
        }
        int at = Random.Range(0, 6);
        attackType = (AttackType)at;
        StartCoroutine(Attack());
    }

    IEnumerator GateOfBabylon()
    {
        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(.1f);
            CovidProjectile prj = Instantiate(Projectiles[0], new Vector2(Random.Range(-8f, 8.1f), Random.Range(1f, 4.1f)), Quaternion.identity).GetComponent<CovidProjectile>();
            prj.atk = atk;
            StartCoroutine(GOBCor(prj, i));
        }
        int at = Random.Range(0, 6);
        attackType = (AttackType)at;
        StartCoroutine(Attack());
    }

    IEnumerator GOBCor(CovidProjectile prj, int cnt)
    {
        yield return new WaitForSeconds(10f - (.1f * (cnt + 1)));
        if (prj != null)
        {
            prj.dir = (Vector2)(GameManager.Instance.Player.transform.position - prj.transform.position) + new Vector2(Random.Range(-2f, 2.1f), 0);
            prj.moveSpeed *= 2f;
        }
    }

    IEnumerator GateOfBabylon2()
    {
        for (int i = 0; i < 70; i++)
        {
            yield return new WaitForSeconds(.1f);
            CovidProjectile prj = Instantiate(Projectiles[0], new Vector2(Random.Range(-8f, 8.1f), Random.Range(1f, 4.1f)), Quaternion.identity).GetComponent<CovidProjectile>();
            prj.atk = atk;
            StartCoroutine(GOBCor2(prj, i));
        }
        int at = Random.Range(0, 6);
        attackType = (AttackType)at;
        StartCoroutine(Attack());
    }

    IEnumerator GOBCor2(CovidProjectile prj, int cnt)
    {
        yield return new WaitForSeconds(.1f);
        if (prj != null)
        {
            prj.dir = (Vector2)(GameManager.Instance.Player.transform.position - prj.transform.position) + new Vector2(Random.Range(-2f, 2.1f), 0);
            prj.moveSpeed *= 2f;
        }
    }

    IEnumerator GateOfBabylon3()
    {
        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(.1f);
            CovidProjectile prj = Instantiate(Projectiles[0], new Vector2(Random.Range(-8f, 8.1f), Random.Range(1f, 4.1f)), Quaternion.identity).GetComponent<CovidProjectile>();
            prj.atk = atk;
            StartCoroutine(GOBCor3(prj, i));
        }
        int at = Random.Range(0, 6);
        attackType = (AttackType)at;
        StartCoroutine(Attack());
    }

    IEnumerator GOBCor3(CovidProjectile prj, int cnt)
    {
        yield return new WaitForSeconds(5f);
        if (prj != null)
        {
            prj.dir = (Vector2)(GameManager.Instance.Player.transform.position - prj.transform.position) + new Vector2(Random.Range(-2f, 2.1f), 0);
            prj.moveSpeed *= 2f;
        }
    }
}
