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
    [HideInInspector]
    public GameObject Win;
    [HideInInspector]
    public Text WinMessage;
    [HideInInspector]
    public GameObject Background;
    public float hp;
    [HideInInspector]
    public float TotalHp;
    [HideInInspector]
    public Slider hpSlider;
    [HideInInspector]
    public bool isDie;
    [HideInInspector]
    public Vector3 position;
    float MoveTime = 0;
    float AllMovetime = 1f;
    [HideInInspector]
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
            Win.SetActive(true);
            WinMessage.text = "恭喜你贏了";
            Time.timeScale=0; 
            Background.SetActive(true);
            Menu.canESC = false;
        }
        Move();
        Jump();
    }
    void Move()
    {
        x = this.gameObject.transform.position.x - Player.gameObject.transform.position.x;
        if(x>0&&x<=20)
        {
            transform.Translate(new Vector3(-MoveSpeed*Time.deltaTime,0,0),Space.World);    
        }
        else if(x<0&&x>=-20)
        {
            transform.Translate(new Vector3(MoveSpeed*Time.deltaTime,0,0),Space.World);
        }
        else if(x>20||x<-20)
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
