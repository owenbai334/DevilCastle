using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public static class SaveSystem
{

    #region "Json"
    public static void SaveByjson(string SaveFilename,object data)
    {
        if (!Directory.Exists(Application.persistentDataPath + "/game_SaveData"))//若路径下不存在指定文件夹
		{
			Directory.CreateDirectory(Application.persistentDataPath + "/game_SaveData");//创建指定文件夹
		}
        var json = JsonUtility.ToJson(data);
        var path = Path.Combine(Application.persistentDataPath+"/game_SaveData",SaveFilename);

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
        var path = Path.Combine(Application.persistentDataPath+"/game_SaveData",SaveFilename);
        
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
}


