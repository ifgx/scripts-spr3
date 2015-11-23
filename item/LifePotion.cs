using UnityEngine;
using System.Collections;

public class LifePotion : Potion {

	private float gain = 200.0f;

	protected override void triggerEffect(Hero hero) {
		hero.HealthPoint += gain;
	}


}
