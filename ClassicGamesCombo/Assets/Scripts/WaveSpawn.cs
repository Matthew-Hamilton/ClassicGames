using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawn : MonoBehaviour
{
    public GameObject enemyPrefab;

    int roundNumber = 0;
    int numRows = 3;
    int numPerRow = 8;


    bool TimerActive;
    public float RoundBreakLength;
    float RoundBreakTimer;

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

        if(Controller.NumAlive == 0)
        {
            StartRoundCountDown();
        }
    }

    void StartRoundCountDown()
    {
        TimerActive = true;
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
        for(int y = 0; y < numRows + roundNumber; y++)
        {
            for(int x = 0; x< numPerRow + (int)roundNumber/2; x++)
            {
                GameObject newEnemy = Instantiate(enemyPrefab);
                newEnemy.transform.position = new Vector3(-4 + x, 8 + y, 0);
                Controller.AddEnemies(1);
            }
        }
    }
}
