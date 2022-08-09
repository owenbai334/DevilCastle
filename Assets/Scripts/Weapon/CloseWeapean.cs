using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseWeapean : MonoBehaviour
{
    public float damage;
    void Update()
    {
        transform.Rotate(0,0,-90*Time.deltaTime*4);
        Die();
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
        Destroy(gameObject,0.4f);
    }
}
