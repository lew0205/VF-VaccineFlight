using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingManager : MonoBehaviour
{
    private static RankingManager instance;
    public static RankingManager Instance
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

    public List<Ranking> rankingList = new List<Ranking>();
    Ranking newRanking;

    private void Start()
    {
        InitNewRanking();
    }

    public void InitNewRanking()
    {
        Debug.Log("1");
        newRanking = new Ranking(null, 0);
        Debug.Log(newRanking);
    }

    public void GetName(string name)
    {
        Debug.Log("5");
        newRanking.name = name;
        Debug.Log(newRanking.name);
        SortRanking();

    }

    public void GetScore(float score)
    {
        Debug.Log("2");
        newRanking.score = score;
        Debug.Log(newRanking.score);
        CheckScoreRanking();
    }

    public void CheckScoreRanking()
    {
        Debug.Log("3");
        rankingList.Add(newRanking);
        if (rankingList.Count > 5)
        {
            if (rankingList[4].score < rankingList[5].score)
            {
                Ranking temp = rankingList[5];
                rankingList[5] = rankingList[4];
                rankingList[4] = temp;
            GameManager.Instance.uiManager.OpenInputNamePanel();
            }
            rankingList.RemoveAt(5);
            GameManager.Instance.uiManager.rankingPanel.SetActive(true);
            return;
        }
        Debug.Log(rankingList.Count);
        GameManager.Instance.uiManager.OpenInputNamePanel();
    }

    public void SortRanking()
    {
        Debug.Log("7");
        if (rankingList.Count > 1)
            rankingList.Sort(delegate (Ranking a, Ranking b)
            {
                if (a.score > b.score)
                    return -1;
                else if (a.score < b.score)
                    return 1;
                else
                    return 0;
            });
        GameManager.Instance.uiManager.UpdateRanking();
        GameManager.Instance.uiManager.UpdateMyResult();
        InitNewRanking();
    }
}

public class Ranking
{
    public string name;
    public float score;
    public Ranking(string name, float score)
    {
        this.name = name;
        this.score = score;
    }
}