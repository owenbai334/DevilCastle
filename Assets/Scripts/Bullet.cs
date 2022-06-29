using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveSpeed=10;
    public float damage=10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.up*moveSpeed*Time.deltaTime,Space.World);
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
    }
    void Die()
    {
        Destroy(this.gameObject);
    }
}
