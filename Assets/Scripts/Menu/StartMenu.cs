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
    
    public GameObject[] Menus; //0背景 1按鈕 2設定 3關於我們 4開始 5繼續
    bool clickStart = false;
    bool clickLoad = false;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Menus[0].SetActive(true);
            Menus[1].SetActive(true);
            Menus[2].SetActive(false);
            Menus[3].SetActive(false);
        }
    }
    public void StartGame()
    {
        clickStart = !clickStart;
        clickLoad = false;
        Menus[4].SetActive(clickStart);
        Menus[5].SetActive(clickLoad);
    }
    public void LoadGame()
    {
        clickLoad = !clickLoad;
        clickStart = false;
        Menus[5].SetActive(clickLoad);
        Menus[4].SetActive(clickStart);
    }
    public void StartChallengeGame()
    {
        ChangeScene.canload=2;
        SceneManager.LoadScene("Game");
    }
    public void LoadChallengeGame()
    {
        ChangeScene.canload=1;
        SceneManager.LoadScene("Game");
    }
    public void StartStory()
    {
        SceneManager.LoadScene("Story");
    }
    public void LoadStory()
    {
        SceneManager.LoadScene("Story");
    }
    public void Confiig()
    {
        Menus[0].SetActive(false);
        Menus[1].SetActive(false);
        Menus[2].SetActive(true);
    }
    public void AboutUs()
    {
        Menus[0].SetActive(false);
        Menus[1].SetActive(false);
        Menus[3].SetActive(true);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
