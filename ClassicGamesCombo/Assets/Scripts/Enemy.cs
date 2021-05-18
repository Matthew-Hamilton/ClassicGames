using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Enemy : MonoBehaviour
{

    public int Health;

    public GameObject _EnemyController;
    EnemyController Controller;
    WaveSpawn WaveController;

    public Color myColour;

    public int Direction;
    public Vector3 Destination;

    float MoveTimer;


    // Start is called before the first frame update
    void Start()
    {
        Direction = 1;
        GetComponent<SpriteRenderer>().color = Color.red;
        WaveController = _EnemyController.GetComponent<WaveSpawn>();
        MoveTimer = Controller.MoveFrequency;
        Destination = transform.position + new Vector3(Direction * Controller.MoveDistance,0,0);

    }

    private void FixedUpdate()
    {
        if(transform.gameObject.GetComponent<SpriteRenderer>().color != myColour)
        {
            transform.gameObject.GetComponent<SpriteRenderer>().color = myColour;
        }
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
            transform.position += (Destination - transform.position).normalized * Controller.MoveSpeed * Time.fixedDeltaTime;
        }
        else
        {
            transform.position = Destination;
            Debug.Log("Destination: " +Destination);
            Controller.SetCheckDrop();
            MoveTimer = Controller.MoveFrequency;
        }
    }

    public void ChangeDirection()
    {
        Direction = -Direction;
    }

    public void SetDestination(Vector3 destination)
    {
        Destination = transform.position + destination;
    }

    public void SetController(EnemyController controller)
    {
        Controller = controller;
    }

    public void SetColour(Color color)
    {
        myColour = color;
        transform.gameObject.GetComponent<SpriteRenderer>().color = color;
    }

    public void Die()
    {
        Controller.ReportDeath(this);

        Destroy(transform.gameObject);
    }
}
