using UnityEngine;
using System.Collections;

public class Tutorial {

	private float distTrigger = 20.0f;

	private string text;
	private string imagePath;
	private bool played;
	private string name;

	public Tutorial(string text, string imagePath, string name) {
		this.text = text;
		this.imagePath = imagePath;
		this.name = name;
	}

	public string Text {
		get {
			return this.text;
		}

		set {
			this.text = value;
		}
	}

	public string ImagePath {
		get {
			return this.imagePath;
		}
		
		set {
			this.imagePath = value;
		}
	}

	public bool Played {
		get {
			return this.played;
		}

		set {
			this.played = value;
		}
	}



	public bool requestTrigger(){
		if (name == "onPop") {

			return (GameModel.HerosInGame [0].GetPosition ().z >= 5.0f);

		} else if (name == "firstAttack") {

			return (GameModel.HerosInGame [0].GetPosition ().z >= 10.0f);

		} else if (name == "firstDefence") {

			return (GameModel.HerosInGame [0].GetPosition ().z >= 15.0f);

		} else if (name == "Lancer") {

			NPC npc = GameModel.getNearestNPC ();
			return (npc != null && npc.GetType().ToString().Contains("Lancer") && npc.GetPosition().z-GameModel.HerosInGame[0].GetPosition().z < distTrigger);
		
		} else if (name == "Dragonet") {

			NPC npc = GameModel.getNearestNPC ();
			return (npc != null && npc.GetType().ToString().Contains("Dragonet") && npc.GetPosition().z-GameModel.HerosInGame[0].GetPosition().z < distTrigger);


		} else if (name == "Assassin") {

			NPC npc = GameModel.getNearestNPC ();
			return (npc != null && npc.GetType().ToString().Contains("Assassin") && npc.GetPosition().z-GameModel.HerosInGame[0].GetPosition().z < distTrigger);

		
		} else if (name == "Cannon") {

			NPC npc = GameModel.getNearestNPC ();
			return (npc != null && npc.GetType().ToString().Contains("Cannon") && npc.GetPosition().z-GameModel.HerosInGame[0].GetPosition().z < distTrigger);

		
		} else if (name == "Wall") {

			NPC npc = GameModel.getNearestNPC ();
			return (npc != null && npc.GetType().ToString().Contains("Wall") && npc.GetPosition().z-GameModel.HerosInGame[0].GetPosition().z < distTrigger);


		} else if (name == "Potion") {

			return false;
		
		} else if (name == "Fire") {

			NPC npc = GameModel.getNearestNPC ();
			return (npc != null && npc.GetType().ToString().Contains("Fire") && npc.GetPosition().z-GameModel.HerosInGame[0].GetPosition().z < distTrigger);

		
		} else if (name == "Ice") {

			NPC npc = GameModel.getNearestNPC ();
			return (npc != null && npc.GetType().ToString().Contains("Ice") && npc.GetPosition().z-GameModel.HerosInGame[0].GetPosition().z < distTrigger);

		} else {
			return false;
		}
	}
}
