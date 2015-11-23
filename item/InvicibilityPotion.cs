using UnityEngine;
using System.Collections;

/**
 * @author Adrien D
 * @version 1.0
 */

/**
 * Invincibility potion script
 */
public class InvicibilityPotion : Potion {

	//private float gain = 200.0f;

	/**
	 * Do the effet of the potion on the hero
	 */
	protected override void triggerEffect(Hero hero) {
		hero.makeInvicible (7.0f);
	}
}
