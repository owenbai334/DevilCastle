using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrangeEnemy : MonoBehaviour
{
    //移動
    public float MoveSpeed=10;
    float MoveTime;
    public float AllMovetime = 0.5f;
    //轉身
    private SpriteRenderer sr;
    public Sprite[] OrangeSprites;
    private float SpriteTime;
    //攻擊力
    public float damage =10;
    //生命
    public float hp = 100;
    private float TotalHp;
    public Slider hpSlider;
    public Player player;
    //掉落
    public static OrangeEnemy Instance;
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
        MoveSprite();
    }
    void Move()
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
    void MoveSprite()
    {
        if(SpriteTime<0.2)
        {
            sr.sprite = OrangeSprites[0];
            SpriteTime+=Time.deltaTime;
        }
        else
        {
            sr.sprite = OrangeSprites[1];
            SpriteTime+=Time.deltaTime;
            if(SpriteTime>=0.4)
            {
                SpriteTime=0;
            }
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
        Destroy(gameObject);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag=="Charator")
        {      
            other.gameObject.GetComponent<Player>().Ondamage(damage);
        }
    }
}
