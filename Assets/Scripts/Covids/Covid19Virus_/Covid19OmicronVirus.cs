using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Covid19OmicronVirus : Covid
{
    private enum AttackType { AroundShot, AroundShott, TargetSprayShot, GateOfBabylon, GateOfBabylon2, GateOfBabylon3, Sprint, Sprint2, SpawnPattern };
    [SerializeField]
    private AttackType attackType;
    [SerializeField]
    private GameObject LDividedCovid19OmicronVirus;
    [SerializeField]
    private GameObject RDividedCovid19OmicronVirus;

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
        transform.position = new Vector2(0, 2.5f);
        SoundManager.Instance.PlayBGM(SoundManager.Instance.BGM2);
        StartCoroutine(Attack());
    }

    void Update()
    {

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
            case AttackType.GateOfBabylon3:
                StartCoroutine(GateOfBabylon3());
                break;
            case AttackType.Sprint2:
                StartCoroutine(Sprint2());
                break;
            case AttackType.AroundShott:
                StartCoroutine(AroundShott());
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
            HPSlider.value = hp / maxhp;
            HPText.text = hp.ToString();
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
        int at = Random.Range(0, 5);
        attackType = (AttackType)at;
        StartCoroutine(Attack());
    }

    IEnumerator AroundShott()
    {
        for (int i = 0; i < 30; i++)
        {
            yield return new WaitForSeconds(.3f);
            for (int j = 0; j < 25; j++)
            {
                CovidProjectile prj = Instantiate(Projectiles[0], transform.position, Quaternion.identity).GetComponent<CovidProjectile>();
                prj.atk = atk;
                prj.dir = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / 10 + j), Mathf.Sin(Mathf.PI * 2 * i / 10 + j));
            }
        }
        int at = Random.Range(0, 8);
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
        int at = Random.Range(0, 8);
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
        Instantiate(LDividedCovid19OmicronVirus, new Vector2(transform.position.x - 3.5f, 3), Quaternion.identity).GetComponent<DividedCovid19OmicronVirus>();
        Instantiate(RDividedCovid19OmicronVirus, new Vector2(transform.position.x + 3.5f, 3), Quaternion.identity).GetComponent<DividedCovid19OmicronVirus>();
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
        int at = Random.Range(0, 8);
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
        int at = Random.Range(0, 8);
        attackType = (AttackType)at;
        StartCoroutine(Attack());
    }

    IEnumerator GOBCor2(CovidProjectile prj, int cnt)
    {
        yield return new WaitForSeconds(.1f);
        if (prj != null)
        {
            prj.dir = (Vector2)(GameManager.Instance.Player.transform.position - prj.transform.position) + new Vector2(Random.Range(-1f, 1f), 0);
            //prj.moveSpeed *= 2f;
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
        int at = Random.Range(0, 8);
        attackType = (AttackType)at;
        StartCoroutine(Attack());
    }

    IEnumerator GOBCor3(CovidProjectile prj, int cnt)
    {
        yield return new WaitForSeconds(5f);
        if (prj != null)
        {
            prj.dir = (Vector2)(GameManager.Instance.Player.transform.position - prj.transform.position) + new Vector2(Random.Range(-1f, 1f), 0);
            //prj.moveSpeed *= 2f;
        }
    }


    IEnumerator Sprint()
    {
        int cnt = 0;
        int maxCnt = Random.Range(3, 6);
        StartCoroutine(SprintShot());
        while (cnt < maxCnt)
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
        int at = Random.Range(0, 8);
        attackType = (AttackType)at;
        StartCoroutine(Attack());
    }

    IEnumerator SprintShot()
    {
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(.3f/*Random.Range(.5f, 1f)*/);
            for (int j = 0; j < 20; j++)
            {
                CovidProjectile prj = Instantiate(Projectiles[0], transform.position, Quaternion.identity).GetComponent<CovidProjectile>();
                prj.atk = atk;
                prj.dir = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / 10 + j), Mathf.Sin(Mathf.PI * 2 * i / 10 + j));
            }
        }
    }
    IEnumerator Sprint2()
    {
        int cnt = 0;
        int maxCnt = Random.Range(3, 6);
        while (cnt < maxCnt)
        {
            if (isDownExited())
            {
                int a = Random.Range(0, 2);
                if (a == 0)
                    transform.position = new Vector2(Random.Range(-8f, 8.1f), transform.position.y);
                else
                    transform.position = new Vector2(GameManager.Instance.Player.transform.position.x, transform.position.y);
                transform.rotation = Quaternion.Euler(0f, 0f, 180f);
                cnt++;
            }
            else if (isUpExited())
            {
                int a = Random.Range(0, 2);
                if (a == 0)
                    transform.position = new Vector2(Random.Range(-8f, 8.1f), transform.position.y);
                else
                    transform.position = new Vector2(GameManager.Instance.Player.transform.position.x, transform.position.y);
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                cnt++;
            }
            transform.Translate(Vector2.down * 10 * Time.deltaTime);
            yield return new WaitForSeconds(.0005f);
        }
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        transform.position = new Vector2(0, 6);
        while (transform.position.y > 2.5f)
        {
            transform.Translate(Vector2.down * 10 * Time.deltaTime);
            yield return new WaitForSeconds(.001f);
        }
        int at = Random.Range(0, 8);
        attackType = (AttackType)at;
        StartCoroutine(Attack());
    }

    IEnumerator SpawnPattern()
    {
        GameManager.Instance.spawnManager.BossSpawn(spawnPattern);
        yield return new WaitForSeconds(5);
        StartCoroutine(Attack());
    }
}
