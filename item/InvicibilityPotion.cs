using UnityEngine;
using System.Collections;

public class InvicibilityPotion : Potion {

	//private float gain = 200.0f;

	protected override void triggerEffect(Hero hero) {
		hero.makeInvicible (7.0f);
	}
}
