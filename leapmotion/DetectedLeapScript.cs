using UnityEngine;
using System.Collections;

/**
 * @author Baptiste Valthier
 * @see similar class : DetectedLeapSandbox which has a SandboxController slot instead of a GameController slot.
 */
public class DetectedLeapScript : MonoBehaviour {

	public Game.GameController gc;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/**
	 * @author Baptiste Valthier
	 * hides the leap canvas (referenced by the slot gc (gamecontroller)) 
	 */
	public void IgnoreLeapNotConnected()
	{
		this.gameObject.GetComponent<Canvas>().enabled = false;
		gc.Resume();

	}
}
