using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public List<AI> EnemyPrefabs;
    public List<Transform> Spawns;

    [Space]
    public float SpawnDelay;

    private bool Playing;
    private int Waves;
    
    public List<AI> Enemies;
    private float Timer;

    private static Transform Points;

    public static Transform Player;
    public static float TimeScale;

    // Use this for initialization
    private void OnEnable()
    {
        Points = transform.GetChild(0);
        Player = GameObject.FindWithTag("Player").transform;
        TimeScale = Time.timeScale;

        Playing = true;
        Enemies = new List<AI>();

        Health.OnDeath += GetKill;
        Health.OnPlayerDeath += Gameover;

        StartCoroutine(_WaveTimer());
        StartCoroutine(_ExtraTimer());
	}

    public static Vector2 GetPoint()
    {
        int rnd = Random.Range(0, Points.childCount - 1);
        return Points.GetChild(rnd).position;
    }

    private void GetKill(AI killed)
    {
        Enemies.Remove(killed);
    }

    private void Gameover()
    {
        Playing = false;
    }

    /// <summary>
    /// Spawns a new wave when there are no enemies left
    /// </summary>
    IEnumerator _WaveTimer()
    {
        yield return new WaitForSeconds(1f);
        while (Playing)
        {
            if (Enemies.Count == 0)
            {
                Timer = 0;
                Waves++;
                StartCoroutine(_SpawnEnemies());
                yield return new WaitForSeconds(2f);
            }

            yield return null;
        }
    }

    /// <summary>
    /// Spawns the next wave if the player is taking too long
    /// </summary>
    IEnumerator _ExtraTimer()
    {
        while (Playing)
        {
            if (Timer > 60)
            {
                Timer = 0;
                Waves++;

                StartCoroutine(_SpawnEnemies());
            }

            Timer += 1 + Waves/10f;
            yield return new WaitForSeconds(1f);
        }
    }

    /// <summary>
    /// For each wave killed, spawn a wave with 1 more enemy than the last
    /// </summary>
    IEnumerator _SpawnEnemies()
    {
        int enemies = Waves;
        while (enemies > 0)
        {
            yield return new WaitForSeconds(1f);
            for (int i = enemies; i > 0 && i > enemies - 5; i--)
            {
                SpawnEnemy();

                yield return new WaitForSeconds(0.25f);
            }

            enemies -= 5;

            foreach (AI enemy in Enemies)
            {
                enemy.transform.parent = null;
            }          
        }
    }

    private AI SpawnEnemy()
    {
        AI enemy = Instantiate(RandomEnemy(0, Waves), RandomSpawn());
        Enemies.Add(enemy);

        return enemy;
    }

    private AI RandomEnemy(int min, int max)
    {
        int rnd = Random.Range(min, Mathf.Clamp(max, 1, 2));
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