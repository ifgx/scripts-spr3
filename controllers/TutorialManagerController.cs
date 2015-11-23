using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TutorialManagerController : MonoBehaviour {

	public enum TutorialState {
		FREEZE,
		NORMAL,
	};

	public static List<Tutorial> tutorials;

	private float freezeTimeScale = 0.1f;

	private float maxTimerFreeze = 3.0f;
	private float maxTimerNormal = 3.0f;

	private float timerFreeze = 0.0f;
	private float timerNormal = 0.0f;



	private GameObject tutoUIPrefab;
	private GameObject tutoUIInstance;

	private bool lancerDone = false;
	private bool dragonetDone = false;

	private TutorialState state = TutorialState.NORMAL;

	// Use this for initialization
	void Start () {
		maxTimerFreeze *= freezeTimeScale;

		tutoUIPrefab = Resources.Load("prefabs/hud/BasicTutorial") as GameObject;


	}
	
	// Update is called once per frame
	void Update () {
		if (state == TutorialState.NORMAL)
			normal ();
		else if (state == TutorialState.FREEZE)
			freeze ();
	}

	private void freeze(){
		timerFreeze += Time.deltaTime;
		//Debug.Log (timerFreeze);
		if (timerFreeze >= maxTimerFreeze) {
			Destroy(tutoUIInstance);

			state = TutorialState.NORMAL;
			Time.timeScale = 1.0f;
			timerFreeze = 0.0f;
		}
	}

	private void normal(){
		timerNormal += Time.deltaTime;


		foreach (Tutorial tuto in tutorials) {
			if (!tuto.Played && tuto.requestTrigger()) {
				launchFreeze(tuto.Text,tuto.ImagePath);
				tuto.Played = true;
			}
		}

		/*
		NPC npc = GameModel.getNearestNPC ();
		if (npc != null && npc.GetPosition().z-GameModel.HerosInGame[0].GetPosition().z < distTrigger) {
			string npcType = npc.GetType().ToString();
			if (npcType.Contains("Lancer") && !lancerDone){

				lancerDone = true;
			}else if (npcType.Contains("Dragonet") && !dragonetDone) {
				launchFreeze("Si les meilleurs partent en premier, pourquoi suis-je toujours en vie ?","xhamster");
				dragonetDone = true;
			}


		}
		*/
		
	}

	private void launchFreeze(string text, string imagePath) {
		tutoUIInstance = Instantiate(tutoUIPrefab) as GameObject;
		TutorialUIManager uiMan = tutoUIInstance.GetComponent<TutorialUIManager> ();
		uiMan.setText (text);
		uiMan.setImage (imagePath);
		
		state = TutorialState.FREEZE;
		Time.timeScale = freezeTimeScale;
		timerNormal = 0.0f;
		

	}
	
	void OnDestroy(){
		Time.timeScale = 1.0f;
	}
}
