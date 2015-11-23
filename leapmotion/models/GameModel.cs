using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * @author Adrien D
 * @version 1.0
 */

/**
 * This class is made to keep the context between controllers
 */
public class GameModel {

	/**
	 * The hero : mainly to keep the class and XP
	 */
	private static Hero hero;

	private static List<Hero> herosInGame;

	private static List<NPC> npcsInGame;

	private static Level actualLevel;

	private static List<Level> levels;

	private static int actualLevelId;

	private static List<Save> saves;

	private static bool customLevel;

	private static List<HighScore> highScores;

	private static int score;

	private static int slot;

	public static int Slot {
		get {
			return slot;
		}
		
		set {
			slot = value;
		}
	}

	public static int Score {
		get {
			return score;
		}
		
		set {
			score = value;
		}
	}

	public static Hero Hero {
		get {
			return hero;
		}

		set {
			hero = value;
		}
	}

	public static List<Hero> HerosInGame {
		get {
			return herosInGame;
		}
		
		set {
			herosInGame = value;
		}
	}

	public static List<NPC> NPCsInGame {
		get {
			return npcsInGame;
		}
		
		set {
			npcsInGame = value;
		}
	}

	public static Level ActualLevel {
		get {
			return actualLevel;
		}
		
		set {
			actualLevel = value;
		}
	}

	public static int ActualLevelId {
		get {
			return actualLevelId;
		}
		
		set {
			if (value >= 0 && value < levels.Count){
				actualLevelId = value;
				actualLevel = levels[actualLevelId];
			}
		}
	}


	public static List<Level> Levels {
		get {
			return levels;
		}
		
		set {
			levels = value;
		}
	}

	public static int getLevelIdByName(string name){
		int res = -1;

		int size = levels.Count;
		for (int i=0; i<size; i++) {
			if (levels[i].Name == name){
				res = i;
				break;
			}
		}
		return res;
	}
	

	public static List<Save> Saves {
		get {
			return saves;
		}
		
		set {
			saves = value;
		}
	}

	public static List<HighScore> HighScores {
		get {
			return highScores;
		}
		
		set {
			highScores = value;
		}
	}
	
	public static NPC getNearestNPC(){
		NPC res = null;

		foreach (NPC npc in npcsInGame) {
			if (res == null || npc.GetPosition().z < res.GetPosition().z) res = npc;	
		}

		return res;
	}

	/**
	 * Initialisation of the game model
	 */
	public static void Init(){
		hero  = new Wizard();
		levels = LevelParser.parseAllLevelFiles ("LvlList");

		//Debug.Log (levels.Count + " levels parsed");

		ActualLevelId = 0;
		//Debug.Log (actualLevel.Name + " is the actual level");

		herosInGame = new List<Hero> ();

		//create saves
		saves = SaveParser.parseLevelFile ();

		highScores = HighScoreParser.parseHighScoreFile();

		score = 0;

		TutorialManagerController.tutorials = new List<Tutorial> ();
		TutorialManagerController.tutorials.Add(new Tutorial("Texte du tuto du POP", "weaponWarrior", "onPop"));
		TutorialManagerController.tutorials.Add(new Tutorial("Texte du tuto de l'ATTAQUE", "weaponWarrior", "firstAttack"));
		TutorialManagerController.tutorials.Add(new Tutorial("Texte du tuto de la DEFENSE", "ShieldWarrior", "firstDefence"));
		TutorialManagerController.tutorials.Add(new Tutorial("Texte du tuto pour un LANCIER", "BasicLancer", "Lancer"));
		TutorialManagerController.tutorials.Add(new Tutorial("Texte du tuto pour un DRAGONNET", "BasicDragonet", "Dragonet"));
		TutorialManagerController.tutorials.Add(new Tutorial("Texte du tuto pour un ASSASSIN", "Assassin", "Assassin"));
		TutorialManagerController.tutorials.Add(new Tutorial("Texte du tuto pour un CANON", "Cannon", "Cannon"));
		TutorialManagerController.tutorials.Add(new Tutorial("Texte du tuto pour un MUR", "Wall", "Wall"));
		TutorialManagerController.tutorials.Add(new Tutorial("Texte du tuto pour un MUR", "FireDragonet", "Fire"));
		TutorialManagerController.tutorials.Add(new Tutorial("Texte du tuto pour un MUR", "IceLancer", "Ice"));
	}

	public static void resetDataBeforeLevel(){
		npcsInGame = new List<NPC> ();
	}

	public static void loadSave(int saveNum){

		Save save = saves [saveNum];
		hero = save.Hero;
		score = save.Score;
		ActualLevelId = save.LevelId;
	}

	public static void initSandbox() {
		herosInGame = new List<Hero> ();
		npcsInGame = new List<NPC> ();

		//herosInGame.Add( new 
	}
	
	public static void resetSaveSlot(int slot){
		Slot = slot;
		if (saves.Count < 3) {
			saves.Add (new Save (Hero, ActualLevelId, Score));
		} else {
			Save save = saves [slot];
			save.Hero = Hero;
			save.LevelId = ActualLevelId;
			save.Score = Score;
		}
	}

	public static bool CustomLevel {
		get {
			return customLevel;
		}
		set {
			customLevel = value;
		}
	}

	
}
