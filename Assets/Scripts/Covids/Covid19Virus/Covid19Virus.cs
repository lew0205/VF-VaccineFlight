using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Covid19Virus : Covid
{
    private enum AttackType { AroundShot, TargetSprayShot, GateOfBabylon, GateOfBabylon2, Sprint,SpawnPattern };
    [SerializeField]
    private AttackType attackType;
    [SerializeField]
    private GameObject LDividedCovid19Virus;
    [SerializeField]
    private GameObject RDividedCovid19Virus;

    private Slider HPSlider;
    private TextMeshProUGUI HPText;

    public Wave spawnPattern;

    private void Start()
    {
        HPSlider = GetComponentInChildren<Slider>();
        HPText = GetComponentInChildren<TextMeshProUGUI>();
        HPSlider.value = hp / maxhp;
        HPText.text = hp.ToString();
        GameManager.Instance.spawnManager.isCovidSpawnable = false;
        transform.position = new Vector2(0, transform.position.y);
        SoundManager.Instance.PlayBGM(SoundManager.Instance.BGM2);
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
            case AttackType.Sprint:
                StartCoroutine(Sprint());
                break;
            case AttackType.SpawnPattern:
                StartCoroutine(SpawnPattern());
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerProjectile"))
        {
            GetDamaged(collision.GetComponent<PlayerProjectile>().atk);
            HPText.text = hp.ToString();
            HPSlider.value = hp / maxhp;
            if (hp / maxhp <= .5f)
                Divide();
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

        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(.4f/*Random.Range(.5f, 1f)*/);
            for (int j = 0; j < 20; j++)
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


    IEnumerator TargetSprayShot()
    {
        for (int j = 0; j < 100; j++)
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

    void Divide()
    {
        CovidProjectile[] covidProjectiles = FindObjectsOfType<CovidProjectile>();
        foreach (CovidProjectile covidProjectile in covidProjectiles)
        {
            Destroy(covidProjectile.gameObject);
        }
        Instantiate(LDividedCovid19Virus, new Vector2(transform.position.x - 3, 3), Quaternion.identity).GetComponent<DividedCovid19Virus>();
        Instantiate(RDividedCovid19Virus, new Vector2(transform.position.x + 3, 3), Quaternion.identity).GetComponent<DividedCovid19Virus>();
        Destroy(gameObject);
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
        int at = Random.Range(0, 4);
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
        int at = Random.Range(0, 4);
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

    IEnumerator Sprint()
    {
        int cnt = 0;
        while (cnt < 3)
        {
            if (isDownExited())
            {
                transform.position = new Vector2(transform.position.x, 7);
                cnt++;
            }
            transform.Translate(Vector2.down * 10 * Time.deltaTime);
            yield return new WaitForSeconds(.001f);
        }
        while (transform.position.y > 2.5f)
        {
            transform.Translate(Vector2.down * 10 * Time.deltaTime);
            yield return new WaitForSeconds(.001f);
        }
        attackType = AttackType.AroundShot;
        StartCoroutine(Attack());
    }

    IEnumerator SpawnPattern()
    {
        GameManager.Instance.spawnManager.BossSpawn(spawnPattern);
        yield return new WaitForSeconds(5);
        StartCoroutine(Attack());
    }
}
