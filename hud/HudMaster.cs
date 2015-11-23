using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.UI;

public class HudMaster : MonoBehaviour {

    public enum HudType { Life, Special };

    public GameObject hudLife;
    public GameObject hudSpecial;
	public GameObject hudXPBar;
	public GameObject hudXPText;
	public GameObject hudShield;

	private bool shieldActivated;

	private float timerShield = 0.0f;
	private float maxTimerShield = 0.5f;

	private float groundOnCam;

    // Use this for initialization
    void Start () {
       // hudLife = GameObject.Find("hud_life");
       // hudSpecial = GameObject.Find("hud_special");
		Color32 newColor;
		if (GameModel.HerosInGame [0].GetType ().ToString () == "Warrior") {
			newColor = new Color32(43, 170, 11, 184);
		} else if (GameModel.HerosInGame [0].GetType ().ToString () == "Wizard") {
			newColor = new Color32 (25, 1, 202, 184);
		} else {
			newColor = new Color32 (20, 20, 20, 184);
		}

		hudSpecial.GetComponent<Image> ().color = (Color) newColor;

		shieldActivated = false;


		groundOnCam = Camera.main.WorldToViewportPoint(new Vector3 (0.0f, 0.0f, GameModel.HerosInGame[0].GetPosition().z + 3.0f)).y*this.gameObject.GetComponent<RectTransform>().sizeDelta.y;

    }
	
	// Update is called once per frame
	void Update () {
	    if (shieldActivated) {

			if (timerShield >= maxTimerShield) {
				hudShield.SetActive(!hudShield.activeSelf);
				timerShield = 0.0f;
			}



			timerShield += Time.deltaTime;
		}

		if (Input.GetKeyDown(KeyCode.S)) {
			ShieldActivated = !ShieldActivated;
		}
	}

	/**
	 * @param HudType _hudType Is it power HUD or Life HUD
	 * @param float _levelPercent Number between 0 and 100 that will graphically sets the filling percentage of the gauje
	 **/
    public void setLevel(HudType _hudType, float _levelPercent)
    {
        GameObject hudTarget = null;

        if (_hudType == HudType.Life)
        {
            hudTarget = hudLife;
        }
        else if (_hudType == HudType.Special)
        {
            hudTarget = hudSpecial;
        }

        if (hudTarget != null)
        {
            hudTarget.transform.localScale = new Vector3(1, _levelPercent/100, 1);
        }
    }

	public void updateXP(float xpPercent, int level) {

		hudXPBar.transform.localScale = new Vector3 (xpPercent / 100.0f, 1.0f, 1.0f);

		//GameObject levelDigit = hudXP.transform.FindChild ("levelText").gameObject;
		hudXPText.GetComponentInChildren<Text>().text = level.ToString();


	}

	public bool ShieldActivated {
		get {
			return this.shieldActivated;
		}
		set {
			Vector3 vec = new Vector3(Random.Range(-2.0F, 2.0F), 1.50f, GameModel.HerosInGame[0].GetPosition().z + 3.0f);
			Debug.Log("random : " + vec );
			WorldToShieldPosition( vec );
			this.shieldActivated = value;
			hudShield.SetActive(value);
			timerShield = 0.0f;
		
		}
	}
	

	public void WorldToShieldPosition (Vector3 worldPosition) {
		//hudShield.transform. position = Camera.main.WorldToScreenPoint (worldPosition);
		//Debug.Log ("screen : " + hudShield.transform.position);
		//Vector3 pos = hudShield.transform.position;
		//pos.y = 0.0f;


//		//this is the ui element
//		RectTransform UI_Element = hudShield.GetComponent<RectTransform>();
//		
//		//first you need the RectTransform component of your canvas
//		RectTransform CanvasRect=this.gameObject.GetComponent<RectTransform>();
//		
//		//then you calculate the position of the UI element
//		//0,0 for the canvas is at the center of the screen, whereas WorldToViewPortPoint treats the lower left corner as 0,0. Because of this, you need to subtract the height / width of the canvas * 0.5 to get the correct position.
//		
//		Vector2 ViewportPosition=Camera.main.WorldToViewportPoint(worldPosition);
//		Vector2 WorldObject_ScreenPosition=new Vector2(
//			(ViewportPosition.x*CanvasRect.sizeDelta.x),
//			(ViewportPosition.y*CanvasRect.sizeDelta.y + groundOnCam));
//		 ////-(CanvasRect.sizeDelta.x*0.5f))
//		 /// //-(CanvasRect.sizeDelta.y*0.5f)));
//		//now you can set the position of the ui element
//		Debug.Log (WorldObject_ScreenPosition);
//		UI_Element.anchoredPosition=WorldObject_ScreenPosition;


		Debug.Log (RectTransformUtility.WorldToScreenPoint (Camera.main, worldPosition));
		Vector2 pos = hudShield.GetComponent<RectTransform> ().anchoredPosition;
		pos = RectTransformUtility.WorldToScreenPoint (Camera.main, worldPosition);
		//pos.y -= -2f;
		hudShield.GetComponent<RectTransform> ().anchoredPosition = pos;
	}


}
