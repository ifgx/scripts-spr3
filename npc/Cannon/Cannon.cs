using UnityEngine;
using System.Collections;
using System.Collections.Generic; // Lib for List<GameObject>

/**
* @author HugoLS
* @version 1.0
**/
public class Cannon : NPC {
	
	public GameObject CannonBallPrefab;

	GameObject cannonBall;// = Resources.Load("prefabs/item/Ball") as GameObject;
	HudMaster hudShield;

	Vector3 target;
	float projectileSpeed = EnnemyConfigurator.cannonProjectileSpeed;
	float projectileHeight = EnnemyConfigurator.cannonProjectileHeight;
	float rotationSpeed = EnnemyConfigurator.cannonRotationSpeed;

	AudioSource audio;

	protected void Start () {
		cannonBall = Resources.Load("prefabs/item/Ball") as GameObject;
		audio = GetComponentInChildren<AudioSource>();
		hudShield = GameObject.Find("hudPrefab(Clone)").GetComponent<HudMaster>();
	}
	
	protected void Update () {
		base.Update ();
	}
	
	/**
	* Constructeur de la classe Cannon
	* @version 1.0
	**/
	/*public Cannon()
	:base(30.0f, 20.0f, 2.0f, 2.0f, 0.0f, Blocking.FREE, 60.0f, 75.0f, 0.0f, "distance", "anonymous"){
		// attackSpeed,  xpGain,  blocking,  hp,  damage,  movementSpeed,  attackType,  name
	}*/

	//public Cannon(float essai_double_constructeur)
	public Cannon()
		:base(EnnemyConfigurator.cannonAggroDistance,
			EnnemyConfigurator.cannonAttackRange,
			EnnemyConfigurator.cannonDistanceToDisappear,
			EnnemyConfigurator.cannonAttackSpeed,
			EnnemyConfigurator.cannonXpGain,
			Blocking.FREE,
			EnnemyConfigurator.cannonHp,
			EnnemyConfigurator.cannonDamage,
			EnnemyConfigurator.cannonMovementSpeed,
			EnnemyConfigurator.cannonAttackType,
			EnnemyConfigurator.cannonName)
	{

	}

	/**
	* {@inheritDoc}
	**/
	public override void Attack(Hero target)
	{
		//Debug.Log("cannon try to attack");
		float ZDistance = transform.position.z - target.transform.position.z;
		if(ZDistance > EnnemyConfigurator.cannonMinAttackRange)
		{
			float dist = Vector3.Distance(target.transform.position, transform.position);
			float timeToShoot = Mathf.Abs(dist/projectileSpeed);
			float distHero = 3.0f * timeToShoot;

			Vector3 vectorToTarget = target.transform.position - transform.position;
			vectorToTarget.z += distHero;
			//vectorToTarget.y = 0;

			float randomX = Random.Range(-1.5F, 1.5F)/distHero;
			float randomY = Random.Range(0.0F, 1.0F)/distHero;

			vectorToTarget.x += randomX;
			vectorToTarget.y += randomY;
			
			if(LastAttack + AttackSpeed < Time.time )
			{
				transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(vectorToTarget),rotationSpeed);
				//Debug.Log("vect:"+vectorToTarget);

				// INITIALISATION DE L'ACTION EFFECTUE
				base.Action = new UnitAction(target.transform.position.x,target.transform.position.y,target.transform.position.z);
				base.Action.SetActionAsAttack(Damage);
				base.Action.SetActionAsDistant();

				GameObject projectile = Instantiate(cannonBall) as GameObject;
				projectile.transform.parent = transform;
				projectile.transform.position = new Vector3(transform.position.x,transform.position.y+projectileHeight,transform.position.z);
				Rigidbody rb = projectile.GetComponent<Rigidbody>();
				nextAttackCoords = vectorToTarget - Physics.gravity * timeToShoot * timeToShoot;
				nextAttackCoords.z += 3.0f;
				rb.velocity = transform.TransformDirection(0,1,-projectileSpeed);
				
				//Debug.Log("next:"+nextAttackCoords);
				//hudShield.ShieldActivated = true;
				//hudShield.WorldToShieldPosition(nextAttackCoords);
				
				PlayAttackSound();

				LastAttack = Time.time;
				Destroy(projectile, 5);
			}
		}
	}
	
}
