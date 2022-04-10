using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public Slider HPSlider;
    public Slider PGSlider;
    public TextMeshProUGUI HPText;
    public TextMeshProUGUI PGText;
    public TextMeshProUGUI Score;

    public GameObject rankingPanel;
    public GameObject NameFieldPanel;
    public TMP_InputField NameField;
    public List<TextMeshProUGUI> rankingTextList = new List<TextMeshProUGUI>();

    public TextMeshProUGUI TotalKillText;
    public TextMeshProUGUI ClearTimeText;
    public TextMeshProUGUI UsedItemText;
    public TextMeshProUGUI DeadRedCellsText;
    public TextMeshProUGUI CovidKilledText;
    public TextMeshProUGUI CovidKilledNotText;

    public AudioClip goodEffect;
    public AudioClip buttonEffect;

    private void Awake()
    {
        GameManager.Instance.uiManager = this;
        UpdateHP();
        UpdatePG();
        UpdateScore();
    }

    public void UpdateHP()
    {
        HPText.text = GameManager.Instance.hp.ToString();
        HPSlider.value = GameManager.Instance.hp / GameManager.Instance.maxHp;
    }

    public void UpdatePG()
    {
        PGText.text = GameManager.Instance.pg.ToString();
        PGSlider.value = GameManager.Instance.pg / GameManager.Instance.maxPg;
    }

    public void UpdateScore()
    {
        Score.text = GameManager.Instance.score.ToString();
    }

    public void UpdateRanking()
    {
        int i = 0;
        foreach (Ranking ranking in RankingManager.Instance.rankingList)
        {
            rankingTextList[i].gameObject.SetActive(true);
            if (ranking.name == null && ranking.score != 0)
            {
                rankingTextList[i].text = i + 1 + "등 ??? | " + ranking.score;
                i++;
                return;
            }
            else if (ranking.name == null && ranking.score == 0)
                rankingTextList[i].gameObject.SetActive(false);
            rankingTextList[i].text = i + 1 + "등 " + ranking.name + " | " + ranking.score;
            i++;
        }
        for (int j = i; j < rankingTextList.Count; j++)
        {
            rankingTextList[j].gameObject.SetActive(false);
        }
    }

    public void UpdateMyResult()
    {
        TotalKillText.text = "총 처치 " + GameManager.Instance.totalKill;
        ClearTimeText.text = "플레이 타임 " + (int)(GameManager.Instance.clearTime / 60) + ":" + (int)(GameManager.Instance.clearTime % 60);
        UsedItemText.text = "사용한 아이템 " + GameManager.Instance.usedItems;
        DeadRedCellsText.text = "부숴진 적혈구 " + GameManager.Instance.deadRedCells;
        if (GameManager.Instance.isClear)
            CovidKilledText.gameObject.SetActive(true);
        else
        {
            CovidKilledNotText.gameObject.SetActive(false);
        }
    }

    public void OpenInputNamePanel()
    {
        Debug.Log("4");
        Debug.Log(NameFieldPanel);
        SoundManager.Instance.PlaySoundEffect("InputName", goodEffect, .1f);
        NameFieldPanel.SetActive(true);
    }

    public void InputNameButton()
    {
        if (NameField.text.Length == 3)
        {
            RankingManager.Instance.GetName(NameField.text);
            SoundManager.Instance.PlaySoundEffect("Button", buttonEffect, .1f);
            rankingPanel.SetActive(true);
            NameFieldPanel.SetActive(false);
        }
    }

    public void NotInputNameButton()
    {
        RankingManager.Instance.SortRanking();
        SoundManager.Instance.PlaySoundEffect("Button", buttonEffect, .1f);
        rankingPanel.SetActive(true);
        NameFieldPanel.SetActive(false);
    }

    public void StartGame()
    {
        SoundManager.Instance.PlaySoundEffect("Button", buttonEffect, .1f);
        _SceneManager.Instance.gotoStage1();
    }

    public void ReStart()
    {
        SoundManager.Instance.PlaySoundEffect("Button", buttonEffect, .1f);
        _SceneManager.Instance.gotoStartScene();
    }
}
