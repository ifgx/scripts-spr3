using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CustomMenuController : MonoBehaviour {
	
	string userName;
	string customSong;
	string[] tmp;
	
	InputField inputName;
	
	Button buttonPlay;
	Button buttonSong;
	Button buttonWarrior;
	Button buttonWizard;
	//Button buttonMonk;

	Text songText;

	GameObject browserMenu, canvas;
	LaunchFileBrowser launchBrowser;
	
	bool warrior, wizard, monk;
	bool browser;
	
	ColorBlock cb;
	
	// Use this for initialization
	void Start () {
		inputName = GameObject.Find("InputName").GetComponent<InputField>();

		buttonSong = GameObject.Find("Song").GetComponentInChildren<Button>();
		buttonPlay = GameObject.Find("Play").GetComponent<Button>();
		buttonWarrior = GameObject.Find("Warrior").GetComponent<Button>();
		buttonWizard = GameObject.Find("Wizard").GetComponent<Button>();
		//buttonMonk = GameObject.Find("Monk").GetComponent<Button>();

		songText = GameObject.Find ("PathSong").GetComponent<Text>();

		canvas = GameObject.Find("Canvas");

		browserMenu = GameObject.Find("Browser");
		launchBrowser = (LaunchFileBrowser) GameObject.Find ("Browser").GetComponent(typeof(LaunchFileBrowser));
		browserMenu.SetActive(false);

		buttonPlay.interactable = false;
		//buttonMonk.interactable = false;
		
		cb = buttonWarrior.colors;
		
		userName = "";
		customSong = "";
		
		warrior = false;
		wizard = false;
		monk = false;

		browser = false;
	}
	
	// Update is called once per frame
	void Update () {
		userName = inputName.text;

		if(((warrior|wizard|monk) == true) & userName != "" & songText.text != "Empty"){
			buttonPlay.interactable = true;
		} else buttonPlay.interactable = false;

		if(launchBrowser.Select){
			customSong = launchBrowser.Output;
			tmp = customSong.Split('\\');
			songText.text = tmp[tmp.Length-1];
			browserMenu.SetActive(false);
			canvas.SetActive(true);
			launchBrowser.Select = false;
		}

		if(launchBrowser.Cancel){
			//songText.text = "Empty";
			browserMenu.SetActive(false);
			canvas.SetActive(true);
			launchBrowser.Cancel = false;
		}
		
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
	
	public void Play(){
		if (warrior) GameModel.Hero = new Warrior();
		if (monk) GameModel.Hero = new Monk();
		if (wizard) GameModel.Hero = new Wizard();
		GameModel.Hero.Name = userName;
		Application.LoadLevel("GameScene");
	}

	public void Browser(){
		browserMenu.SetActive(true);
		canvas.SetActive(false);
	}
	
	public void Return() {
		Application.LoadLevel("Main_menu");
	}
}
