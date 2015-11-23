using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using System.IO; //pour StreamReader

public class HighScoreParser {

	static JSONArray slots;
	static List<HighScore> highScores;

	public static List<HighScore> parseHighScoreFile() {

		highScores = new List<HighScore> ();
		
		JSONNode root = getJsonFile ();
		
		slots = root ["slots"].AsArray;
		
		int size = slots.Count;

		for (int i=0; i<size; i++) {
			
			string name = slots[i]["name"];
			int score = slots[i]["score"].AsInt;
			
			highScores.Add(new HighScore(name, score));
		}
		
		return highScores;
	}

	public static void addHighScore(string name, int score) {

		highScores = parseHighScoreFile();

		int size = highScores.Count;

		string lastName = "";
		int lastScore = 0;
		int i = size-1;

		while(i != 0) {

			lastName = highScores[i].Name;
			lastScore = highScores[i].Score;

			if(score > lastScore){
				highScores[i].Name = name;
				highScores[i].Score = score;

				if(size - 1 != i){
					highScores[i+1].Name = lastName;
					highScores[i+1].Score = lastScore;
				}
			}

			i--;
		}

		saveHighScoreToFile();
	}

	/**
	 * Parse the JSON file using SimpleJSON
	 * @param path the path to the level JSON file
	 * @return the JSONNode, result of the parsing process
	 */
	private static JSONNode getJsonFile(){
		StreamReader r = new StreamReader ("Saves/highScore.json"); // access the json file
		string json = r.ReadToEnd (); // convert its content to a string

		r.Close();

		return JSON.Parse(json); // return the content as a JSONNode
	}

	private static void saveHighScoreToFile(){

		JSONNode json = HighScoreToJSON ();

		//Debug.Log ("Ecriture");
		
		System.IO.File.WriteAllText (Application.dataPath + "/../Saves/highScore.json", json.ToString());
		//Debug.Log ("Nouveau Fichier");

	}

   private static JSONNode HighScoreToJSON() {		

		JSONClass root = new JSONClass ();

		JSONArray slotsJson = new JSONArray ();
		foreach (HighScore slotInList in highScores) {
			JSONClass slot = new JSONClass();

			JSONData name = new JSONData (slotInList.Name);
			slot.Add ("name", name);
			
			JSONData score = new JSONData (slotInList.Score);
			slot.Add ("score", score);
			
			slotsJson.Add (slot);
		}
		
		root.Add ("slots", slotsJson);
		return root;
	}
}
