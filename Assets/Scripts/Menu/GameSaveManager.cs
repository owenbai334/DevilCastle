using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;//使用输入输出流
using System.Runtime.Serialization.Formatters.Binary;

public class GameSaveManager:MonoBehaviour
{
	public Inventory[] myInventorys; //0其他,1裝備,2武器,3消耗品
	public void SaveGame()
	{

		if (!Directory.Exists(Application.persistentDataPath + "/game_SaveData"))
		{
			Directory.CreateDirectory(Application.persistentDataPath + "/game_SaveData");
		}

		BinaryFormatter formatter = new BinaryFormatter();

		FileStream fileOther = File.Create(Application.persistentDataPath + "/game_SaveData/bagOther.game");
        FileStream fileEquip = File.Create(Application.persistentDataPath + "/game_SaveData/bagEquip.game");
        FileStream fileWeapon = File.Create(Application.persistentDataPath + "/game_SaveData/bagWeapon.game");
        FileStream filePotion = File.Create(Application.persistentDataPath + "/game_SaveData/bagPotion.game");

		var jsonOther = JsonUtility.ToJson(myInventorys[0]);
        var jsonEquip = JsonUtility.ToJson(myInventorys[1]);
		var jsonWeapon = JsonUtility.ToJson(myInventorys[2]);
		var jsonPotion = JsonUtility.ToJson(myInventorys[3]);
    
		formatter.Serialize(fileOther, jsonOther);
        formatter.Serialize(fileEquip, jsonEquip);
        formatter.Serialize(fileWeapon, jsonWeapon);
        formatter.Serialize(filePotion, jsonPotion);

		fileOther.Close();
        fileEquip.Close();
        fileWeapon.Close();
        filePotion.Close();

        Debug.Log("存檔背包成功");
	}

	public void LoadGame()
	{
		BinaryFormatter bf = new BinaryFormatter();

		if (File.Exists(Application.persistentDataPath + "/game_SaveData/bagOther.game"))
		{
            Debug.Log("讀檔背包成功");

			FileStream fileOther = File.Open(Application.persistentDataPath + "/game_SaveData/bagOther.game",FileMode.Open);
            FileStream fileEquip = File.Open(Application.persistentDataPath + "/game_SaveData/bagEquip.game",FileMode.Open);
            FileStream fileWeapon = File.Open(Application.persistentDataPath + "/game_SaveData/bagWeapon.game",FileMode.Open);
            FileStream filePotion = File.Open(Application.persistentDataPath + "/game_SaveData/bagPotion.game",FileMode.Open);

			JsonUtility.FromJsonOverwrite((string)bf.Deserialize(fileOther),myInventorys[0]);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(fileEquip),myInventorys[1]);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(fileWeapon),myInventorys[2]);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(filePotion),myInventorys[3]);

			fileOther.Close();
            fileEquip.Close();
            fileWeapon.Close();
            filePotion.Close();
		}    
        InventoryManager.Refresh();
	}
}