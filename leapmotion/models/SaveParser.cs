using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using System.IO; //pour StreamReader

public class SaveParser {

	static List<Save> saves;
	static JSONArray slots;

	public static List<Save> parseLevelFile(){

		saves = new List<Save> ();

		JSONNode root = getJsonFile ("Saves/saves.JSON");
		
		slots = root ["slots"].AsArray;

		int size = slots.Count;

		for (int i=0; i<size; i++) {

			string name = slots[i]["name"];
			int score = slots[i]["score"].AsInt;
			string currentLevel = slots[i]["currentLevel"];
			int xp = slots[i]["xp"].AsInt;
			string className = slots[i]["class"];

			// HERO
			Hero hero;

			if (className == "Warrior") hero = new Warrior();
			else if (className == "Monk") hero = new Monk();
			else if (className == "Wizard") hero = new Wizard();
			else hero = new Warrior();

			hero.Name = name;
			hero.XpQuantity = xp;

			// LEVEL
			int levelId = GameModel.getLevelIdByName(currentLevel);

			saves.Add(new Save(hero, levelId, score));
		}
		return saves;
	}

	public static void addSave(int slot, Hero hero, int score, int levelId) {
		
		saves = parseLevelFile();

		Save save = new Save(hero, levelId, score);

		saves[slot].Hero = hero;
		saves[slot].LevelId = levelId;
		saves[slot].Score = score;
		
		saveSaveToFile();
	}

	/**
	 * Parse the JSON file using SimpleJSON
	 * @param path the path to the level JSON file
	 * @return the JSONNode, result of the parsing process
	 */
	private static JSONNode getJsonFile(string path){
		StreamReader r = new StreamReader (path); // access the json file
		string json = r.ReadToEnd (); // convert its content to a string 

		r.Close();

		return JSON.Parse(json); // return the content as a JSONNode
	}

	private static void saveSaveToFile(){
		
		JSONNode json = SaveToJSON ();
		
		System.IO.File.WriteAllText (Application.dataPath + "/../Saves/saves.JSON", json.ToString());
		
	}


	private static JSONNode SaveToJSON() {		
		
		JSONClass root = new JSONClass ();
		
		JSONArray slotsJson = new JSONArray ();
		int i =0;
		foreach (Save slotInList in saves) {
			JSONClass slot = new JSONClass();

			if(saves[i].Hero.Name != null) {
				JSONData name = new JSONData (slotInList.Hero.Name);
				slot.Add ("name", name);
				JSONData score = new JSONData (slotInList.Score);
				slot.Add ("score", score);
				JSONData heroClass = new JSONData (slotInList.Hero.GetType().ToString());
				slot.Add ("class", heroClass);
				JSONData heroXp = new JSONData (slotInList.Hero.XpQuantity);
				slot.Add ("xp", heroXp);
				JSONData currentLevel = new JSONData (GameModel.Levels[slotInList.LevelId].Name);
				slot.Add ("currentLevel", currentLevel);
			} else {
				slot.Add ("name", "");
				slot.Add ("score", "");
				slot.Add ("class", "");
				slot.Add ("xp", "");
				slot.Add ("currentLevel", "");
			}
			
			slotsJson.Add (slot);

			Debug.Log(i);
			i++;
		}
		
		root.Add ("slots", slotsJson);
		return root;
	}


}
