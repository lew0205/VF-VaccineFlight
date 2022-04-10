using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cheat : MonoBehaviour
{
    public TMP_InputField hptmp;
    public TMP_InputField pgtmp;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (GameManager.Instance.shotLv < 5)
                GameManager.Instance.shotLv++;
            else
            {
                GameManager.Instance.shotLv = 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            _SceneManager.Instance.gotoStage1();
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            _SceneManager.Instance.gotoStage2();
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            GameManager.Instance.ControllInvincibility(9999999);
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            GameManager.Instance.StopInvin();
        }
        if (Input.GetKeyDown(KeyCode.F6))
        {
            foreach (Covid covid in FindObjectsOfType<Covid>())
            {

                Debug.Log(FindObjectsOfType<Covid>().Length);
                Destroy(covid.gameObject);
            }
        }
        if (Input.GetKeyDown(KeyCode.F7))
        {
            GameManager.Instance.spawnManager.SpawnNPC100per("white");
        }
        if (Input.GetKeyDown(KeyCode.F8))
        {
            GameManager.Instance.spawnManager.SpawnNPC100per("red");
        }
        if (Input.GetKeyDown(KeyCode.F9))
        {
            Time.timeScale = 0;
            hptmp.enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.F10))
        {
            Time.timeScale = 0;
            pgtmp.enabled = true;
        }

        void sethealth()
        {
            Time.timeScale = 1;
            float sethp = float.Parse(hptmp.text);
            hptmp.enabled = false;
            if(sethp>GameManager.Instance.hp)
            GameManager.Instance.Heal(sethp - GameManager.Instance.hp);
            else if (sethp < GameManager.Instance.hp)
            {
                GameManager.Instance.GetDamaged(sethp - GameManager.Instance.hp);
            }

        }
        void setpain()
        {
            Time.timeScale = 1;
            float setpg = float.Parse(pgtmp.text);

            pgtmp.enabled = false;
            if (setpg> GameManager.Instance.pg)
                GameManager.Instance.PainUp(setpg - GameManager.Instance.pg);
            else if (setpg < GameManager.Instance.pg)
                GameManager.Instance.PainKill(GameManager.Instance.pg - setpg);
        }
    }
}
