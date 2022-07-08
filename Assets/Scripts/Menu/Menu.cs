using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public static Menu Instance;
    public GameObject[] Menus; //0 暫停背景, 1 兩個退出按鈕,2 一般按鈕,3 滾輪
    public GameObject[] Buttons; //0 狀態,1 背包,2 好感度,3設定,4存檔,5讀檔,6任務
    private bool menucount = false;
    private bool exitcount = false; 
    public Scrollbar scrollbar;
    private int count = -1;
    void Awake()
    {
        Menus[0].SetActive(true);
        Bags();
        Menus[2].SetActive(true);
        Menus[3].SetActive(true);
        Buttons[count].SetActive(false);    
        count = -1;
        Menus[0].SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        escape();
    }
    public void escape()
    {
        if(Input.GetKeyDown(KeyCode.Escape)&&count==-1)
        {
            if(!menucount)
            {
                Time.timeScale=0;
                exitcount=false;
            }
            else
            {
                Time.timeScale=1;
                exitcount=false;
            }
            menucount =!menucount;
            Menus[0].SetActive(menucount);
            Menus[1].SetActive(exitcount);         
        } 
        else if(Input.GetKeyDown(KeyCode.Escape)&&count!=-1)
        {
            Menus[2].SetActive(true);
            Menus[3].SetActive(true);
            Buttons[count].SetActive(false);    
            count = -1; 
        }       
    }
    public void Exit()
    {
        exitcount = !exitcount; 
        Menus[1].SetActive(exitcount);        
    }
    public void MenuMove()
    {
        Menus[2].transform.localPosition=new Vector3(0,scrollbar.value*600,0);  
    }
    void MenuClose()
    {
        Menus[1].SetActive(false);
        Menus[2].SetActive(false);
        Menus[3].SetActive(false);
        Buttons[count].SetActive(true);
    }
    public void Status()
    {
        count = 0;
        MenuClose();
    }

    public void Bags()
    {
        count = 1 ;
        MenuClose();
    }
    public void Loves()
    {
        count = 2 ;
        MenuClose();
    }
    public void Config()
    {
        count = 3 ;
        MenuClose();
    }
    public void Save()
    {
        count = 4 ;
        MenuClose();
    }
    public void Load()
    {
        count = 5 ;
        MenuClose();
    }
    public void Tesk()
    {
        count = 6 ;
        MenuClose();
    }
}
