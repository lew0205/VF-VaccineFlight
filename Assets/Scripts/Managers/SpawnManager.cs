using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> NPCS;
    [SerializeField]
    private List<Wave> Wave;

    int WaveCnt = 0;

    public bool isCovidSpawnable = true;

    public float NPCSpawnTime;
    public int WhiteCellSpawnPercentage;
    public int RedCellSpawnPercentage;

    private void Start()
    {
        GameManager.Instance.spawnManager = this;
        SpawnWaves();
        StartCoroutine(SpawnNPCCor());
    }

    private void Update()
    {
        if (FindObjectsOfType<Covid>().Length == 0)
            isCovidSpawnable = true;
    }

    void SpawnWaves()
    {
        if (isCovidSpawnable)
        {
            StartCoroutine(SpawnWaveCor());
        }
    }

    public void BossSpawn(Wave bossSpawnWave)
    {
        foreach (GameObject gameObject in bossSpawnWave.Waves)
        {
            Vector2 spawnLoc = new Vector2(Random.Range(-8f, 8f), 4.5f);
            Instantiate(gameObject, spawnLoc, Quaternion.identity);
        }
    }

    IEnumerator SpawnNPCCor()
    {
        yield return new WaitForSeconds(NPCSpawnTime);
        if (Random.Range(0, 100) < WhiteCellSpawnPercentage)
        {
            Vector2 spawnLoc = new Vector2(Random.Range(-8f, 8f), 4.5f);
            Instantiate(NPCS[0], spawnLoc, Quaternion.identity);
        }
        if (Random.Range(0, 100) < RedCellSpawnPercentage)
        {
            Vector2 spawnLoc = new Vector2(Random.Range(-8f, 8f), Random.Range(-4.5f, -4f));
            Instantiate(NPCS[1], spawnLoc, Quaternion.identity);
        }
        StartCoroutine(SpawnNPCCor());
    }

    public void SpawnNPC100per(string name)
    {
        switch (name)
        {
            case "white":
                Vector2 spawnLoc = new Vector2(Random.Range(-8f, 8f), 4.5f);
                Instantiate(NPCS[0], spawnLoc, Quaternion.identity);
                break;
            case "red":
                Vector2 spawnLoc_ = new Vector2(Random.Range(-8f, 8f), Random.Range(-4.5f, -4f));
                Instantiate(NPCS[1], spawnLoc_, Quaternion.identity);
                break;
        }
    }

    IEnumerator SpawnWaveCor()
    {
        isCovidSpawnable = false;
        foreach (GameObject covid in Wave[WaveCnt].Waves)
        {
            if (GameManager.Instance.stage == GameManager.Stage.stage1)
                yield return new WaitForSeconds(Random.Range(1.5f, 3f));
            else yield return new WaitForSeconds(Random.Range(.5f, 1.5f));
            Vector2 spawnLoc = new Vector2(Random.Range(-8f, 8f), 4.5f);
            Instantiate(covid, spawnLoc, Quaternion.identity);
        }
        WaveCnt++;
        while (!isCovidSpawnable)
            yield return null;
        yield return new WaitForSeconds(3f);
        SpawnWaves();
    }
}

[System.Serializable]
public class Wave
{
    public List<GameObject> Waves;
}