using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 Direction;

    public GameObject ControllerObj;
    BulletController Controller;

    Camera cam;
    float height;
    float width;

    //-1 = Enemy, 0 = Neutral, 1 = Player
    int Team;


    // Start is called before the first frame update
    void Start()
    {
        Controller = ControllerObj.GetComponent<BulletController>();

        cam = Camera.main;
        height = 2f * cam.orthographicSize;
        width = height * cam.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(Direction.x, Direction.y, 0) * Controller.Speed * Time.deltaTime;
        if (transform.position.y > height / 2)
            Destroy(transform.gameObject);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Bullet on team " + Team + " triggered by " + collision.tag);
        if(Team == 0)
        {
            if(collision.tag == "Player")
            {
                collision.GetComponent<ShipController>().Health -= Controller.Damage;
                Destroy(transform.gameObject);
                return;
            }
            if (collision.tag == "Enemy")
            {
                collision.GetComponent<Enemy>().Health -= Controller.Damage;
                if (collision.GetComponent<Enemy>().Health <= 0)
                    collision.GetComponent<Enemy>().Die();
                Destroy(transform.gameObject);
                return;
            }
            return;
        }
        if (Team == 1)
        {
            if (collision.tag == "Enemy")
            {
                collision.GetComponent<Enemy>().Health -= Controller.Damage;
                if (collision.GetComponent<Enemy>().Health <= 0)
                    collision.GetComponent<Enemy>().Die();
                Destroy(transform.gameObject);
                return;
            }
        }
        if (Team == -1)
        {
            if (collision.tag == "Player")
            {
                collision.GetComponent<ShipController>().Health -= Controller.Damage;
                Destroy(transform.gameObject);
                return;
            }
            return;
        }
    }


    public void SetDirection(Vector2 _Direction)
    {
        Direction = _Direction;
    }

     public void SetTeam(int _Team)
    {
        Team = _Team;

        switch(Team)
        {
            case -1:
                GetComponent<SpriteRenderer>().color = Color.red;
                break;
            case 0:
                GetComponent<SpriteRenderer>().color = Color.white;
                break;
            case 1:
                GetComponent<SpriteRenderer>().color = Color.blue;
                break;
        }
    }

}
