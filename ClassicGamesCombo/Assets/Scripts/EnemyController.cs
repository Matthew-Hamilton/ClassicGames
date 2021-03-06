using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public GameObject ScoreGO;
    public GameObject LivesGO;
    Lives lives;
    Score score;
    public float MoveFrequency = 1;
    public float MoveSpeed = 10;
    public int MoveDistance = 2;

    public float FireRate;

    public int NumAlive;

    public bool CheckDrop = false;

    public List<List<GameObject>> WaveRows;

    private bool destReachedLock = false;

    Camera cam;
    public float height;
    public float width;

    public bool DestReachedLock { get => destReachedLock; set => destReachedLock = value; }

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
                    for (int x = 0; x < WaveRows.Count; x++)
                        foreach (GameObject enemyGO in WaveRows[x])
                        {
                            enemyGO.GetComponent<Enemy>().SetDestination(Vector3.down * MoveDistance);
                            enemyGO.GetComponent<Enemy>().ChangeDirection();
                        }

                    CheckDrop = false;
                    return;
                }
                else
                    foreach (GameObject enemyGO in WaveRows[i])
                    {
                        enemyGO.GetComponent<Enemy>().SetDestination(new Vector3(enemyGO.GetComponent<Enemy>().Direction, 0, 0) * MoveDistance);
                    }


            if (WaveRows[i][0].GetComponent<Enemy>().Direction == -1)
                if (WaveRows[i][0].transform.position.x + MoveDistance < -7)
                {
                    for (int x = 0; x < WaveRows.Count; x++)
                        foreach (GameObject enemyGO in WaveRows[x])
                        {
                            enemyGO.GetComponent<Enemy>().SetDestination(Vector3.down * MoveDistance);
                            enemyGO.GetComponent<Enemy>().ChangeDirection();
                        }

                    CheckDrop = false;
                    return;
                }
                else
                    foreach (GameObject enemyGO in WaveRows[i])
                        enemyGO.GetComponent<Enemy>().SetDestination(new Vector3(enemyGO.GetComponent<Enemy>().Direction, 0, 0) * MoveDistance);


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
                RecalculateFront();
                return;
            }
        }
    }


    public void DestinationReached(GameObject enemy)
    {

        for (int i = 0; i < WaveRows.Count; i++)
        {
            if(WaveRows[i].Contains(enemy))
            {
                for (int j = 0; j < WaveRows[i].Count; j++)
                {
                    NumAlive--;
                    Destroy(WaveRows[i][j]);
                    WaveRows[i].RemoveAt(j);
                    score.AddScore(-30);
                }

                if (WaveRows[i].Count <= 0)
                {
                    WaveRows.RemoveAt(i);
                }
                break;
            }
        }


        if (transform.gameObject.TryGetComponent<Lives>(out lives))
        {
            Debug.Log("Life lost");
            lives.RemoveLife();
        }

        RecalculateFront();
        destReachedLock = false;
    }

    public void RecalculateFront()
    {
        List<int> filledPositions = new List<int>();
        for(int y = 0; y < WaveRows.Count; y++)
        {
            for (int x = 0; x < WaveRows[(WaveRows.Count-1) - y].Count; x++)
            {
                if(!filledPositions.Contains(WaveRows[(WaveRows.Count -1) - y][x].GetComponent<Enemy>().GetNumInRow()))
                {
                    filledPositions.Add(WaveRows[(WaveRows.Count - 1) - y][x].GetComponent<Enemy>().GetNumInRow());
                    WaveRows[(WaveRows.Count - 1) - y][x].GetComponent<Enemy>().SetFront(true);
                }
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
