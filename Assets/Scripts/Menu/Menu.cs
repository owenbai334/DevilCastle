using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public static Menu Instance;
    public GameObject[] Menus; //0 暫停背景, 1 滾輪,2 一般按鈕,3 兩個退出按鈕
    public GameObject[] Buttons; //0 狀態,1 背包,2 好感度,3設定,4存檔,5讀檔,6任務
    public Button save;
    public Button load;
    private bool menucount = false;
    private bool exitcount = false; 
    public Scrollbar scrollbar;
    private int count = -1;
    public Player player;
    void Awake()
    {
        Instance = this;
        Bags();
        Buttons[count].SetActive(false);    
        count = -1;
        save.onClick.AddListener(() => player.Save());
        load.onClick.AddListener(() => player.Load());
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
            }
            else
            {
                Time.timeScale=1;              
            }
            exitcount=false;
            menucount =!menucount;
            Menus[0].SetActive(menucount);
            Menus[1].SetActive(menucount);
            Menus[2].SetActive(menucount);
            Menus[3].SetActive(exitcount);         
        } 
        else if(Input.GetKeyDown(KeyCode.Escape)&&count!=-1)
        {
            Menus[1].SetActive(true);
            Menus[2].SetActive(true);
            Buttons[count].SetActive(false);    
            count = -1; 
        }       
    }
    public void Exit()
    {
        exitcount = !exitcount; 
        Menus[3].SetActive(exitcount);        
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
    public void Tesk()
    {
        count = 6 ;
        MenuClose();
    }
}
