using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //移動速度
    public float moveSpeed = 10;
    //跳躍
    public float jumpSpeed = 100;
    private bool canJump = true;
    //遠程攻擊
    public GameObject[] farPrefab;
    private Vector3 farEulerAngles;
    private float AttackTime=0;
    public float AttackTimeval = 0.4f;
    //近戰攻擊
    public GameObject[] closePrefab;
    private Vector3 closeEulerAngles;
    public Transform[] ClosePosition;
    private Transform RealPosition;
    //轉向
    private SpriteRenderer sr;
    public Sprite[] PlayerSprites;
    //生命
    public float hp = 100;
    private float TotalHp;
    public Slider hpSlider;
    public static Player Instance;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        TotalHp = hp;
        hpSlider = GetComponentInChildren<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        CloseAttack();
        if(AttackTime>AttackTimeval)
        {
            FarAttack();
        }
        else{
            AttackTime += Time.deltaTime;
        }  
        
    }

    void Move()
    {
        if(Input.GetKey(KeyCode.A))
        {
            sr.sprite = PlayerSprites[0];
            RealPosition = ClosePosition[1];
            transform.Translate(new Vector3(-moveSpeed*Time.deltaTime,0,0),Space.World);
            farEulerAngles = new Vector3(0,0,90);
            closeEulerAngles = new Vector3(0,0,180);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            sr.sprite = PlayerSprites[1];
            RealPosition = ClosePosition[0];
            transform.Translate(new Vector3(moveSpeed*Time.deltaTime,0,0),Space.World);
            farEulerAngles = new Vector3(0,0,-90);
            closeEulerAngles = new Vector3(0,0,0);
        }
    }
    void FarAttack()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            Instantiate(farPrefab[0],transform.position,Quaternion.Euler(transform.eulerAngles+farEulerAngles));
            AttackTime = 0;
        }
    } 
    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            if(canJump)
            {
                canJump=false;
                transform.Translate(0,jumpSpeed*Time.deltaTime,0);
            }           
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        switch(other.gameObject.tag)
        {
            case "Floor":
                canJump=true;         
                break;
        }
    }
    void CloseAttack()
    {
         if(Input.GetKeyDown(KeyCode.K))
        {
            Instantiate(closePrefab[0],RealPosition.position,Quaternion.Euler(transform.eulerAngles+closeEulerAngles));
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
        Destroy(this.gameObject);
    }
}
