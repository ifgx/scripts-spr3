using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WarningLimit : MonoBehaviour {

	private RawImage symbol;

	Renderer planeRenderer = null;
	int showingFrame = 0;

	// Use this for initialization
	void Start () {
		planeRenderer = GetComponent<Renderer>();

		GameObject symbolObject = null;
		switch (gameObject.tag)
		{
		case "RightConstraint" :
				symbolObject = GameObject.Find("HandOutRight");
			break;
		case "LeftConstraint": 
			symbolObject = GameObject.Find("HandOutLeft");
			break;
		case "TopConstraint": 
			symbolObject = GameObject.Find("HandOutTop");
			break;
		}
		

		if (symbolObject == null)
			Debug.LogError("Baptiste says: Can't find object HandOut[Right/Left/Top] make sure is it in the scene or loaded");
		else
			symbol = symbolObject.GetComponentInChildren<RawImage>();
	}
	
	// Update is called once per frame
	void Update () {

		//decrease time left
		if (showingFrame > 0)
			showingFrame --;
		else if (showingFrame == 0)
		{
			//time's up diable warning
			planeRenderer.enabled = false;
			if (symbol != null) symbol.enabled = false;
			showingFrame = -1; //disable loop
		}
	}

	public void showLimit()
	{
		planeRenderer.enabled = true;
		if (symbol != null) symbol.enabled = true;
		showingFrame = 10; //show while 10 frame
	}
}
