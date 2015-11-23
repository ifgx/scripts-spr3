using UnityEngine;
using System.Collections;

public abstract class Potion : MonoBehaviour {
	

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			Hero hero = other.gameObject.GetComponentInParent<Hero> ();
			triggerEffect (hero);
			Destroy(this.gameObject);
		}
	}

	protected abstract void triggerEffect(Hero hero);
}
