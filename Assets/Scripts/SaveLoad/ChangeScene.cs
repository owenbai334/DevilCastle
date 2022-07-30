using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public Player player;
    public GameSaveManager GameSave;
    public static int canload = 0;    
    void Awake()
    {
        SaveLoad();   
    }
    void Update()
    {
        SaveLoad();
    }
    void SaveLoad()
    {
        if(canload==1)
        {
            canload=0;
            player.Load();
            GameSave.LoadGame();
        }
        if(canload==2)
        {
            canload=0;
            player.Save();
            GameSave.SaveGame();
        } 
    }
}
