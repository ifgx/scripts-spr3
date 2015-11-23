using UnityEngine;
using System.Collections;

public class PowerPotion : Potion {

	private float gain = 200.0f;

	protected override void triggerEffect(Hero hero) {
		hero.PowerQuantity += gain;
	}
}
