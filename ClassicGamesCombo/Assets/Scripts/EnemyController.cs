using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public GameObject ScoreGO;
    Score score;
    public float MoveFrequency = 1;
    public float MoveSpeed = 10;
    public int MoveDistance = 2;

    public float FireRate;

    public int NumAlive;

    public bool CheckDrop = false;

    public List<List<GameObject>> WaveRows;

    Camera cam;
    public float height;
    public float width;

    // Start is called before the first frame update
    void Start()
    {
        score = ScoreGO.GetComponent<Score>();
        WaveRows = new List<List<GameObject>>();
        cam = Camera.main;
        height = 2f * cam.orthographicSize;
        width = height * cam.aspect;
    }

    private void FixedUpdate()
    {
        if (CheckDrop)
        {
            DropCheck();
            Debug.Log("Please");
        }
    }

    void DropCheck()
    {
        Debug.Log("This Called");
        for (int i = 0; i < WaveRows.Count; i++)
        {
            if (WaveRows[i][0].GetComponent<Enemy>().Direction == 1)
                if (WaveRows[i][WaveRows[i].Count - 1].transform.position.x + MoveDistance > 9)
                {
                    foreach (GameObject enemyGO in WaveRows[i])
                    {
                        enemyGO.GetComponent<Enemy>().SetDestination(Vector3.down * MoveDistance);
                        enemyGO.GetComponent<Enemy>().ChangeDirection();
                    }
                    continue;
                }
                else
                    foreach (GameObject enemyGO in WaveRows[i])
                    {
                        enemyGO.GetComponent<Enemy>().SetDestination(new Vector3(enemyGO.GetComponent<Enemy>().Direction, 0, 0) * MoveDistance);
                        Debug.Log("Called");
                    }


            if (WaveRows[i][0].GetComponent<Enemy>().Direction == -1)
                if (WaveRows[i][0].transform.position.x + MoveDistance < -7)
                {
                    foreach (GameObject enemyGO in WaveRows[i])
                    {
                        enemyGO.GetComponent<Enemy>().SetDestination(Vector3.down * MoveDistance);
                        enemyGO.GetComponent<Enemy>().ChangeDirection();
                    }
                    continue;
                }
                else
                    foreach (GameObject enemyGO in WaveRows[i])
                        enemyGO.GetComponent<Enemy>().SetDestination(new Vector3(enemyGO.GetComponent<Enemy>().Direction, 0, 0) * MoveDistance);


            Debug.Log("Set New Destinations");

        }
        CheckDrop = false;
    }

    public void AddEnemies(int newEnemies)
    {
        NumAlive += newEnemies;
    }

    public void ReportDeath(Enemy deadEnemys)
    {

        NumAlive--;
        for (int i = 0; i < WaveRows.Count; i++)
        {
            if (WaveRows[i].Contains(deadEnemys.transform.gameObject))
            {
                WaveRows[i].Remove(deadEnemys.transform.gameObject);
                score.AddScore(10);
                if (WaveRows[i].Count <= 0)
                {
                    WaveRows.RemoveAt(i);
                }
                return;
            }
        }
    }

    public void SetWave(List<List<GameObject>> inputWaves)
    {
        WaveRows = inputWaves;
    }

    public void SetCheckDrop()
    {
        CheckDrop = true;
    }
}
