using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{   
    [System.Serializable]
    class SaveData
    {
        public float TotalHp;
        public float TotalMp;
        public float playerhp;
        public float playermp;
        public float playeratk;
        public float playerdef;
        public float playermoveSpeed;
        public bool playerisDefended;
        public Vector3 playerposition;
        public int countHead;
        public int countBody;
        public int countShoose;
        public int countFar;
        public int countClose;
        public int countRing;
        public float tempHead;
        public float tempBody;
        public float tempShoose;
        public float tempFar;
        public float tempClose;
        public float tempRing;
        public float Fardamage;    
        public float MagicUse;   
        public int Farcount;
        public int CloseCount;
        public int MagicCount;
        public int[] potionNum = new int[18];
        public bool[] isHead = new bool[18];
        public bool[] isBody = new bool[18];
        public bool[] isShoose = new bool[18];
        public bool[] isFar = new bool[18];
        public bool[] isClose = new bool[18];
        public bool[] isRing = new bool[18];
        public float Defendedtime;
        public float DefendedtimeVal;
    }
    const string PLAYER_DATA_FILE_NAME = "PlayerData.game";
    [HideInInspector]
    public Inventory PotionBag;
    [HideInInspector]
    public Inventory[] Equips; //0裝備,1武器
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
    //遠程攻擊
    public GameObject[] farPrefab;
    Vector3 farEulerAngles;
    float AttackTime=0;
    [HideInInspector]
    public float AttackTimeval = 0.4f;
    //近戰攻擊
    public GameObject[] closePrefab;
    Vector3 closeEulerAngles;
    [HideInInspector]
    public Transform[] ClosePosition;
    Transform RealPosition;
    //魔法攻擊
    public GameObject[] magicPrefab;
    //轉向
    private SpriteRenderer sr;
    [HideInInspector]
    public Sprite[] PlayerSprites;
    //生命
    static float TotalHp;
    [HideInInspector]
    public Slider hpSlider;
    float TotalMp;
    [HideInInspector]
    public Slider mpSlider;
    //狀態監控
    [HideInInspector]
    public Text[] Status; //0 hp,1 攻擊,2防禦,3 mp,4速度,5無敵狀態
    //死亡
    // [HideInInspector]
    public GameObject[] Menus; //0 背景,1死亡
    float Fardamage = 0;
    float MagicUse = 0;
    int Farcount = 0;
    int CloseCount = 0;
    int MagicCount = 0;
    //跳躍
    public float jumpSpeed = 100;
    bool canJump = true;
    //玩家數值
    public float hp = 100;
    public float mp = 100;
    public float atk = 20;
    public float def = 10;
    public float moveSpeed = 10;
    //無敵狀態
    [HideInInspector]
    public bool isDefended= false;  
    [HideInInspector]
    public float Defendedtime;
    float DefendedtimeVal = 0;
    public static Player Instance;
    // Start is called before the first frame update
    public void Start()
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
            case "Ice":
                canJump=true; 
                moveSpeed*=2;
                break;
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        switch(other.gameObject.tag)
        {
            case "Ice":
                moveSpeed/=2;
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
        Menus[1].SetActive(true);
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
    #region "使用物品後狀態"
    public void PlayerStatus(float Idtype,float value,float usevalue,int weaponcount)
    {
        if(Idtype>=10&&Idtype<20)
        { 
            switch(Idtype)
            {
                case 10:
                    if(countHead==0)
                    {
                        def-=tempHead;
                        tempHead = value;
                    }
                    else
                    {
                        def-=tempHead;
                        tempHead = value;
                        countHead=-1;
                    }
                    def+=value; 
                    Status[2].text = "防禦力:"+def.ToString(); 
                    countHead++;               
                    break;
                case 11:
                    if(countBody==0)
                    {
                        def-=tempBody;
                        tempBody = value;
                    }
                    else
                    {
                        def-=tempBody;
                        tempBody = value;
                        countBody=-1;
                    }
                    def+=value; 
                    Status[2].text = "防禦力:"+def.ToString(); 
                    countBody++;   
                    break;
                case 12:
                    if(countShoose==0)
                    {
                        moveSpeed-=tempShoose;
                        tempShoose = value;                        
                    }
                    else
                    {
                        moveSpeed-=tempShoose;
                        tempShoose = value;
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
                        atk-=tempFar;
                        tempFar = value;
                    }
                    else
                    {
                        atk-=tempFar;
                        tempFar=value;
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
                        atk-=tempClose;
                        tempClose = value;                    
                    }
                    else
                    {
                        atk-=tempClose;
                        tempClose=value;
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
                        atk-=tempRing;
                        tempRing = value;
                    }
                    else
                    {
                        atk-=tempRing;
                        tempRing = value;
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
                    Defendedtime = value;
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
    #endregion
    #region "存檔讀檔"
    public void Save()
    {
        SaveMyJson();
    }
    public void Load()
    {
        LoadMyJson();
        LoadStart();
    }
    #endregion

    #region "json"

    void SaveMyJson()
    {
        SaveSystem.SaveByjson(PLAYER_DATA_FILE_NAME,SavingData());
    }
    void LoadMyJson()
    {
        var SaveData = SaveSystem.LoadFromJson<SaveData>(PLAYER_DATA_FILE_NAME);
        LoadData(SaveData);
        InventoryManager.Refresh();  
    }
    #endregion

    #region "存檔幫助"
    SaveData SavingData()
    {
        var SaveData = new SaveData();
        SaveData.playerhp=hp;
        SaveData.playermp=mp;
        SaveData.playeratk=atk;
        SaveData.playerdef=def;
        SaveData.playermoveSpeed=moveSpeed;
        SaveData.playerisDefended=isDefended;
        SaveData.playerposition=transform.position;
        SaveData.countHead = countHead;  
        SaveData.countBody = countBody;
        SaveData.countShoose = countShoose;
        SaveData.countFar = countFar;
        SaveData.countClose = countClose;
        SaveData.countRing = countRing;
        SaveData.tempHead = tempHead;     
        SaveData.tempBody = tempBody; 
        SaveData.tempShoose = tempShoose; 
        SaveData.tempFar = tempFar; 
        SaveData.tempClose = tempClose; 
        SaveData.tempRing = tempRing;     
        SaveData.Fardamage = Fardamage;
        SaveData.MagicUse = MagicUse;
        SaveData.Farcount = Farcount;
        SaveData.CloseCount = CloseCount;
        SaveData.MagicCount = MagicCount;
        SaveData.TotalHp = TotalHp;
        SaveData.TotalMp = TotalMp;
        SaveData.Defendedtime = Defendedtime;
        SaveData.DefendedtimeVal = DefendedtimeVal;
        for(int i=0;i<PotionBag.itemList.Count;i++)
        {
            if(PotionBag.itemList[i]==null)
            {
                continue;
            }
            SaveData.potionNum[i]=PotionBag.itemList[i].ItemNum;
        }
        for(int i=0;i<Equips[0].itemList.Count;i++)
        {
            if(Equips[0].itemList[i]==null)
            {
                continue;
            }
            SaveData.isHead[i]=Equips[0].itemList[i].isHead;
            SaveData.isBody[i]=Equips[0].itemList[i].isBody;
            SaveData.isShoose[i]=Equips[0].itemList[i].isShoose;
        }
        for(int i=0;i<Equips[1].itemList.Count;i++)
        {
            if(Equips[1].itemList[i]==null)
            {
                continue;
            }
            SaveData.isFar[i]=Equips[1].itemList[i].isFar;
            SaveData.isClose[i]=Equips[1].itemList[i].isClose;
            SaveData.isRing[i]=Equips[1].itemList[i].isRing;
        }
        return SaveData;
    }
    void LoadData(SaveData saveData)
    {
        hp = saveData.playerhp;
        mp = saveData.playermp;
        atk = saveData.playeratk;
        def = saveData.playerdef;
        moveSpeed = saveData.playermoveSpeed;
        isDefended = saveData.playerisDefended;
        transform.position = saveData.playerposition;
        countHead = saveData.countHead;  
        countBody = saveData.countBody;  
        countShoose = saveData.countShoose;  
        countFar = saveData.countFar;  
        countClose = saveData.countClose;  
        countRing = saveData.countRing;  
        tempHead = saveData.tempHead; 
        tempBody = saveData.tempBody; 
        tempShoose = saveData.tempShoose; 
        tempFar = saveData.tempFar; 
        tempClose = saveData.tempClose; 
        tempRing = saveData.tempRing; 
        Fardamage = saveData.Fardamage;
        MagicUse = saveData.MagicUse;
        Farcount= saveData.Farcount;
        CloseCount= saveData.CloseCount;
        MagicCount= saveData.MagicCount;
        TotalHp = saveData.TotalHp;
        TotalMp = saveData.TotalMp;
        Defendedtime = saveData.Defendedtime;
        DefendedtimeVal = saveData.DefendedtimeVal;
        for(int i=0;i<PotionBag.itemList.Count;i++)
        {
            if(PotionBag.itemList[i]==null)
            {
                continue;
            }
            PotionBag.itemList[i].ItemNum=saveData.potionNum[i];
        }
        for(int i=0;i<Equips[0].itemList.Count;i++)
        {
            if(Equips[0].itemList[i]==null)
            {
                continue;
            }
            Equips[0].itemList[i].isHead=saveData.isHead[i];
            Equips[0].itemList[i].isBody=saveData.isBody[i];
            Equips[0].itemList[i].isShoose=saveData.isShoose[i];
        }
        for(int i=0;i<Equips[1].itemList.Count;i++)
        {
            if(Equips[1].itemList[i]==null)
            {
                continue;
            }
            Equips[1].itemList[i].isFar=saveData.isFar[i];
            Equips[1].itemList[i].isClose=saveData.isClose[i];
            Equips[1].itemList[i].isRing=saveData.isRing[i];
        }
    }
    void LoadStart()
    {
        sr = GetComponent<SpriteRenderer>();
        Ondamage(0);
        MpLose(0);
        hpSlider = GetComponentInChildren<Slider>();
        Status[0].text = "HP:"+hp.ToString();
        Status[1].text = "攻擊力:"+atk.ToString();
        Status[2].text = "防禦力:"+def.ToString();
        Status[3].text = "mp:"+mp.ToString();
        Status[4].text = "移動速度:"+moveSpeed.ToString();
        Status[5].text = "無敵狀態:無";
    }
    #endregion
}
