using UnityEngine;
using System.Collections;

/**
* @author HugoLS
* @version 1.0
**/
public class Assassin : NPC {

	void Awake(){

	}

	void Start () {
		gameObject.transform.Rotate(0,180,0);
	}
	
	protected void Update () {
		base.Update ();
	}

	/**
	* Constructeur de la classe Assassin
	* @version 1.0 
	**/
	/*public Assassin()
		:base(30.0f, 4.5f, 2.0f, 2.0f, 8.0f, Blocking.FREE, 40.0f, 100.0f, 12.0f, "derriere", "anonzaymous"){
		//attackSpeed, xpGain, blocking, hp, damage, movementSpeed, attackType, name
	}*/

	//public Assassin(float essai_double_constructeur)
	public Assassin()
		:base(EnnemyConfigurator.assassinAggroDistance,
			EnnemyConfigurator.assassinAttackRange,
			EnnemyConfigurator.assassinDistanceToDisappear,
			EnnemyConfigurator.assassinAttackSpeed,
			EnnemyConfigurator.assassinXpGain,
			Blocking.FREE,
			EnnemyConfigurator.assassinHp,
			EnnemyConfigurator.assassinDamage,
			EnnemyConfigurator.assassinMovementSpeed,
			EnnemyConfigurator.assassinAttackType,
			EnnemyConfigurator.assassinName)
	{

	}

	public override void Attack(Hero target)
	{
		target.LostHP(this.Damage);
		target.PlayBloodAnimation();
	}
}
