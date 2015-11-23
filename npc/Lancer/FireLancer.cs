using UnityEngine;
using System.Collections;

/**
* @author HugoLS
* @version 1.0
**/
public class FireLancer : Lancer {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	protected void Update () {
		base.Update ();
	}
	
	/**
	* Constructeur de la classe FireLancer
	* @version 1.0
	**/
	/*public FireLancer()
		:base(30.0f, 4.5f, 2.0f, 2.0f, 6.0f, 30.0f, 300.0f, 10.0f, "cac", "anonymous"){

	}*/

	//public FireLancer(float essai_double_constructeur)
	public FireLancer()
		:base(EnnemyConfigurator.fireLancerAggroDistance,
			EnnemyConfigurator.fireLancerAttackRange,
			EnnemyConfigurator.fireLancerDistanceToDisappear,
			EnnemyConfigurator.fireLancerAttackSpeed,
			EnnemyConfigurator.fireLancerXpGain,
			EnnemyConfigurator.fireLancerHp,
			EnnemyConfigurator.fireLancerDamage,
			EnnemyConfigurator.fireLancerMovementSpeed,
			EnnemyConfigurator.fireLancerAttackType,
			EnnemyConfigurator.fireLancerName)
	{

	}
}

