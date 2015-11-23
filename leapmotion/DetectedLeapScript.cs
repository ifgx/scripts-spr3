using UnityEngine;
using System.Collections;

public class DetectedLeapScript : MonoBehaviour {

	public Game.GameController gc;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void IgnoreLeapNotConnected()
	{
		this.gameObject.GetComponent<Canvas>().enabled = false;
		gc.Resume();

	}
}
