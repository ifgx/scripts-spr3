using UnityEngine;
using System.Collections;

/**
* @author HugoLS
* @version 1.0
**/
public class Monk : Hero {
	
	float specialCapacityCooldown = 30.0f;
	float specialCapacityTimer = 0.0f;
	public bool prayerMode;
	public float lastHeal;
	public float speedHeal;
	public float powerHealConsumption;
	public float hpHealed;
	
	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Renderer>().material.color = Color.yellow;
	}
	
	// Update is called once per frame
	void Update () {
		if(prayerMode)
		{
			Prayer();
		}
	}

	/**
	* Constructeur de la classe Monk
	* @version 1.0
	**/
	public Monk()
		:base(5.0f, 0.0f,50.0f,"armeHast",1000.0f, 1.0f, 1.0f, 800.0f, 15.0f, 3.0f, "semiDistance", "anonymous"){
		prayerMode = false;
		lastHeal = Time.time;
		speedHeal = HeroConfigurator.monkSpeedHeal;
		powerHealConsumption = HeroConfigurator.monkPowerHealConsumption;
		hpHealed = HeroConfigurator.monkHpHealed;
	}

	public void Prayer()
	{
		if(PowerQuantity <= 0.0f)
		{
			prayerMode = false;
		}
		else
		{
			if(lastHeal + speedHeal < Time.time)
			{
				PowerQuantity -= powerHealConsumption;
				HealthPoint += hpHealed;
				lastHeal = Time.time;
			}
		}
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
			Damage *= 1.1f;
		}
		else if(level > 3)
		{
			MaxHealthPoint *= 1.1f;
		}
		else if(level > 2)
		{
			MaxHealthPoint *= 1.1f;
		}
	}

	/**
	* {@inheritDoc}
	**/
	public override void SpecialCapacitySpell()
	{
		if(LastCapacityUsed + specialCapacityCooldown < Time.time) // Si le cooldown est passé
		{
			specialCapacity = true;
			LastCapacityUsed = Time.time;
		}
	}
	
}


