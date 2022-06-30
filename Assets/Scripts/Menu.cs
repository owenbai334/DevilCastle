using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public static Menu Instance;
    public GameObject[] Menus;
    private bool menucount = false;
    private bool exitcount = false; 
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
        if(Input.GetKeyDown(KeyCode.Escape)&&menucount==false)
        {
            Menus[0].SetActive(true);
            Menus[1].SetActive(false);
            Time.timeScale=0;
            menucount=true;
        } 
        else if(Input.GetKeyDown(KeyCode.Escape)&&menucount==true)
        {
            Menus[0].SetActive(false);
            Menus[1].SetActive(false);
            Time.timeScale=1;
            exitcount = false;
            menucount=false;
        }              
    }
    public void Exit()
    {
        if(exitcount == false)
        {
            Menus[1].SetActive(true);
            exitcount = true;
        }
        else
        {
            Menus[1].SetActive(false);
            exitcount = false;
        }      
    }
}
