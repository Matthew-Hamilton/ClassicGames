using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float MoveFrequency = 1;
    public float MoveSpeed = 10;
    public int MoveDistance = 2;

    public float FireRate;


    Camera cam;
    public float height;
    public float width;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        height = 2f * cam.orthographicSize;
        width = height * cam.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
