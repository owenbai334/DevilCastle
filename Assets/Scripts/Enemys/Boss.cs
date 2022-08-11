using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public float MoveSpeed = 10f;
    [HideInInspector]
    public float x = 0;
    [HideInInspector]
    public float y = 0;
    [HideInInspector]
    public bool canJump = true;
    public float jumpSpeed = 4;
    public float damage = 50;
    public float hp;
    [HideInInspector]
    public float TotalHp;
    public Slider hpSlider;
    [HideInInspector]
    public bool isDie;
    public Vector3 position;
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
        Jump();
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
            realmove();
        }
    }
    void Jump()
    {
        y = this.gameObject.transform.position.y - Player.gameObject.transform.position.y;
        if(canJump)
        {
            if(y<0)
            {
                transform.Translate(new Vector2(0,jumpSpeed),Space.World);
            }       
        }
    }
    void realmove()
    {
        float MoveTime = 0;
        float AllMovetime = 0.5f;
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

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag=="Charator")
        {      
            other.gameObject.GetComponent<Player>().Ondamage(damage);
        }
    }
    void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.tag=="Floor"||other.gameObject.tag=="Ice")
        {
            canJump = true;
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.tag=="Floor"||other.gameObject.tag=="Ice")
        {
            canJump = false;
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
