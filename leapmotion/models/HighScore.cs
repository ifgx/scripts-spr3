using UnityEngine;
using System.Collections;

public class HighScore {

	private string name;
	
	private int score;
	
	public HighScore(string name, int score){
		this.name = name;
		this.score = score;
	}
	
	public HighScore(){
	}
	
	public string Name {
		get {
			return name;
		}
		
		set {
			name = value;
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
