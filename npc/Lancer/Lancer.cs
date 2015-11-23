using UnityEngine;
using System.Collections;

/**
* @author HugoLS
* @version 1.0
**/
public abstract class Lancer : NPC {

	Animation animation;
	
	AudioSource audio;

	protected void Awake(){
		animation = GetComponentInChildren<Animation>();
		audio = GetComponentInChildren<AudioSource>();
	}

	// Use this for initialization
	protected void Start () {
		
	}
	
	// Update is called once per frame
	protected void Update () {
		base.Update ();
	}

	/**
	* FR:
	* Constructeur de la classe Lancer
	* Les paramètres attackSpeed, xpGain, Blocking, hp, damage, movementSpeed, attackType, name sont transmis lors de la construction à la classe mère (NPC) de Lancer.
	* @see NPC
	*
	* EN:
	* Lancer class constructor
	* Parameters attackSpeed, xpGain, Blocking, hp, damage, movementSpeed, attackType, name are sent to the mother class (NPC) during the build of an Lancer
	* @see NPC
	*
	* @version 1.0
	**/
	public Lancer(float aggroDistance, float attackRange, float distanceToDisappear, float attackSpeed, float xpGain, float hp, float damage, float movementSpeed, string attackType, string name)
		:base(aggroDistance, attackRange, distanceToDisappear, attackSpeed, xpGain, Blocking.FREE, hp, damage, movementSpeed, attackType, name){
	
	}

	public override void Attack(Hero target)
	{
		if(NbAttack == 0 )
		{
			animation.CrossFadeQueued("Attack",0.2f);
			PlayAttackSound();
			NbAttack = NbAttack+1;
			//Die(10.0f);
			LastAttack = Time.time;
		}
		else
		{
			Die(10.0f);
		}
	}
}
