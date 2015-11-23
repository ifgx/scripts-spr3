using UnityEngine;
using System.Collections;

/**
 * @author Adrien D
 * @version 1.0
 */

/**
 * Potion script
 */

public abstract class Potion : MonoBehaviour {
	
	/**
	 * Triggers the effect on collision with the Hero
	 */
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			Hero hero = other.gameObject.GetComponentInParent<Hero> ();
			triggerEffect (hero);
			Destroy(this.gameObject);
		}
	}

	/**
	 * Do the effet of the potion on the hero
	 */
	protected abstract void triggerEffect(Hero hero);
}
