using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public static Menu Instance;
    public GameObject OurMenu;
    private int count=0;
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
        if(Input.GetKeyDown(KeyCode.Escape)&&count ==0)
        {
            OurMenu.SetActive(true);
            Time.timeScale=0;
            count=1;
        } 
        else if(Input.GetKeyDown(KeyCode.Escape)&&count ==1)
        {
            OurMenu.SetActive(false);
            Time.timeScale=1;
            count=0;
        }              
    }
}
