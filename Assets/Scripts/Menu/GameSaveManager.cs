using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class GameSaveManager:MonoBehaviour
{
	public Inventory[] myInventorys; //0其他,1裝備,2武器,3消耗品
	public Image[] images; //0 head,1 body,2 shoose,3 far,4 close,5 ring
	const string GAME_DATA = "/game_SaveData/Game.game";
	public void SaveGame()
	{
		if (!Directory.Exists(Application.persistentDataPath + "/game_SaveData"))
		{
			Directory.CreateDirectory(Application.persistentDataPath + "/game_SaveData");
		}
		BinaryFormatter formatter = new BinaryFormatter();

		FileStream fileGame = File.Create(Application.persistentDataPath +GAME_DATA);
        // FileStream fileEquip = File.Create(Application.persistentDataPath + "/game_SaveData/bagEquip.game");
        // FileStream fileWeapon = File.Create(Application.persistentDataPath + "/game_SaveData/bagWeapon.game");
        // FileStream filePotion = File.Create(Application.persistentDataPath + "/game_SaveData/bagPotion.game");
		// FileStream fileHead = File.Create(Application.persistentDataPath + "/game_SaveData/headImage.game");
		// FileStream fileBody = File.Create(Application.persistentDataPath + "/game_SaveData/bodyImage.game");
		// FileStream fileShoose = File.Create(Application.persistentDataPath + "/game_SaveData/shooseImage.game");
		// FileStream fileFar = File.Create(Application.persistentDataPath + "/game_SaveData/farImage.game");
		// FileStream fileClose = File.Create(Application.persistentDataPath + "/game_SaveData/closeImage.game");
		// FileStream fileRing = File.Create(Application.persistentDataPath + "/game_SaveData/ringImage.game");

		var jsonOther = JsonUtility.ToJson(myInventorys[0]);
        var jsonEquip = JsonUtility.ToJson(myInventorys[1]);
		var jsonWeapon = JsonUtility.ToJson(myInventorys[2]);
		var jsonPotion = JsonUtility.ToJson(myInventorys[3]);
		var jsonHead = JsonUtility.ToJson(images[0]);
		var jsonBody = JsonUtility.ToJson(images[1]);
		var jsonShoose = JsonUtility.ToJson(images[2]);
		var jsonFar = JsonUtility.ToJson(images[3]);
		var jsonClose = JsonUtility.ToJson(images[4]);
		var jsonRing = JsonUtility.ToJson(images[5]);
    
		formatter.Serialize(fileGame, jsonOther);
        formatter.Serialize(fileGame, jsonEquip);
        formatter.Serialize(fileGame, jsonWeapon);
        formatter.Serialize(fileGame, jsonPotion);
		formatter.Serialize(fileGame, jsonHead);
		formatter.Serialize(fileGame, jsonBody);
		formatter.Serialize(fileGame, jsonShoose);
		formatter.Serialize(fileGame, jsonFar);
		formatter.Serialize(fileGame, jsonClose);
		formatter.Serialize(fileGame, jsonRing);

		fileGame.Close();
        // fileEquip.Close();
        // fileWeapon.Close();
        // filePotion.Close();
		// fileHead.Close();
		// fileBody.Close();
		// fileShoose.Close();
		// fileFar.Close();
		// fileClose.Close();
		// fileRing.Close();

        Debug.Log("存檔背包成功");
	}

	public void LoadGame()
	{

		BinaryFormatter bf = new BinaryFormatter();

		if (File.Exists(Application.persistentDataPath +GAME_DATA))
		{           
			FileStream fileGame = File.Open(Application.persistentDataPath +GAME_DATA,FileMode.Open);
            // FileStream fileEquip = File.Open(Application.persistentDataPath + "/game_SaveData/bagEquip.game",FileMode.Open);
            // FileStream fileWeapon = File.Open(Application.persistentDataPath + "/game_SaveData/bagWeapon.game",FileMode.Open);
            // FileStream filePotion = File.Open(Application.persistentDataPath + "/game_SaveData/bagPotion.game",FileMode.Open);
			// FileStream fileHead = File.Open(Application.persistentDataPath + "/game_SaveData/headImage.game",FileMode.Open);
			// FileStream fileBody = File.Open(Application.persistentDataPath + "/game_SaveData/bodyImage.game",FileMode.Open);
			// FileStream fileShoose = File.Open(Application.persistentDataPath + "/game_SaveData/shooseImage.game",FileMode.Open);
			// FileStream fileFar = File.Open(Application.persistentDataPath + "/game_SaveData/farImage.game",FileMode.Open);
			// FileStream fileClose = File.Open(Application.persistentDataPath + "/game_SaveData/closeImage.game",FileMode.Open);
			// FileStream fileRing = File.Open(Application.persistentDataPath + "/game_SaveData/ringImage.game",FileMode.Open);

			JsonUtility.FromJsonOverwrite((string)bf.Deserialize(fileGame),myInventorys[0]);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(fileGame),myInventorys[1]);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(fileGame),myInventorys[2]);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(fileGame),myInventorys[3]);
			JsonUtility.FromJsonOverwrite((string)bf.Deserialize(fileGame),images[0]);
			JsonUtility.FromJsonOverwrite((string)bf.Deserialize(fileGame),images[1]);
			JsonUtility.FromJsonOverwrite((string)bf.Deserialize(fileGame),images[2]);
			JsonUtility.FromJsonOverwrite((string)bf.Deserialize(fileGame),images[3]);
			JsonUtility.FromJsonOverwrite((string)bf.Deserialize(fileGame),images[4]);
			JsonUtility.FromJsonOverwrite((string)bf.Deserialize(fileGame),images[5]);

			fileGame.Close();
        	// fileEquip.Close();
        	// fileWeapon.Close();
        	// filePotion.Close();
			// fileHead.Close();
			// fileBody.Close();
			// fileShoose.Close();
			// fileFar.Close();
			// fileClose.Close();
			// fileRing.Close();

			Debug.Log("讀檔背包成功");
		}    
        InventoryManager.Refresh();
	}
}