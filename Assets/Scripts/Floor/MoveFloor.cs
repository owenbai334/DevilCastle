using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFloor : MonoBehaviour
{
    public float MoveSpeed = 10f;
    float MoveTime;
    public float AllMovetime = 0.5f;
    void Update() 
    {
        Move();
    }
    void Move()
    {
        if(MoveTime<AllMovetime)
        {
            transform.Translate(new Vector3(MoveSpeed*Time.deltaTime,0,0),Space.World);
            MoveTime+=Time.deltaTime;
        }
        else 
        {
            transform.Translate(new Vector3(-MoveSpeed*Time.deltaTime,0,0),Space.World);
            MoveTime+=Time.deltaTime;
            if(MoveTime>=AllMovetime*2)
            {
                MoveTime=0;
            }
        }
    }
}
