using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float movespeed=10;
    public float damage;
    void Update()
    {
        transform.Translate(transform.up*movespeed*Time.deltaTime,Space.World);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag=="Enemy")
        {
            switch(col.gameObject.layer)
            {
                case 6:
                    col.GetComponent<OrangeEnemy>().Ondamage(damage);
                    break;
            }          
            Die();
        }
       else if(col.gameObject.tag=="Boss")
        {
            col.GetComponent<Boss>().Ondamage(damage);         
            Die();  
        }
    }
    void Die()
    {
        Destroy(this.gameObject);
    }
}
