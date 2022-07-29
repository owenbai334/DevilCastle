using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class StartMenu : MonoBehaviour
{
    
    public GameObject[] Menus; //0背景 1按鈕 2設定
    public Scrollbar[] scrollbars; //0背景 1音效
    public Text[] Music; //0背景 1音效
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Menus[0].SetActive(true);
            Menus[1].SetActive(true);
            Menus[2].SetActive(false);
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void LoadGame()
    {
        ChangeScene.canload=true;
        SceneManager.LoadScene("Game");
    }
    public void Confiig()
    {
        Menus[0].SetActive(false);
        Menus[1].SetActive(false);
        Menus[2].SetActive(true);
    }
    public void AboutUs()
    {

    }
    public void Exit()
    {
        Application.Quit();
    }
    public void BackMusic()
    {
        Music[0].text = ((int)(scrollbars[0].value*100)).ToString();
    }

    public void OtherMusic()
    {
        Music[1].text = ((int)(scrollbars[1].value*100)).ToString();
    }
}
