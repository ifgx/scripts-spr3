using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TutorialUIManager : MonoBehaviour {

	private Text text;
	private Image image;

//	private float fadeTimer = 0.0f;
//	private float fadeTimerMax = 0.3f;

	// Use this for initialization
	void Awake () {
		text = transform.GetComponentInChildren<Text>();
		Debug.Log ("text : " + text);
		image = transform.GetComponentsInChildren<Image>()[1];
	}



//	void Update(){
//
//		if (fadeTimer < fadeTimerMax) {
//			Image panel = GetComponentInChildren<Image>();
//			Color c = panel.color;
//			Debug.Log ((fadeTimer / fadeTimerMax));
//			c.a = (fadeTimer / fadeTimerMax);
//		}
//
//		fadeTimer += Time.deltaTime;
//	}

	public void setText(string newText){
		text.text = newText;
	}

	public void setImage(string imageName){
		Sprite sprite = Resources.Load<Sprite>("helpImages/"+imageName) as Sprite;
		Debug.Log ("helpImages/" + imageName);
		//Debug.Log (sprite.bounds);
		image.sprite = sprite;
	}
}
