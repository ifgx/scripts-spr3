using UnityEngine;
using System.Collections;

/**
 * @author Valthier Baptiste
 **/
public class FireballCollision : MonoBehaviour {
	
	private bool called = false;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		//if fireball has been dropped, make it last only one second then destroy
		if (!this.GetComponent<Rigidbody> ().isKinematic && called == false) {
			Destroy (gameObject, 1);
			called = true;
		}
	}

	void onTriggerEnter(Collider col)
	{
		Debug.LogError("** FIRE Fireball Trigger with : "+col);
		//Destroy(this);
		Destroy (gameObject, 1);
	}
}
