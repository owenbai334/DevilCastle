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
    public Text[] Status; //0 hp,1 攻擊,2防禦,3 mp
    //玩家數值
    public float hp = 100;
    public float mp = 100;
    public float atk = 100;
    public float def = 100;
    public float moveSpeed = 10;
    public bool isDefended= false;
    static Player player;
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
        InventoryManager.UseItem(thisItem,itemnum);
        // ID = 0;
        // value = 0;
        // thisItem = null;
        // itemnum=null;
    }
    public void PlayerStatus(float Idtype,float value)
    {
        if(Idtype>=10&&Idtype<20)
        { 
            def+=value;
            Status[2].text = "防禦力:"+def.ToString();
            
        }
        else if(Idtype>=20&&Idtype<30)
        {
            atk += value;
            Status[1].text = "攻擊力:"+atk.ToString();
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
