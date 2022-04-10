using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance != null)
                return instance;
            else
                return null;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public GameObject Player;

    public int shotLv = 1;
    public float hp;
    public float maxHp;
    public float pg;
    public float maxPg;
    public float score = 0;

    public bool isInvincible = false;

    public BossManager bossManager;
    public UIManager uiManager;

    public enum Stage { stage1, stage2 };
    public Stage stage;

    public Coroutine Invincibility;
    float currentTime;

    public SpawnManager spawnManager;

    public int totalKill;
    public float clearTime;
    public int usedItems = 0;
    public int deadRedCells = 0;
    public bool isClear = false;

    public AudioClip clip;

    private void Start()
    {
        hp = maxHp;
        uiManager.UpdateHP();
        uiManager.UpdatePG();
    }

    private void Update()
    {
        currentTime -= Time.deltaTime;
        clearTime += Time.deltaTime;
    }

    public void ScoreUp(float score)
    {
        if (stage == Stage.stage2)
            score *= 2;
        this.score += score;
        uiManager.UpdateScore();
    }

    public void GetDamaged(float damage)
    {
        if (!isInvincible)
        {
            SoundManager.Instance.PlaySoundEffect("PlayerAttacked", clip,.5f);
            hp = Mathf.Max(hp - damage, 0);
            uiManager.UpdateHP();
            DeadCheck();
            ControllInvincibility(1.5f);
        }
    }

    public void Heal(float healValue)
    {
        hp = Mathf.Min(hp + healValue, 100);
        uiManager.UpdateHP();
    }

    public void PainKill(float pValue)
    {
        pg = Mathf.Max(pg - pValue, 0);
        uiManager.UpdatePG();
    }

    public void PainUp(float pValue)
    {
        pg = Mathf.Min(pg + pValue, 100);
        uiManager.UpdatePG();
        DeadCheck();
    }

    public void DeadCheck()
    {
        if (hp <= 0 || pg >= 100)
        {
            GameEnd();
        }
    }

    public void GameEnd()
    {
        _SceneManager.Instance.gotoResultScene();
    }

    public void StarEffect()
    {
        ControllInvincibility(3);
    }

    public void StopInvin()
    {
        StopCoroutine(Invincibility);
        isInvincible = false;
    }

    public void ControllInvincibility(float duration)
    {
        if (currentTime != 0 && duration > currentTime && Invincibility != null)
        {
            StopCoroutine(Invincibility);
        }
        Invincibility = StartCoroutine(InvincibilityCor(duration));
    }

    IEnumerator InvincibilityCor(float duration)
    {
        currentTime = duration;
        isInvincible = true;
        Player.GetComponent<PlayerController>().anim.SetBool("isInvincible", true);
        yield return new WaitForSeconds(duration - .5f);
        Player.GetComponent<PlayerController>().anim.SetBool("isInvincible", false);
        yield return new WaitForSeconds(.5f);
        isInvincible = false;
    }
}
