using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class HighScoreMenuController : MonoBehaviour {

	//private Text text1, text2, text3, text4, text5, text6, text7, text8, text9, text10;
	//private List<Text> texts;

	GameObject panel;
	RectTransform panelRect;
	float posY;

	// Use this for initialization
	void Start () {

		/*text1 = GameObject.Find ("Text1").GetComponent<Text>(); text2 = GameObject.Find ("Text2").GetComponent<Text>(); text3 = GameObject.Find ("Text3").GetComponent<Text>();
		text4 = GameObject.Find ("Text4").GetComponent<Text>(); text5 = GameObject.Find ("Text5").GetComponent<Text>(); text6 = GameObject.Find ("Text6").GetComponent<Text>();
		text7 = GameObject.Find ("Text7").GetComponent<Text>(); text8 = GameObject.Find ("Text8").GetComponent<Text>(); text9 = GameObject.Find ("Text9").GetComponent<Text>();
		text10 = GameObject.Find ("Text10").GetComponent<Text>();

		texts  = new List<Text>();
		texts.Add(text1); texts.Add(text2); texts.Add(text3); texts.Add(text4);
		texts.Add(text5); texts.Add(text6); texts.Add(text7); texts.Add(text8);
		texts.Add(text9); texts.Add(text10);*/

		panel = GameObject.Find ("PanelText");
		panelRect = panel.GetComponent<RectTransform>();

		checkHighScore ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Return(){
		Application.LoadLevel ("Main_menu");
	}

	/*void checkHighScore() {
		List<HighScore> highScores = GameModel.HighScores;
		for (int i = 0; i < highScores.Count; i++) {
			Debug.Log (highScores[i].Name);
			int pos = i+1;
			texts[i].text = pos.ToString()+"\tName : "+highScores[i].Name+"\t\tScore : "+highScores[i].Score;

		}
	}*/

	void checkHighScore() {
		List<HighScore> highScores = GameModel.HighScores;

		posY = 150f;

		for (int i = 0; i < highScores.Count; i++) {
			int pos = i+1;
			//GUIText text = new GUIText();
			//RectTransform rect = new RectTransform();
			GameObject tmp = new GameObject("Text_"+pos.ToString());

			tmp.AddComponent<Text>();
			Text text = tmp.GetComponent<Text>();
			RectTransform rect = tmp.GetComponent<RectTransform>();
			tmp.layer = 5;

			tmp.transform.parent = panel.transform;

			rect.sizeDelta = new Vector2(250, 30);
			rect.localPosition = new Vector3(-8.5f, posY, 0.0f);
			//rect.position = new Vector3(-8.5f, 152.4f, 0.0f);

			text.text = pos.ToString()+"\tName : "+highScores[i].Name+"\t\tScore : "+highScores[i].Score;
			text.fontSize = 18;
			text.font = Resources.Load("fonts/augusta") as Font;
			text.color = Color.black;
			text.alignment = TextAnchor.MiddleLeft;

			posY -= 33.0f;

		}
	}
}
