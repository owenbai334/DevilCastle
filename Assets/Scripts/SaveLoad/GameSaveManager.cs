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
	public OrangeEnemy[] orangeEnemy;
	
	const string GAME_DATA = "/game_SaveData/Game.game";
	public void SaveGame()
	{
		if (!Directory.Exists(Application.persistentDataPath + "/game_SaveData"))
		{
			Directory.CreateDirectory(Application.persistentDataPath + "/game_SaveData");
		}
		BinaryFormatter formatter = new BinaryFormatter();

		FileStream fileGame = File.Create(Application.persistentDataPath + GAME_DATA);

		for(int i = 0;i<myInventorys.Length;i++)
		{
			var jsonGame = JsonUtility.ToJson(myInventorys[i]);
			formatter.Serialize(fileGame, jsonGame);
		}
		for(int i = 0;i<images.Length;i++)
		{
			var jsonGame = JsonUtility.ToJson(images[i]);
			formatter.Serialize(fileGame, jsonGame);
		}
		for(int i = 0;i<orangeEnemy.Length;i++)
		{
			var jsonGame = JsonUtility.ToJson(orangeEnemy[i]);
			formatter.Serialize(fileGame, jsonGame);
		}
		
		fileGame.Close();

        Debug.Log("存檔背包成功");
	}

	public void LoadGame()
	{

		BinaryFormatter bf = new BinaryFormatter();

		if (File.Exists(Application.persistentDataPath +GAME_DATA))
		{           
			FileStream fileGame = File.Open(Application.persistentDataPath + GAME_DATA,FileMode.Open);

			for(int i = 0;i<myInventorys.Length;i++)
			{
				JsonUtility.FromJsonOverwrite((string)bf.Deserialize(fileGame),myInventorys[i]);
			}

			for(int i = 0;i<images.Length;i++)
			{
				JsonUtility.FromJsonOverwrite((string)bf.Deserialize(fileGame),images[i]);
			}

			for(int i = 0;i<orangeEnemy.Length;i++)
			{
				JsonUtility.FromJsonOverwrite((string)bf.Deserialize(fileGame),orangeEnemy[i]);
			}

			EnemyData();

			fileGame.Close();

			Debug.Log("讀檔背包成功");
		}    
        InventoryManager.Refresh();
	}

	void EnemyData()
	{
		for(int i=0;i<orangeEnemy.Length;i++)
		{
			orangeEnemy[i].Ondamage(0);
			orangeEnemy[i].Load();
			if(orangeEnemy[i].isDie==false)
			{
				orangeEnemy[i].gameObject.SetActive(true);
			}
		}
	}
}