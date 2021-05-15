using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public int Health = 10;

    [Range(1,50)]
    public float PlayerMoveSpeed;

    public GameObject Bullet;
    [Range(0, 2)]
    public float FireRate;
    float FireTimer;

    Camera cam;
    float height;
    float width;
    // Start is called before the first frame update
    void Start()
    {
        FireTimer = 0;
        cam = Camera.main;
        height = 2f * cam.orthographicSize;
        width = height * cam.aspect;
        GetComponent<SpriteRenderer>().color = Color.blue;
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            Move(new Vector3(-1,0,0));
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            Move(new Vector3(1, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        if((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && FireTimer <= 0)
        {
            Fire();
            FireTimer = FireRate;
        }

        FireTimer -= Time.deltaTime;
    }

    private void Move(Vector3 Dir)
    {
        Debug.Log(width);
        if(Dir.x >0)
        {
            if ((transform.position + Dir * PlayerMoveSpeed * Time.deltaTime).x + 0.25f > width/2)
            {
                transform.position = transform.position + new Vector3(-transform.position.x + width/2 - 0.25f, 0, 0);
                return;
            }
            transform.position += Dir * PlayerMoveSpeed * Time.deltaTime;
            return;
        }
        if(Dir.x<0)
        {

            if ((transform.position + Dir * PlayerMoveSpeed * Time.deltaTime).x - 0.25f < -width / 2)
            {
                transform.position = transform.position + new Vector3(-transform.position.x - width / 2 + 0.25f, 0, 0);
                return;
            }

            transform.position += Dir * PlayerMoveSpeed * Time.deltaTime;
            return;
        }
    }

    void Fire()
    {
        GameObject newBullet = Instantiate(Bullet);
        newBullet.transform.position = transform.position;
        newBullet.GetComponent<Bullet>().SetTeam(1);
        newBullet.GetComponent<Bullet>().SetDirection(Vector2.up);

        FireTimer = FireRate;
    }
}
