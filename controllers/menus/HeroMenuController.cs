using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HeroMenuController : MonoBehaviour {

	string userName;

	InputField inputName;

	Button buttonPlay;

	Button buttonWarrior;
	Button buttonWizard;
	Button buttonMonk;
	bool warrior, wizard, monk;

	Button buttonSlot1;
	Button buttonSlot2;
	Button buttonSlot3;
	bool slot1, slot2, slot3;

	int save;

	ColorBlock cb;

	// Use this for initialization
	void Start () {
		inputName = GameObject.Find("InputName").GetComponent<InputField>();

		buttonPlay = GameObject.Find("Play").GetComponent<Button>();

		buttonWarrior = GameObject.Find("Warrior").GetComponent<Button>();
		buttonWizard = GameObject.Find("Wizard").GetComponent<Button>();
		//buttonMonk = GameObject.Find("Monk").GetComponent<Button>();

		buttonSlot1 = GameObject.Find("Slot1").GetComponent<Button>();
		buttonSlot2 = GameObject.Find("Slot2").GetComponent<Button>();
		buttonSlot3 = GameObject.Find("Slot3").GetComponent<Button>();

		buttonPlay.interactable = false;

		cb = buttonWarrior.colors;

		userName = "";

		warrior = false;
		wizard = false;
		monk = false;

		/*if(GameModel.Saves.Count == 0) {
			buttonSlot1.interactable
		} else if(GameModel.Saves.Count == 1) {
			
		} else if(GameModel.Saves.Count == 0) {
			
		} else {

		}*/
		slot1 = false;
		slot2 = false;
		slot3 = false;
	}
	
	// Update is called once per frame
	void Update () {
		userName = inputName.text;
		if(((warrior|wizard|monk) == true) & ((slot1|slot2|slot3) == true) & userName != ""){
			buttonPlay.interactable = true;
		} else buttonPlay.interactable = false;

	}

	public void Warrior(){
		cb.normalColor = new Color32(163, 124, 124, 255);
		buttonWarrior.colors = cb;
		warrior = true;
		
		cb.normalColor = Color.white;
		buttonWizard.colors = cb;
		//buttonMonk.colors = cb;
		wizard = false;
		//monk = false;

	}

	public void Wizard() {
		cb.normalColor = new Color32(163, 124, 124, 255);
		buttonWizard.colors = cb;
		wizard = true;

		cb.normalColor = Color.white;
		buttonWarrior.colors = cb;
		//buttonMonk.colors = cb;
		warrior = false;
		//monk = false;
	}

	/*public void Monk() {
		cb.normalColor = new Color32(163, 124, 124, 255);
		buttonMonk.colors = cb;
		monk = true;

		cb.normalColor = Color.white;
		buttonWarrior.colors = cb;
		buttonWizard.colors = cb;
		warrior = false;
		wizard = false;

	}*/

	public void Slot1() {
		cb.normalColor = new Color32(163, 124, 124, 255);
		buttonSlot1.colors = cb;
		slot1 = true;
		
		cb.normalColor = Color.white;
		buttonSlot2.colors = cb;
		buttonSlot3.colors = cb;
		slot2 = false;
		slot3 = false;

		save = 0;
	}

	public void Slot2() {
		cb.normalColor = new Color32(163, 124, 124, 255);
		buttonSlot2.colors = cb;
		slot2 = true;
		
		cb.normalColor = Color.white;
		buttonSlot1.colors = cb;
		buttonSlot3.colors = cb;
		slot1 = false;
		slot3 = false;

		save = 1;
	}

	public void Slot3() {
		cb.normalColor = new Color32(163, 124, 124, 255);
		buttonSlot3.colors = cb;
		slot3 = true;
		
		cb.normalColor = Color.white;
		buttonSlot1.colors = cb;
		buttonSlot2.colors = cb;
		slot1 = false;
		slot2 = false;

		save = 2;
	}

	public void Play(){
		if (warrior) GameModel.Hero = new Warrior();
		if (monk) GameModel.Hero = new Monk();
		if (wizard) GameModel.Hero = new Wizard();

		GameModel.resetSaveSlot(save);

		GameModel.Hero.Name = userName;
		Application.LoadLevel("GameScene");
	}

	public void Return() {
		Application.LoadLevel("Main_menu");
	}
}
