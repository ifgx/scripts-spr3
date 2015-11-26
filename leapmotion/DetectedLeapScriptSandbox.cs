using UnityEngine;
using System.Collections;

/**
 * @author Baptiste Valthier
 * @see similar class : DetectedLeap which has a GameController slot instead of a SandboxController slot.
 */
public class DetectedLeapScriptSandbox : MonoBehaviour {

	public SandboxController sc;

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
		sc.Resume();

	}
}
