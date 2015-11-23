using UnityEngine;
using System.Collections;
//using UnityEditor;

/**
* @author HugoLS
* @version 1.0
**/
public class BasicLancer : Lancer {

	// Use this for initialization
	
	void Start () {
		
	}
	

	protected void Update () {
		base.Update ();
	}

	/**
	* Constructeur de la classe BasicLancer
	* @version 1.0
	**/
	/*public BasicLancer()
		:base(30.0f, 4.5f, 2.0f, 2.0f, 5.0f, 30.0f, 300.0f, 10.0f, "cac", "anonymous"){

	}*/

	//public BasicLancer(float essai_double_constructeur)
	public BasicLancer()
		:base(EnnemyConfigurator.basicLancerAggroDistance,
			EnnemyConfigurator.basicLancerAttackRange,
			EnnemyConfigurator.basicLancerDistanceToDisappear,
			EnnemyConfigurator.basicLancerAttackSpeed,
			EnnemyConfigurator.basicLancerXpGain,
			EnnemyConfigurator.basicLancerHp,
			EnnemyConfigurator.basicLancerDamage,
			EnnemyConfigurator.basicLancerMovementSpeed,
			EnnemyConfigurator.basicLancerAttackType,
			EnnemyConfigurator.basicLancerName)
	{

	}
}
