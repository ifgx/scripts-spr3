using UnityEngine;
using System.Collections;

/**
* @author HugoLS
* @version 1.0
**/
public class IceLancer : Lancer {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	protected void Update () {
		base.Update ();
	}

	/**
	* Constructeur de la classe iceLancer
	* @version 1.0
	**/
	/*public IceLancer()
		:base(30.0f, 4.5f, 2.0f, 2.0f, 7.0f, 40.0f, 400.0f, 10.0f, "cac", "anonymous"){

	}*/

	//public IceLancer(float essai_double_constructeur)
	public IceLancer()
		:base(EnnemyConfigurator.iceLancerAggroDistance,
			EnnemyConfigurator.iceLancerAttackRange,
			EnnemyConfigurator.iceLancerDistanceToDisappear,
			EnnemyConfigurator.iceLancerAttackSpeed,
			EnnemyConfigurator.iceLancerXpGain,
			EnnemyConfigurator.iceLancerHp,
			EnnemyConfigurator.iceLancerDamage,
			EnnemyConfigurator.iceLancerMovementSpeed,
			EnnemyConfigurator.iceLancerAttackType,
			EnnemyConfigurator.iceLancerName)
	{

	}
}
