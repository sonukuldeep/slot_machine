using System.IO;
using UnityEngine;
using System.Collections.Generic;

namespace SaveSystemCore
{
	public class GameData

	{
		public List<int> lastTwentyScores = new List<int>();
		public bool gameSound;
		public bool gameMusic;

		public GameData()
        {
			lastTwentyScores = PersistentData.lastTwentyScores;
			gameSound = PersistentData.gameSound;
			gameMusic = PersistentData.gameMusic;
        }

	}

	public static class SaveSystem
	{
		
		private static string path = Application.persistentDataPath + "/player.data";
		private static string password = "autobattlepets";

        public static void SavePlayer()
		{
			//copy persistance file
			GameData data = new GameData();
			
			//creating json
			string jsonData = JsonUtility.ToJson(data);
			
			//encrypt data
			string[] encryptedData = AESEncryptionDecryption.Encrypt(jsonData, password);

			//write to file
			File.WriteAllLines(path, encryptedData);
			
		}
		
		public static void LoadPlayer()
		{

			//check file exist or not
			if(File.Exists(path))
			{
				//copy file content 
				string[] encryptedData = File.ReadAllLines(path);
				
				//decrypt data
				string jsonData = AESEncryptionDecryption.Decrypt(encryptedData, password);
				
				//copy to persistent file
				GameData data = new GameData(); 
				data = JsonUtility.FromJson<GameData>(jsonData);
				updateData(data);
				
			}else{
				//file not found then
				Debug.Log("file not found hence creating a new now");
				
				//save to file
				SavePlayer();
			
			}
		}

		public static void updateData(GameData data)
		{
			if(data == null)
            {
				PersistentData.lastTwentyScores.RemoveRange(0,PersistentData.lastTwentyScores.Count);
				PersistentData.gameMusic = true;
				PersistentData.gameSound = true;
				return;
			}
			PersistentData.lastTwentyScores = data.lastTwentyScores;
			PersistentData.gameMusic = data.gameMusic;
			PersistentData.gameSound = data.gameSound;
		}

		
		public static void DeleteSavedFile()
        {
			if (File.Exists(path))
			{
				File.Delete(path);
				Debug.Log("Saved file destroyed!");

			}
			else Debug.Log("Nothing to delete!");
        }
	}
	
}
