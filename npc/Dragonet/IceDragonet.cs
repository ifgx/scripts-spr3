using UnityEngine;
using System.Collections;

/**
* @author HugoLS
* @version 1.0
**/
public class IceDragonet : Dragonet {

	void Start () {
		base.Start();
	}

	// Update is called once per frame
	protected void Update () {
		base.Update ();

		//gameObject.GetComponent<Animation>().CrossFadeQueued("BattementAiles", 0.2F);

		if(Input.GetKey(KeyCode.Space)){
			//gameObject.GetComponent<Animation>().CrossFade("OuvrirBouche", 0.2F);
		}

	}

	/**
	* Constructeur de la classe IceDragonet
	* @version 1.0
	**/
	/*public IceDragonet()
		:base(30.0f, 4.5f, 2.0f, 2.0f, 7.0f, 40.0f, 25.0f, 10.0f, "semiDistance", "anonymous"){
			
	}*/

	//public IceDragonet(float essai_double_constructeur)
	public IceDragonet()
		:base(EnnemyConfigurator.iceDragonetAggroDistance,
			EnnemyConfigurator.iceDragonetAttackRange,
			EnnemyConfigurator.iceDragonetDistanceToDisappear,
			EnnemyConfigurator.iceDragonetAttackSpeed,
			EnnemyConfigurator.iceDragonetXpGain,
			EnnemyConfigurator.iceDragonetHp,
			EnnemyConfigurator.iceDragonetDamage,
			EnnemyConfigurator.iceDragonetMovementSpeed,
			EnnemyConfigurator.iceDragonetAttackType,
			EnnemyConfigurator.iceDragonetName)
	{

	}
}
