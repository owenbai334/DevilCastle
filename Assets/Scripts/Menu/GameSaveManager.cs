using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;//使用输入输出流
using System.Runtime.Serialization.Formatters.Binary;//二进制序列化

public class GameSaveManager:MonoBehaviour
{
	public Inventory[] myInventorys; //0其他,1裝備,2武器,3消耗品
	public void SaveGame()
	{

		if (!Directory.Exists(Application.persistentDataPath + "/game_SaveData"))//若路径下不存在指定文件夹
		{
			Directory.CreateDirectory(Application.persistentDataPath + "/game_SaveData");//创建指定文件夹
		}

		BinaryFormatter formatter = new BinaryFormatter();//二进制序列化

		FileStream fileOther = File.Create(Application.persistentDataPath + "/game_SaveData/bagOther.txt");//创建存储文件inventory
        FileStream fileEquip = File.Create(Application.persistentDataPath + "/game_SaveData/bagEquip.txt");//创建存储文件inventory
        FileStream fileWeapon = File.Create(Application.persistentDataPath + "/game_SaveData/bagWeapon.txt");//创建存储文件inventory
        FileStream filePotion = File.Create(Application.persistentDataPath + "/game_SaveData/bagPotion.txt");//创建存储文件inventory

		var jsonOther = JsonUtility.ToJson(myInventorys[0]);//将ScriptableObject(只读)转换为Json存储方法
        var jsonEquip = JsonUtility.ToJson(myInventorys[1]);//将ScriptableObject(只读)转换为Json存储方法
		var jsonWeapon = JsonUtility.ToJson(myInventorys[2]);//将ScriptableObject(只读)转换为Json存储方法
		var jsonPotion = JsonUtility.ToJson(myInventorys[3]);//将ScriptableObject(只读)转换为Json存储方法

    
		formatter.Serialize(fileOther, jsonOther);//将json序列化到file中
        formatter.Serialize(fileEquip, jsonEquip);//将json序列化到file中
        formatter.Serialize(fileWeapon, jsonWeapon);//将json序列化到file中
        formatter.Serialize(filePotion, jsonPotion);//将json序列化到file中

		fileOther.Close();//关闭文件流
        fileEquip.Close();
        fileWeapon.Close();
        filePotion.Close();
        Debug.Log("存檔背包成功");
	}

	public void LoadGame()
	{
		BinaryFormatter bf = new BinaryFormatter();

		if (File.Exists(Application.persistentDataPath + "/game_SaveData/bagOther.txt"))//若指定文件存在
		{
            Debug.Log("讀檔背包成功");
			FileStream fileOther = File.Open(Application.persistentDataPath + "/game_SaveData/bagOther.txt",FileMode.Open);//打开指定文件
            FileStream fileEquip = File.Open(Application.persistentDataPath + "/game_SaveData/bagEquip.txt",FileMode.Open);//打开指定文件
            FileStream fileWeapon = File.Open(Application.persistentDataPath + "/game_SaveData/bagWeapon.txt",FileMode.Open);//打开指定文件
            FileStream filePotion = File.Open(Application.persistentDataPath + "/game_SaveData/bagPotion.txt",FileMode.Open);//打开指定文件

			//通过JsonUtility中的方法重写Inventory
			JsonUtility.FromJsonOverwrite((string)bf.Deserialize(fileOther),myInventorys[0]);//将存储的数据反序列化并重写到Inventory中
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(fileEquip),myInventorys[1]);//将存储的数据反序列化并重写到Inventory中
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(fileWeapon),myInventorys[2]);//将存储的数据反序列化并重写到Inventory中
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(filePotion),myInventorys[3]);//将存储的数据反序列化并重写到Inventory中

			fileOther.Close();//关闭文件流
            fileEquip.Close();
            fileWeapon.Close();
            filePotion.Close();
		}    
        InventoryManager.Refresh();
	}
}