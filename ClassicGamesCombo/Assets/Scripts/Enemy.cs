using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Enemy : MonoBehaviour
{

    int Health;

    public GameObject _EnemyController;
    EnemyController Controller;
    int Direction;
    Vector3 Destination;

    float MoveTimer;


    // Start is called before the first frame update
    void Start()
    {
        Direction = 1;
        GetComponent<SpriteRenderer>().color = Color.red;
        Controller = _EnemyController.GetComponent<EnemyController>();
        Destination = transform.position;

    }

    private void FixedUpdate()
    {

        if (MoveTimer <= 0)
        {

            //Something wrong here, It goes off the screen
            Destination = transform.position + new Vector3(Direction * Controller.MoveDistance, 0, 0);
            Debug.Log(Destination);

            //Check the enemy position to see if it is off the screen
            if (Destination.x  > 8 || Destination.x < -8)
            {
                Destination = transform.position + new Vector3(0, -1 * Controller.MoveDistance, 0);
                Direction = -Direction;
                Debug.Log(Destination);
            }

            MoveTimer = Controller.MoveFrequency;

        }

        if ((transform.position - Destination).magnitude > 0.05)
        {
            transform.position += (Destination - transform.position).normalized * Controller.MoveSpeed * Time.deltaTime;
        }
        else
            transform.position = Destination;

        MoveTimer -= Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
