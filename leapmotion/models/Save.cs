using UnityEngine;
using System.Collections;

public class Save {

	private Hero hero;

	private int levelId;

	private int score;


	public Save(Hero hero, int levelId, int score){
		this.hero = hero;
		this.levelId = levelId;
		this.score = score;
	}

	public Save(){
	}

	public Hero Hero {
		get {
			return hero;
		}
		
		set {
			hero = value;
		}
	}

	public int LevelId {
		get {
			return levelId;
		}
		
		set {
			levelId = value;
		}
	}

	public int Score {
		get {
			return score;
		}
		
		set {
			score = value;
		}
	}

}
