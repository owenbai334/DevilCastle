using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieFloor : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag=="Charator")
        {      
            other.gameObject.GetComponent<Player>().Die();
        }
    }
}
