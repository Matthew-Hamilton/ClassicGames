using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawn : MonoBehaviour
{
    public GameObject enemyPrefab;

    int roundNumber = 0;
    int numRows = 3;
    int numPerRow = 8;

    


    public bool TimerActive;
    public float RoundBreakLength;
    public float RoundBreakTimer;

    public GameObject enemyController;
    EnemyController Controller;

     void Start()
     {
        TimerActive = true;
        RoundBreakTimer = RoundBreakLength;
        Controller = enemyController.GetComponent<EnemyController>();
     }
    // Update is called once per frame
    void Update()
    {

        if(TimerActive)
        {
            RoundCountDown();
        }

        if(Controller.NumAlive == 0 && !TimerActive)
        {
            StartRoundCountDown();
        }
    }

    void StartRoundCountDown()
    {
        TimerActive = true;
        numRows++;
        numPerRow += 2;
    }

    void RoundCountDown()
    {
        if(RoundBreakTimer <= 0)
        {
            TimerActive = false;
            SpawnWave();
            return;
        }

        RoundBreakTimer -= Time.deltaTime;
    }

    void SpawnWave()
    {
        List<List<GameObject>> EnemyRows = new List<List<GameObject>>();
        for(int y = 0; y < numRows + roundNumber; y++)
        {
            List<GameObject> NewRow = new List<GameObject>();
            for(int x = 0; x< numPerRow + (int)roundNumber/2; x++)
            {
                GameObject newEnemy = Instantiate(enemyPrefab);
                newEnemy.transform.position = new Vector3((-(numPerRow +(int)roundNumber / 2)/2) + 0.5f + x, 7.5f + y, 0);
                newEnemy.GetComponent<Enemy>().SetController(Controller);
                NewRow.Add(newEnemy);
            }
            EnemyRows.Add(NewRow);
            Controller.AddEnemies(NewRow.Count);
            Debug.Log("Row with " + NewRow.Count + " enemies");
            NewRow = new List<GameObject>();
        }
        Controller.SetWave(EnemyRows);
        Debug.Log("Wave with " + EnemyRows.Count + " rows");
        TimerActive = false;
        return;

    }
}
