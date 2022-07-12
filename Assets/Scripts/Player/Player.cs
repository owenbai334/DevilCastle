using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{   
    //使用物品
    public static int ID;
    public static float value; 
    public static Item thisItem;
    public static Text itemnum;
    //計算裝備數值
    int countHead = 0;
    float tempHead = 0;
    int countBody = 0;
    float tempBody = 0;
    int countShoose = 0;
    float tempShoose = 0;
    int countFar =0;
    float tempFar=0;
    int countClose =0;
    float tempClose=0;
    int countRing =0;
    float tempRing=0;
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
    static float TotalHp;
    public static Slider hpSlider;
    //狀態監控
    public Text[] Status; //0 hp,1 攻擊,2防禦,3 mp,4速度
    //玩家數值
    public float hp = 100;
    public float mp = 100;
    public float atk = 20;
    public float def = 10;
    public float moveSpeed = 10;
    public bool isDefended= false;
    public float Fardamage = 10;
    public static Player Instance;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        TotalHp = hp;
        hpSlider = GetComponentInChildren<Slider>();
        Status[0].text = "HP:"+hp.ToString();
        Status[1].text = "攻擊力:"+atk.ToString();
        Status[2].text = "防禦力:"+def.ToString();
        Status[3].text = "mp:"+mp.ToString();
        Status[4].text = "移動速度:"+moveSpeed.ToString();
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
            Ondamage(Fardamage);
            Bullet bullet = Instantiate(farPrefab[0],transform.position,Quaternion.Euler(transform.eulerAngles+farEulerAngles)).GetComponent<Bullet>();
            bullet.damage=atk;
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
        if(hp>=TotalHp)
        {
            hp=TotalHp;
        }
        hpSlider.value =hp/TotalHp;
        Status[0].text = "HP:"+hp.ToString();
        if(hp<=0)
        {
            Die();
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }
    public void ItemUse()
    {
        PlayerStatus(ID,value);
        InventoryManager.UseItem(thisItem,itemnum,ID);
    }
    public void PlayerStatus(float Idtype,float value)
    {
        if(Idtype>=10&&Idtype<20)
        { 
            switch(Idtype)
            {
                case 10:
                    if(countHead==0)
                    {
                        tempHead = value;
                    }
                    else
                    {
                        def-=tempHead;
                        countHead=-1;
                    }
                    def+=value; 
                    Status[2].text = "防禦力:"+def.ToString(); 
                    countHead++;               
                    break;
                case 11:
                    if(countBody==0)
                    {
                        tempBody = value;
                    }
                    else
                    {
                        def-=tempBody;
                        countBody=-1;
                    }
                    def+=value; 
                    Status[2].text = "防禦力:"+def.ToString(); 
                    countBody++;   
                    break;
                case 12:
                    if(countShoose==0)
                    {
                        tempShoose = value;
                    }
                    else
                    {
                        moveSpeed-=tempShoose;
                        countShoose=-1;
                    }
                    moveSpeed+=value; 
                    Status[4].text = "移動速度:"+moveSpeed.ToString(); 
                    countShoose++; 
                    break;
            }      
        }
        else if(Idtype>=20&&Idtype<30)
        {
            switch(Idtype)
            {
                case 20:
                    if(countFar==0)
                    {
                        tempFar = value;
                    }
                    else
                    {
                        atk-=tempFar;
                        countFar=-1;
                    }
                    atk+=value; 
                    Status[1].text = "攻擊力:"+atk.ToString(); 
                    countFar++; 
                    break;
                case 21:
                    if(countClose==0)
                    {
                        tempClose = value;
                    }
                    else
                    {
                        atk-=tempClose;
                        countClose=-1;
                    }
                    atk+=value; 
                    Status[1].text = "攻擊力:"+atk.ToString(); 
                    countClose++;
                    break;
                case 22:
                    if(countRing==0)
                    {
                        tempRing = value;
                    }
                    else
                    {
                        atk-=tempRing;
                        countRing=-1;
                    }
                    atk+=value; 
                    Status[1].text = "攻擊力:"+atk.ToString(); 
                    countRing++;
                    break;
            }
        }
        else if(Idtype>=30)
        {
            switch(Idtype)
            {
                case 30:
                    Ondamage(-value);
                    break;
                case 31:
                    break;
                case 32:
                    atk += value;
                    Status[1].text = "攻擊力:"+atk.ToString();
                    break;
                case 33:
                    def += value;
                    Status[2].text = "防禦力:"+def.ToString();
                    break;
                case 34:
                    isDefended =true;
                    break;
                case 35:
                    moveSpeed += value;
                    break;
                case 36:
                    TotalHp += value;
                    Ondamage(0);
                    break;
                case 37:
                    break;
            }
        }
    }
}
