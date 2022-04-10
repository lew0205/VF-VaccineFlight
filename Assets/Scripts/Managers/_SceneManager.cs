using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class _SceneManager : MonoBehaviour
{
    private static _SceneManager instance;
    public static _SceneManager Instance
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

    public void gotoStartScene()
    {
        Time.timeScale = 0;
        SceneManager.LoadScene("StartScene");
        GameManager.Instance.isClear = false;
        GameManager.Instance.score = 0;
    }

    public void gotoStage1()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Stage1");
        GameManager.Instance.clearTime = 0f;
        GameManager.Instance.stage = GameManager.Stage.stage1;
        GameManager.Instance.hp = GameManager.Instance.maxHp;
        GameManager.Instance.pg = 10;
        GameManager.Instance.totalKill = 0;
        GameManager.Instance.clearTime = 0;
        GameManager.Instance.deadRedCells = 0;
        GameManager.Instance.usedItems = 0;
        GameManager.Instance.isClear = false;
        SoundManager.Instance.PlayBGM(SoundManager.Instance.BGM1);
    }

    public void gotoStage2()
    {
        SceneManager.LoadScene("Stage2");
        Time.timeScale = 1;
        GameManager.Instance.stage = GameManager.Stage.stage2;
        GameManager.Instance.hp = GameManager.Instance.maxHp;
        GameManager.Instance.pg = 30;
        SoundManager.Instance.PlayBGM(SoundManager.Instance.BGM1);
    }

    public void gotoResultScene()
    {
        Time.timeScale = 0;
        SceneManager.LoadScene("ResultScene");
        RankingManager.Instance.GetScore(GameManager.Instance.score);
    }
}
