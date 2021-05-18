using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject WaveTextGO;
    public GameObject WaveCountDownGO;
    Text WaveText;
    Text WaveCountDownText;

    int roundNumber = 1;
    int numRows = 2;
    int numPerRow = 6;

    


    public bool TimerActive;
    public float RoundBreakLength;
    public float RoundBreakTimer;

    public GameObject enemyController;
    EnemyController Controller;

     void Start()
     {
        WaveText = WaveTextGO.GetComponent<Text>();
        WaveCountDownText = WaveCountDownGO.GetComponent<Text>();
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
        RoundBreakTimer = RoundBreakLength;
        TimerActive = true;
        if(roundNumber % 3 == 0)
            numRows++;
        if(roundNumber % 2 ==0 && numPerRow < 16)
            numPerRow += 2;
        Controller.MoveFrequency *= 0.9f;
    }

    void RoundCountDown()
    {
        if(RoundBreakTimer <= 0)
        {
            TimerActive = false;
            SpawnWave();
            roundNumber++;
            return;
        }
        SetTexts();
        RoundBreakTimer -= Time.deltaTime;
    }

    void SpawnWave()
    {
        DeactivateTexts();
        List<List<GameObject>> EnemyRows = new List<List<GameObject>>();
        for(int y = 0; y < numRows; y++)
        {
            Color rowColour = Color.red/numRows * (y+1) + Color.white / numRows* (numRows -(y+1));
            Debug.Log("Row Colour: " + rowColour);
            List<GameObject> NewRow = new List<GameObject>();
            for(int x = 0; x< numPerRow; x++)
            {
                GameObject newEnemy = Instantiate(enemyPrefab);
                newEnemy.transform.position = new Vector3((-numPerRow/2) + 0.5f + x, 6.5f - y, 0);
                newEnemy.GetComponent<Enemy>().SetColour(rowColour);
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

    void SetTexts()
    {
        if(!WaveTextGO.active)
        {
            WaveTextGO.active = true;
            WaveCountDownGO.active = true;
        }
        WaveText.text = "Round " + roundNumber.ToString();
        WaveCountDownText.text = ((int)RoundBreakTimer +1).ToString();
    }

    void DeactivateTexts()
    {
        WaveTextGO.active = false;
        WaveCountDownGO.active = false;
    }
}
