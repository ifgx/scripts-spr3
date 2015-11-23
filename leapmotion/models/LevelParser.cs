using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO; //pour StreamReader
using SimpleJSON;

/**
 * @author Florentin M & Adrien D
 * @version 1.0
 */

/**
 * Class used to parse a level file in JSON format using SimpleJSON library
 * The aim is to return a Level class
 */
public class LevelParser {

	public static void saveLevelToFile(Level level) {
		JSONNode json = LevelToJSON (level);

		System.IO.File.WriteAllText (Application.dataPath + "/../Levels/" + level.Name + ".JSON", LevelToJSON(level).ToString());
		Debug.Log (level.Name);
		//json.SaveToFile(Application.dataPath + "/../Levels/" + level.Name + ".JSON");
	}

	private static JSONNode LevelToJSON(Level level) {

		JSONClass root = new JSONClass ();

		JSONData name = new JSONData (level.Name);
		root.Add ("name",name);

		JSONData music = new JSONData (level.MusicPath);
		root.Add ("music",name);

		JSONData map = new JSONData (level.Map);
		root.Add ("map",name);

		JSONData tutorial = new JSONData (level.Tutorial);
		root.Add ("tutorial", tutorial);


		int i = 0;
		JSONArray items = new JSONArray ();
		foreach (Item itemInList in level.ItemList) {
			JSONClass item = new JSONClass();

			i++;
			JSONData id = new JSONData (i);
			item.Add ("id", id);

			JSONData type = new JSONData (itemInList.Type);
			item.Add ("type", type);

			JSONData position_seconds = new JSONData (itemInList.PositionInSeconds);
			item.Add ("position_seconds", position_seconds);

			JSONData position_x = new JSONData (itemInList.PositionInX);
			item.Add ("position_x", position_x);

			items.Add (item);
		}

		root.Add ("items", items);
		return root;
	}
	

	public static Level parseLevelFile(string levelFileName){

		JSONNode jsonContent = getJsonFile ("Levels/"+levelFileName+".JSON");

		string name = getName (jsonContent);
		string musicPath = getMusicPath (jsonContent);
		string map = getMap (jsonContent);
		List<Item> items = getItems (jsonContent);
		bool tutorial = getTutorial (jsonContent);

		return new Level (name, musicPath, map, items, tutorial);
	}

	public static List<Level> parseAllLevelFiles(string mainLevelFileName){

		List<Level> levels = new List<Level> ();

		JSONNode root = getJsonFile ("Levels/"+mainLevelFileName+".JSON");

		JSONNode fileList = root.AsArray;

		int size = fileList.Count;
		for (int i=0; i<size; i++) {
			string file = fileList[i];
			//Debug.Log("parsing " + file);
			levels.Add (parseLevelFile(file));
		}

		return levels;
	}
	

	/**
	 * Parse the JSON file using SimpleJSON
	 * @param path the path to the level JSON file
	 * @return the JSONNode, result of the parsing process
	 */
	private static JSONNode getJsonFile(string path){
		StreamReader r = new StreamReader (path); // access the json file
		string json = r.ReadToEnd (); // convert its content to a string 
		return JSON.Parse(json); // return the content as a JSONNode
	}

	/**
	 * Get a list of items contained on the JSONNode
	 * @param json the JSONNode
	 * @return a list of Item describing partly the level
	 */
	private static List<Item> getItems(JSONNode json){
		List<Item> list = new List<Item>();
		int size = json["items"].AsArray.Count;
		for (int i=0; i<size; i++) {
			//Debug.Log (json["items"][i]); // display the ennemy 
			//prepare proprieties of the ennemy
			string t = json["items"][i]["type"];
			int p = json["items"][i]["position_seconds"].AsInt;
			float px = 0.0f;
			if (json["items"][i]["position_x"] != null){
				px = json["items"][i]["position_x"].AsFloat;
			}
			Item m = new Item(t, p, px); // create it
			list.Add(m); // add it to the list
		}

		return list;
	}

	private static string getName(JSONNode json){
		return json ["name"];
	}

	private static string getMusicPath(JSONNode json){
		return json ["music"];
	}
	
	private static string getMap(JSONNode json){
		return json ["map"];
	}

	private static bool getTutorial(JSONNode json){
		return json ["tutorial"].AsBool;
	}
}


