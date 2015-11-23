using UnityEngine;
using System.Collections;

/**
* @author HugoLS
* @version 1.0
**/
public class Warrior : Hero {
	

	float specialCapacityCooldown = 30.0f;
	float specialCapacityTimer = 5.0f;
	
	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Renderer>().material.color = Color.magenta;

	}
	
	// Update is called once per frame
	void Update () {
		base.Update();
	}

	/**
	* Constructeur de la classe Warrior
	* @version 1.0
	**/
	public Warrior()
		:base(5.0f, 0.0f,100.0f,"epee",1000.0f, 2.0f, 2.0f, 1000.0f, 10.0f, 3.0f, "cac", "anonymous"){
		PowerQuantity = 0;
	}

	/**
	* {@inheritDoc}
	**/
	public override void adaptStatAccordingToLevel()
	{
		if(level > 6)
		{
			SpecialCapacityUnlocked = true;
			LastCapacityUsed = Time.time;
		}
		else if(level > 5)
		{
			HpRefresh +=1;
		}
		else if(level > 4)
		{
			HpRefresh +=1;
		}
		else if(level > 3)
		{
			Damage *= 1.1f;
		}
		else if(level > 2)
		{
			MaxHealthPoint *= 1.1f;
		}
	}

	public override float Damage {
		get {
			float coeff = (2 - 1 + 1) * (PowerQuantity / MaxPowerQuantity) + 1;
			return this.damage * coeff;
		}
		set {
			damage = value;
		}
	}

	/**
	* {@inheritDoc}
	**/
	public override void LostHP(float damageEnemy)
	{
		float damageToLost = 0.0f;

		if(!SpecialCapacity)
		{
			if(Defending)
			{
				damageToLost = damageEnemy - (blockingPercent*damageEnemy/100);
				PowerQuantity += damageEnemy/2.0f;
			}
			else
			{
				damageToLost = damageEnemy;
				PowerQuantity += damageEnemy;
			}
		}
		base.LostHP(damageToLost);
	}


	/**
	* {@inheritDoc}
	**/
	public override void SpecialCapacitySpell()
	{
		if(LastCapacityUsed + specialCapacityCooldown < Time.time) // Si le cooldown est passé
		{
			if(LastCapacityUsed + specialCapacityTimer < Time.time ) // Si le temps timer est passé on met a off
			{
				specialCapacity = false;
			}
			else													// Sinon on est en cours de spéCapacity
			{
				if(!specialCapacity)								// Si la capacité n'est pas encore déclenché on l'enclenché et la time
				{
					specialCapacity = true;
					LastCapacityUsed = Time.time;
				}
			}			
		}
	}

	public override void PreAttack()
	{

	}

	public override void PostAttack()
	{
		PowerQuantity /= 2;
	}
	
}

