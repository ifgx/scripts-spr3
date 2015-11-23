using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class LevelGenerator {

	private static string[] items =
	{
		"basicLancer",
		"fireLancer",
		"iceLancer",
		"basicDragonet",
		"fireDragonet",
		"iceDragonet",
		"assassin",
		"wall",
		"cannon",
		"life",
		"power",
		"invincibility"
	};

	private static float itemSpaceMin = 5.0f;
	private static float itemSpaceMax = 10.0f;
	private static float itemSpaceBase = 7.0f;

	private static float depthCount;

	public static void generateLevelFromFile(string filePath){


		string[] splitedPath = filePath.Split ('\\');
		string fileName = splitedPath [splitedPath.Length - 1];
		string[] splitedFileName = fileName.Split ('.');
		string musicName = splitedFileName[0];
		Debug.Log (musicName);

		//USING WWW to load the audioclip in root/Musics
		string path = "file://" + Application.dataPath + "/../Musics/" + musicName + ".wav";


		string LevelPath = Application.dataPath + "/../Levels/" + musicName + ".JSON";
		if (!File.Exists (LevelPath)) {

		

			//TODO copier le fichier dans musics
			File.Copy (filePath, Application.dataPath + "/../Musics/" + musicName + ".wav");

			//Debug.Log ("music : " + path);
			WWW www = new WWW (path);
			while (!www.isDone) {
				//Debug.Log ("loading music ...");
			}

			AudioClip clip = www.GetAudioClip (false);
		
		


			float levelLength = 180.0f; //clip.length;
			depthCount = itemSpaceBase;

			List<Item> itemList = new List<Item> ();
			while (depthCount < levelLength) {


				itemList.Add (new Item (randomItemType (), (int)depthCount, Random.Range (-2.0f, 2.0f)));
			
				depthCount += Random.Range (itemSpaceMin, itemSpaceMax);
			}
			//clip.name = "zbra";
			Level level = new Level (musicName, musicName, "undefined", itemList, false);
	
			LevelParser.saveLevelToFile (level);

			GameModel.ActualLevel = level;

		} else {
		
			GameModel.ActualLevel = LevelParser.parseLevelFile(musicName);

		}


		GameModel.CustomLevel = true;
	}

	private static string randomItemType() {
		//Item item = new Item ("type", depthCount, Random.Range (-2.0f, 2.0f));
		return items [Random.Range (0, 11)];
	}


}
