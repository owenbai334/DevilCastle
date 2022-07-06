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
    }
    public void Status()
    {
        count = 0;
        Buttons[0].SetActive(true);
        MenuClose();
    }

    public void Equipment()
    {
        count = 1 ;
        Buttons[1].SetActive(true);
        MenuClose();
    }
    public void Propos()
    {
        count = 2 ;
        Buttons[2].SetActive(true);
        MenuClose();
    }
    public void Config()
    {
        count = 3 ;
        Buttons[3].SetActive(true);
        MenuClose();
    }
    public void Save()
    {
        count = 4 ;
        Buttons[4].SetActive(true);
        MenuClose();
    }
    public void Load()
    {
        count = 5 ;
        Buttons[5].SetActive(true);
        MenuClose();
    }
    public void Tesk()
    {
        count = 6 ;
        Buttons[6].SetActive(true);
        MenuClose();
    }
}
