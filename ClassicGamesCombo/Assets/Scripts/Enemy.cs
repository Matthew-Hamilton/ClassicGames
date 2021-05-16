using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Enemy : MonoBehaviour
{

    int Health;

    public GameObject _EnemyController;
    EnemyController Controller;
    WaveSpawn WaveController;


    public int RowNumber;
    public int NumInRow;
    List<GameObject> myRow;

    public int Direction;
    public Vector3 Destination;

    float MoveTimer;


    // Start is called before the first frame update
    void Start()
    {
        Direction = 1;
        GetComponent<SpriteRenderer>().color = Color.red;
        Controller = _EnemyController.GetComponent<EnemyController>();
        WaveController = _EnemyController.GetComponent<WaveSpawn>();
        Destination = transform.position;

    }

    private void FixedUpdate()
    {

        if (MoveTimer <= 0)
        {
            Move();
        }


        MoveTimer -= Time.deltaTime;
    }

    // Update is called once per frame
    void Move()
    {

        if ((transform.position - Destination).magnitude > 0.05)
        {
            transform.position += (Destination - transform.position).normalized * Controller.MoveSpeed * Time.deltaTime;
        }
        else
        {
            transform.position = Destination;
            Controller.CheckDrop = true;
            MoveTimer = Controller.MoveFrequency;
            Debug.Log("Reset Time To: " +MoveTimer);
        }
    }

    public void SetRowInfo(int rowNum, int numInRow)
    {
        RowNumber = rowNum;
        NumInRow = numInRow;
    }

    public void SetRow(List<GameObject> Row)
    {
        myRow = Row;
    }

    public void ChangeDirection()
    {
        Direction = -Direction;
    }

    public void SetDestination(Vector3 destination)
    {
        Destination = transform.position + destination;
    }
}
