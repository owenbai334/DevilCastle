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
    
    public GameObject[] Menus; //0背景 1按鈕 2設定 3關於我們
    
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
        ChangeScene.canload=2;
        SceneManager.LoadScene("Game");
    }
    public void LoadGame()
    {
        ChangeScene.canload=1;
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
        Menus[0].SetActive(false);
        Menus[1].SetActive(false);
        Menus[3].SetActive(true);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
