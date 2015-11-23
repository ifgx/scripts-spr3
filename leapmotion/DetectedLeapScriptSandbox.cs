using UnityEngine;
using System.Collections;

public class DetectedLeapScriptSandbox : MonoBehaviour {

	public SandboxController sc;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void IgnoreLeapNotConnected()
	{
	
		this.gameObject.GetComponent<Canvas>().enabled = false;
		sc.Resume();

	}
}
