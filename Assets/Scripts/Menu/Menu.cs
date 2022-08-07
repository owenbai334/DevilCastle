using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public static Menu Instance;
    public GameObject[] Menus; //0 暫停背景, 1 滾輪,2 一般按鈕,3 兩個退出按鈕
    public GameObject[] Buttons; //0 背包,1 狀態,2 設定
    public Text[] Music; //0背景 1音效
    public Button save;
    public Button load;
    private bool menucount = false;
    private bool exitcount = false; 
    public Scrollbar[] scrollbars; //0 滾輪 1背景 2音效
    private int count = -1;
    public GameObject GameLoad;
    void Awake()
    {
        Instance = this;
        Bags();
        Buttons[count].SetActive(false);    
        count = -1;
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
        Menus[2].transform.localPosition=new Vector3(0,scrollbars[0].value*300,0);  
    }
    void MenuClose()
    {
        Menus[1].SetActive(false);
        Menus[2].SetActive(false);
        Menus[3].SetActive(false);
        Buttons[count].SetActive(true);
    }
    public void Bags()
    {
        count = 0;
        MenuClose();
    }

    public void Status()
    {
        count = 1 ;
        MenuClose();
    }
    public void Config()
    {
        count = 2 ;
        MenuClose();
    }
    public void Replay()
    {
        Time.timeScale=1; 
        SceneManager.LoadScene("Game");       
    }

    public void ExitStartMenu()
    {
        Time.timeScale=1; 
        SceneManager.LoadScene("Main");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void BackMusic()
    {
        Music[0].text = ((int)(scrollbars[1].value*100)).ToString();
    }

    public void OtherMusic()
    {
        Music[1].text = ((int)(scrollbars[2].value*100)).ToString();
    }
    public void Save()
    {
        ChangeScene.canload=2;
    }
    public void Load()
    {
        ChangeScene.canload=1;
    }
}
