using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public static class SaveSystem
{

    #region "Json"
    public static void SaveByjson(string SaveFilename,object data)
    {
        var json = JsonUtility.ToJson(data);
        var path = Path.Combine(Application.persistentDataPath,SaveFilename);

        try
        {
            File.WriteAllText(path,json);
            #if UNITY_EDITOR
            Debug.Log($"成功儲存在{path}.");
            #endif
        }
        catch(System.Exception expection)
        {
            #if UNITY_EDITOR
            Debug.LogError($"成功儲存在{path}.\n{expection}");
            #endif
        }
    }
    public static T LoadFromJson<T>(string SaveFilename)
    {
        var path = Path.Combine(Application.persistentDataPath,SaveFilename);
        
        try
        {
            var json = File.ReadAllText(path);
            var data = JsonUtility.FromJson<T>(json);
            return data;
        }
        catch(System.Exception expection)
        {
            #if UNITY_EDITOR
            Debug.LogError($"成功儲存在{path}.\n{expection}");
            #endif
            return default;
        }
    }
    #endregion

    #region "delete savefile"
    public static void DeleteSaveFile(string SaveFilename)
    {
        var path = Path.Combine(Application.persistentDataPath,SaveFilename);
        
        try
        {
            File.Delete(path);
        }
        catch(System.Exception expection)
        {
            #if UNITY_EDITOR
            Debug.LogError($"成功儲存在{path}.\n{expection}");
            #endif
        }

    }
    
    #endregion
}


