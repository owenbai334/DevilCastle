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
    public static float usevalue;
    public static int weaponcount;
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
    //魔法攻擊
    public GameObject[] magicPrefab;
    //轉向
    private SpriteRenderer sr;
    public Sprite[] PlayerSprites;
    //生命
    static float TotalHp;
    public Slider hpSlider;
    float TotalMp;
    public Slider mpSlider;
    //狀態監控
    public Text[] Status; //0 hp,1 攻擊,2防禦,3 mp,4速度,5無敵狀態
    //無敵狀態
    public float Defendedtime=3;
    float DefendedtimeVal=0;
    //死亡
    public GameObject[] Menus; //0 背景,1滾輪,2按鈕,3死亡
    //武器使用數值
    public float Fardamage = 0;
    public float MagicUse = 0;
    public int Farcount = 0;
    public int CloseCount = 0;
    public int MagicCount = 0;
    //玩家數值
    public float hp = 100;
    public float mp = 100;
    public float atk = 20;
    public float def = 10;
    public float moveSpeed = 10;
    public bool isDefended= false;
    public static Player Instance;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        TotalHp = hp;
        TotalMp = mp;
        hpSlider = GetComponentInChildren<Slider>();
        Status[0].text = "HP:"+hp.ToString();
        Status[1].text = "攻擊力:"+atk.ToString();
        Status[2].text = "防禦力:"+def.ToString();
        Status[3].text = "mp:"+mp.ToString();
        Status[4].text = "移動速度:"+moveSpeed.ToString();
        Status[5].text = "無敵狀態:無";
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        if(isDefended)
        {
            Defended();
        }
        if(AttackTime>AttackTimeval)
        {
            MagicAttack();
            FarAttack();
            CloseAttack();
        }
        else
        {
            AttackTime += Time.deltaTime;
        }  
        
    }
    void Defended()
    {
        if(DefendedtimeVal>=Defendedtime)
        {
            DefendedtimeVal=0;
            isDefended=false;
        }
        else
        {
            DefendedtimeVal+=Time.deltaTime;
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
            closeEulerAngles = new Vector3(0,180,0);
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
            if(Fardamage==0)
            {
                return;
            }
            Ondamage(Fardamage);
            Bullet bullet = Instantiate(farPrefab[Farcount],transform.position,Quaternion.Euler(transform.eulerAngles+farEulerAngles)).GetComponent<Bullet>();
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
            CloseWeapean closeweapean = Instantiate(closePrefab[CloseCount],RealPosition.position,Quaternion.Euler(transform.eulerAngles+closeEulerAngles)).GetComponent<CloseWeapean>();
            closeweapean.damage=atk;
            AttackTime=0;
        }
    }
    void MagicAttack()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            if(mp<=0)
            {
                return;
            }
            if(MagicUse==0)
            {
                return;
            }
            MpLose(MagicUse);
            MagicWeapon magicweapon = Instantiate(magicPrefab[MagicCount],transform.position,Quaternion.Euler(transform.eulerAngles+farEulerAngles)).GetComponent<MagicWeapon>();
            magicweapon.damage=atk;
            AttackTime=0;
        }
    }
    public void Ondamage(float damage)
    {
        if(isDefended)
        {
            return;
        }
        damage-=def;
        if(damage<=0)
        {
            damage=0;
        }
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
    public void MpLose(float mplose)
    {
        mp-=mplose;
        if(mp>=TotalMp)
        {
            mp=TotalMp;
        }
        else if(mp<=0)
        {
            mp=0;
        }
        mpSlider.value = mp/TotalMp;
        Status[3].text = "MP:"+mp.ToString();
        
    }
    public void Die()
    {
        Time.timeScale=0;
        Menus[0].SetActive(true);
        Menus[1].SetActive(false);
        Menus[2].SetActive(false);
        Menus[3].SetActive(true);
        Destroy(gameObject);
    }
    public void ItemUse()
    {
        PlayerStatus(ID,value,usevalue,weaponcount);
        InventoryManager.UseItem(thisItem,itemnum,ID);
    }
    public void ItemTrash()
    {
        InventoryManager.TrashItem(thisItem,ID);
    }
    public void PlayerStatus(float Idtype,float value,float usevalue,int weaponcount)
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
                    Fardamage = usevalue;
                    Farcount = weaponcount;
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
                    CloseCount = weaponcount;
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
                    MagicUse = usevalue;
                    MagicCount = weaponcount;
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
                    MpLose(-value);
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
                    Status[5].text = "無敵狀態:有";
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
                    TotalMp += value;
                    MpLose(0);
                    break;
            }
        }
    }
}
