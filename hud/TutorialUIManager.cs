using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/**
 * @author Adrien D & Baptiste V
 * @version 1.0
 */

 /**
  * Manipulator of a tutorial UI
  */ 
public class TutorialUIManager : MonoBehaviour {

	private Text text;
	private Image image;


	// Use this for initialization
	/**
	 * Initialization
	 */
	void Awake () {
		text = transform.GetComponentInChildren<Text>();
		Debug.Log ("text : " + text);
		image = transform.GetComponentsInChildren<Image>()[1];
	}


	/**
	 * Set the text of the tutorial
	 */
	public void setText(string newText){
		text.text = newText;
	}

	/**
	 * Set the image of the tutorial
	 */
	public void setImage(string imageName){
		Sprite sprite = Resources.Load<Sprite>("helpImages/"+imageName) as Sprite;
		Debug.Log ("helpImages/" + imageName);
		//Debug.Log (sprite.bounds);
		image.sprite = sprite;
	}
}
