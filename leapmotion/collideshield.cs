using UnityEngine;
using System.Collections;

public class collideshield : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/**
	 * set hero defense mode on when the shield collides
	 **/
	void OnTriggerEnter(Collider other)
	{
		//Debug.Log("colli shield sur: "+other.ToString() + " tag: "+other.gameObject.tag);

		if(other.gameObject.tag == "hero_defense")
		{
			//Debug.Log ("SHIELD");
			Hero hero = other.gameObject.GetComponentInParent<Hero>();
			hero.DefenseMode("on");
		}
	}
}
