using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public float MoveSpeed = 10f;
    float x = 0;
    public float damage = 50;
    public float hp;
    [HideInInspector]
    public float TotalHp;
    public Slider hpSlider;
    [HideInInspector]
    public bool isDie;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        TotalHp = hp;
        hpSlider = GetComponentInChildren<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isDie)
        {
            this.gameObject.SetActive(false);
        }
        Move();
    }
    void Move()
    {
        x = this.gameObject.transform.position.x - Player.gameObject.transform.position.x;
        if(x>0)
        {
            transform.Translate(new Vector3(-MoveSpeed*Time.deltaTime,0,0),Space.World);
        }
        else if(x<0)
        {
            transform.Translate(new Vector3(MoveSpeed*Time.deltaTime,0,0),Space.World);
        }
        else if(x>10||x<-10)
        {
            transform.Translate(new Vector3(0,0,0),Space.World);
        }

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag=="Charator")
        {      
            other.gameObject.GetComponent<Player>().Ondamage(damage);
        }
    }
    public void Ondamage(float damage)
    {
        hp-=damage;
        hpSlider.value =hp/TotalHp;
        if(hp<=0)
        {
            Die();
        }
    }
    void Die()
    {
        isDie = true;
    }
}
