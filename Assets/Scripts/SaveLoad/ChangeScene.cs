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
            player.Load();
            GameSave.LoadGame();
            canload=false;
        }  
    }
}
