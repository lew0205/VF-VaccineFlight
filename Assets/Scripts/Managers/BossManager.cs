using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BossManager : MonoBehaviour
{
    int deaddcvCnt = 0;
    int deaddcovCnt = 0;

    public Slider HPSlider;
    public TextMeshProUGUI HPText;

    public void Start()
    {
        GameManager.Instance.bossManager = this;
    }

    public void GetDamaged(float damage, DividedCovid19Virus dcv)
    {
        dcv.hp = Mathf.Max(dcv.hp - damage, 0);
        HPText = dcv.GetComponentInChildren<TextMeshProUGUI>();
        HPSlider = dcv.GetComponentInChildren<Slider>();
        HPText.text = dcv.hp.ToString();
        HPSlider.value = dcv.hp / dcv.maxhp;
        DeadCheck(dcv);
    }

    private void DeadCheck(DividedCovid19Virus dcv)
    {
        if (dcv.hp == 0)
        {
            GameManager.Instance.ScoreUp(dcv.score);
            deaddcvCnt++;
            GameManager.Instance.totalKill++;
            Destroy(dcv.gameObject);
            if (deaddcvCnt == 2)
            {
                GameManager.Instance.score *= (1 + GameManager.Instance.hp * .01f) + (2 - GameManager.Instance.pg * .01f);
                _SceneManager.Instance.gotoStage2();
            }
        }
    }

    public void GetDamaged(float damage, DividedCovid19OmicronVirus dcov)
    {

        dcov.hp = Mathf.Max(dcov.hp - damage, 0);
        HPText = dcov.GetComponentInChildren<TextMeshProUGUI>();
        HPSlider = dcov.GetComponentInChildren<Slider>();
        HPText.text = dcov.hp.ToString();
        HPSlider.value = dcov.hp / dcov.maxhp;
        DeadCheck(dcov);
    }

    private void DeadCheck(DividedCovid19OmicronVirus dcov)
    {
        if (dcov.hp == 0)
        {
            GameManager.Instance.ScoreUp(dcov.score);
            deaddcovCnt++;
            GameManager.Instance.totalKill++;
            Destroy(dcov.gameObject);
            if (deaddcovCnt == 2)
            {
                _SceneManager.Instance.gotoResultScene();
            }
        }
    }
}
