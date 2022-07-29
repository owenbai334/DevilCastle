using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public Player player;
    public GameSaveManager GameSave;
    public static bool canload = false;    
    void Start()
    {
        if(canload)
        {
            canload=false;
            player.Load();
            GameSave.LoadGame();
        }  
    }
    void Update()
    {
        if(canload)
        {
            canload=false;
            player.Load();
            GameSave.LoadGame();
        } 
    }
}
