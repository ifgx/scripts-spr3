using UnityEngine;
using System.Collections;

public class HeroConfigurator : MonoBehaviour {

	// HERO
	/*public static string ;
	public static float ;*/

	// WARRIOR
	public static string warriorAttackType = "CaC";
	public static float warriorXpQuantity = 0.0f;
	public static float warriorBlockingPercent = 100.0f;
	public static float warriorPowerQuantity = 1000.0f;
	public static float warriorHpRefresh = 20.0f;
	public static float warriorPowerRefresh = 200.0f;
	public static float warriorHp = 1000.0f;
	public static float warriorDamage = 10.0f;
	public static float warriorMovementSpeed = 3.0f;
	public static float warriorRange = 5.0f;

	// MONK
	public static string monkAttackType = "CaC";
	public static float monkXpQuantity = 0.0f;
	public static float monkBlockingPercent = 50.0f;
	public static float monkPowerQuantity = 1000.0f;
	public static float monkHpRefresh = 40.0f;
	public static float monkPowerRefresh = 200.0f;
	public static float monkHp = 1100.0f;
	public static float monkDamage = 8.0f;
	public static float monkMovementSpeed = 3.0f;
	public static float monkRange = 5.0f;

	public static float monkSpeedHeal = 1.0f;
	public static float monkPowerHealConsumption = 10.0f;
	public static float monkHpHealed = 100.0f;

	// WIZARD
	public static string wizardAttackType = "Distance";
	public static float wizardXpQuantity = 0.0f;
	public static float wizardBlockingPercent = 100.0f;
	public static float wizardPowerQuantity = 1000.0f;
	public static float wizardHpRefresh = 10.0f;
	public static float wizardPowerRefresh = 200.0f;
	public static float wizardHp = 1100.0f;
	public static float wizardDamage = 15.0f;
	public static float wizardMovementSpeed = 3.0f;
	public static float wizardRange = 5.0f;
	public static float wizardAttackCost = 100;
	public static float wizardDefenseCost = 70;
	public static float wizardRegenAttack = 230;



	public void Init()
	{
		//TODO
		// Lire dans le fichier JSON

		// Hydrater les variables
	}
}
