using UnityEngine;
using System.Collections;

/**
* @author HugoLS
* @version 1.0
**/
public class BasicDragonet : Dragonet {
	
	void Start () {
		base.Start();
	}
	
	protected void Update () {
		base.Update ();
	}

	/**
	* Constructeur de la classe BasicDragonet
	* @version 1.0
	**/
	/*public BasicDragonet()
		:base(30.0f, 4.5f, 2.0f, 2.0f, 5.0f, 30.0f, 40.0f, 5.0f, "semiDistance", "anonymous"){
		
	}*/

	//public BasicDragonet(float essai_double_constructeur)
	public BasicDragonet()
		:base(EnnemyConfigurator.basicDragonetAggroDistance,
			EnnemyConfigurator.basicDragonetAttackRange,
			EnnemyConfigurator.basicDragonetDistanceToDisappear,
			EnnemyConfigurator.basicDragonetAttackSpeed,
			EnnemyConfigurator.basicDragonetXpGain,
			EnnemyConfigurator.basicDragonetHp,
			EnnemyConfigurator.basicDragonetDamage,
			EnnemyConfigurator.basicDragonetMovementSpeed,
			EnnemyConfigurator.basicDragonetAttackType,
			EnnemyConfigurator.basicDragonetName)
	{

	}
}

