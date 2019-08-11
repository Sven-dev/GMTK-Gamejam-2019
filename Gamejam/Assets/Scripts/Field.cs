using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public List<AI> EnemyPrefabs;
    public List<Transform> Spawns;

    [Space]
    public float SpawnDelay;

    private int Kills;
    private bool Playing;
    
    public List<AI> Enemies;

    private static Transform Points;

    public static Transform Player;
    public static float TimeScale;

    // Use this for initialization
    private void OnEnable()
    {
        Points = transform.GetChild(0);
        Player = GameObject.FindWithTag("Player").transform;
        TimeScale = Time.timeScale;

        Kills = 0;
        Playing = true;
        Enemies = new List<AI>();

        Health.OnDeath += GetKill;
        Health.OnPlayerDeath += Gameover;

        StartCoroutine(_SpawnTimer());
	}

    public static Vector2 GetPoint()
    {
        int rnd = Random.Range(0, Points.childCount - 1);
        return Points.GetChild(rnd).position;
    }

    private void GetKill()
    {
        Kills++;

        List<AI> temp = Enemies;
        Enemies.Clear();

        foreach(AI enemy in temp)
        {
            if (enemy != null)
            {
                Enemies.Add(enemy);
            }
        }
    }

    private void Gameover()
    {
        Playing = false;
    }

    IEnumerator _SpawnTimer()
    {
        float progress = 0;
        while (Playing)
        {
            if (Enemies.Count == 0)
            {
                SpawnEnemy();
            }

            if (progress >= SpawnDelay)
            {
                SpawnEnemies();
                progress = 0;
            }
            else
            {
                progress += Time.deltaTime;                         
            }

            yield return null;
        }
    }

    private void SpawnEnemy()
    {
        if (Kills < 5)
        {
            Enemies.Add(Instantiate(RandomEnemy(0, 2), RandomSpawn()));
        }
        else if (Kills < 10)
        {
            Enemies.Add(Instantiate(RandomEnemy(0, 3), RandomSpawn()));
        }
        else if (Kills < 15)
        {
            Enemies.Add(Instantiate(RandomEnemy(0, 4), RandomSpawn()));
        }
        else
        {
            Enemies.Add(Instantiate(RandomEnemy(0, 5), RandomSpawn()));
        }

        foreach (AI enemy in Enemies)
        {
            if (enemy != null)
            {
                enemy.transform.parent = null;
            }
        }
    }

    private void SpawnEnemies()
    {
        if (Kills < 5)
        {
            //spawn 2
            Enemies.Add(Instantiate(RandomEnemy(0, 2), RandomSpawn()));
            Enemies.Add(Instantiate(RandomEnemy(0, 2), RandomSpawn()));
        }
        else if (Kills < 10)
        {
            //spawn 3
            Enemies.Add(Instantiate(RandomEnemy(0, 3), RandomSpawn()));
            Enemies.Add(Instantiate(RandomEnemy(0, 3), RandomSpawn()));
            Enemies.Add(Instantiate(RandomEnemy(0, 3), RandomSpawn()));
        }
        else if (Kills < 15)
        {
            //spawn 4
            Enemies.Add(Instantiate(RandomEnemy(0, 4), RandomSpawn()));
            Enemies.Add(Instantiate(RandomEnemy(0, 4), RandomSpawn()));
            Enemies.Add(Instantiate(RandomEnemy(0, 4), RandomSpawn()));
        }
        else
        {
            //spawn 5
            Enemies.Add(Instantiate(RandomEnemy(0, 5), RandomSpawn()));
            Enemies.Add(Instantiate(RandomEnemy(0, 5), RandomSpawn()));
            Enemies.Add(Instantiate(RandomEnemy(0, 5), RandomSpawn()));
            Enemies.Add(Instantiate(RandomEnemy(0, 5), RandomSpawn()));
            Enemies.Add(Instantiate(RandomEnemy(0, 5), RandomSpawn()));
        }

        foreach (AI enemy in Enemies)
        {
            if (enemy != null)
            {
                enemy.transform.parent = null;
            }
        }
    }

    private AI RandomEnemy(int min, int max)
    {
        int rnd = Random.Range(min, max);
        return EnemyPrefabs[rnd];
    }

    private Transform RandomSpawn()
    {
        List<Transform> spawns = Spawns;
        List<Transform> goodspawns = new List<Transform>();

        //Check which spawns are taken
        foreach(Transform spawn in spawns)
        {
            if (spawn.childCount == 0)
            {
                goodspawns.Add(spawn);
            }
        }

        if (goodspawns.Count > 0)
        {
            int rnd = Random.Range(0, goodspawns.Count - 1);
            return goodspawns[rnd];
        }
        else
        {
            throw new System.Exception("No spawns available");
        }
    }
}