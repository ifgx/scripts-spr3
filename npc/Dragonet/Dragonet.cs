﻿using UnityEngine;
using System.Collections;

/**
* @author HugoLS
* @version 1.0
**/
public abstract class Dragonet : NPC {
	
	private GameObject fireball = null;
	private GameObject fireballGo = null;
	
	protected void Awake(){
		
	}

	protected void Start () {
		//gameObject.transform.Rotate(0,180,0);
		fireballGo = Resources.Load("prefabs/leapmotion/FireballDragonnet") as GameObject;
	}
	
	protected void Update () {
		base.Update ();
	
	}
	
	/**
	* Constructeur de la classe Dragonet
	* Les paramètres attackSpeed, xpGain, Blocking, hp, damage, movementSpeed, attackType, name sont transmis lors de la construction à la classe mère (NPC) de Dragonet.
	* attackSpeed
	*					Vitesse d'attaque d'un ennemi. Correspond au nombre de seconde entre chaque attaque.
	* xpGain
	*					Valeur de l'expérience donné au héro lors de sa mort
	* @version 1.0
	**/
	public Dragonet(float aggroDistance, float attackRange, float distanceToDisappear, float attackSpeed, float xpGain, float hp, float damage, float movementSpeed, string attackType, string name)
		:base(aggroDistance, attackRange, distanceToDisappear, attackSpeed, xpGain, Blocking.SEMIBLOCK, hp, damage, movementSpeed, attackType, name){
		
	}

	/**
	* {@inheritDoc}
	**/
	public override void UnderAttackRange(Hero target)
	{
		Attack(target);
		FollowPlayer(target);
		if(SuccessiveBlocked >= 3)
		{
			MoveToAttack();
			SuccessiveBlocked = 0;
		}
	}

	/**
	* {@inheritDoc}
	**/
	public override void Attack(Hero target)
	{
		//CurrentAttackSpeed

		if(LastAttack + CurrentAttackSpeed < Time.time )
		{
			Vector3 vectorToTarget = target.transform.position - transform.position;
			vectorToTarget.z = -vectorToTarget.z;
			//Debug.Log("Dragonnet:"+vectorToTarget);
			//transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(Camera.main.transform.position),10f);
			GetComponentInChildren<Animation>().CrossFadeQueued("Attack",0.2f);
			PlayAttackSound();
			NbAttack = NbAttack+1;
			LastAttack = Time.time;
			//add attacking BV
			if (fireball == null) 
			{
				//loading fireball in the dragonnet
				fireball = Instantiate(fireballGo);
				//fireball.GetComponentInChildren<HeroLinkWeapon>().Hero = hero;
				fireball.transform.parent = transform; //need to attach to this
				//get the lifebar position because dragonnet position is messed up
				fireball.transform.position = this.transform.position; //GetComponentInChildren<Canvas>().transform.position;
				fireball.transform.localPosition = new Vector3(0, 2.3f, 0);
				//fireball.transform.localPosition = new Vector3(0f, 0f, 0f);

			}
		}
		
	}

	/**
	* FR:
	* Cette fonction permet de suivre le héro ciblé
	* EN:
	* This function permit to follow the targeted hero
	* @param hero
	* @version 1.0
	**/
	public void FollowPlayer(Hero target)
	{
		Transform character = target.transform;
		transform.position = new Vector3(transform.position.x,transform.position.y,character.position.z + attackRange);
	}

	/**
	* FR:
	* Cette fonction permet de bouger pour attaquer autre part
	* EN:
	* This function permit to move to attack at another place
	* @version 1.0
	**/
	public void MoveToAttack()
	{
		Vector3 moveToAttack = new Vector3(Random.Range(-2.0F, 2.0F),Random.Range(0.0F, 2.0F),transform.position.z);
		transform.Translate(base.MovementSpeed * moveToAttack * Time.deltaTime, Space.World);
		//transform.position = new Vector3(Random.Range(-2.0F, 2.0F),Random.Range(0.0F, 2.0F),transform.position.z);
	}
}
