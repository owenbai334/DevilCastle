using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DehpFloor : MonoBehaviour
{
    public float damage = 10;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag=="Charator")
        {      
            other.gameObject.GetComponent<Player>().Ondamage(damage);
        }
    }
}
