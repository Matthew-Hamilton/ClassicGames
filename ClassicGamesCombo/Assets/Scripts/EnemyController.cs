﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
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

        WaveRows = new List<List<GameObject>>();
        cam = Camera.main;
        height = 2f * cam.orthographicSize;
        width = height * cam.aspect;
    }

    private void FixedUpdate()
    {
        if(CheckDrop)
            DropCheck();
    }

    void DropCheck()
    {
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
                        enemyGO.GetComponent<Enemy>().SetDestination(new Vector3(enemyGO.GetComponent<Enemy>().Direction, 0, 0) * MoveDistance);


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
    }

    public void SetWave(List<List<GameObject>> inputWaves)
    {
        WaveRows = inputWaves;
    }
}
