using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveSystem
{
    public static void SaveByPlayerPref(string key,object data)
    {
        var json =JsonUtility.ToJson(data);
        PlayerPrefs.SetString(key,json);
        PlayerPrefs.Save();
        Debug.Log("存檔成功");
    }
    public static string LoadByPlayerPref(string key)
    {
        return PlayerPrefs.GetString(key,null);
    }
}
