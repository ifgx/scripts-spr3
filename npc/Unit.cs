using UnityEngine;
using System.Collections;
using System;

/**
* @author HugoLS
* @version 1.0
**/
public abstract class Unit : MonoBehaviour {

	protected float hp;
	protected float maxHp;
	protected float damage;
	protected float movementSpeed;
	protected string attackType;
	protected string name;
	UnitAction action;
	protected GameObject weapon;
	protected GameObject shield;

	// Use this for initialization
	void Start () {

	}

	protected void Update(){
		
	}

	/**
	* FR:
	* Constructeur de la classe Unit
	* EN:
	* Unit class constructor
	* @see Unit
	*
	* @version 1.0
	**/
	public Unit(float hp, float damage, float movementSpeed, string attackType, string name){
		this.hp = hp;
		this.maxHp = hp;
		this.damage = damage;
		this.movementSpeed = movementSpeed;
		this.attackType = attackType;
		this.name = name;
	}

	/**
	* FR:
	* Getter/Setter de hp
	* EN:
	* Getter/Setter of hp
	* @return 
	* FR:
	*	Retourne un float pour le getter et void pour le setter
	* EN:
	*	Return a float for the getter and void for the setter
	* @version 1.0
	**/
	public float HealthPoint {
		get {
			return this.hp;
		}
		set {
			hp = value;
		}
	}

	/**
	* FR:
	* Getter/Setter de maxHp
	* EN:
	* Getter/Setter of maxHp
	* @return 
	* FR:
	*	Retourne un float pour le getter et void pour le setter
	* EN:
	*	Return a float for the getter and void for the setter
	* @version 1.0
	**/
	public float MaxHealthPoint {
		get {
			return this.maxHp;
		}
		set {
			maxHp = value;
		}
	}

	/**
	* FR:
	* Getter/Setter de action
	* EN:
	* Getter/Setter of action
	* @return 
	* FR:
	*	Retourne une structure action float pour le getter et void pour le setter
	* EN:
	*	Return an action structure for the getter and void for the setter
	* @version 1.0
	**/
	public UnitAction Action {
		get {
			return this.action;
		}
		set {
			action = value;
		}
	}

	/**
	* FR:
	* Getter/Setter de damage
	* EN:
	* Getter/Setter of damage
	* @return 
	* FR:
	*	Retourne un float pour le getter et void pour le setter
	* EN:
	*	Return a float for the getter and void for the setter
	* @version 1.0
	**/
	public virtual float Damage {
		get {
			return this.damage;
		}
		set {
			damage = value;
		}
	}

	/**
	* FR:
	* Getter/Setter de movementSpeed
	* EN:
	* Getter/Setter of movementSpeed
	* @return 
	* FR:
	*	Retourne un float pour le getter et void pour le setter
	* EN:
	*	Return a float for the getter and void for the setter
	* @version 1.0
	**/
	public float MovementSpeed {
		get {
			return this.movementSpeed;
		}
		set {
			movementSpeed = value;
		}
	}

	/**
	* FR:
	* Getter/Setter de attackType
	* EN:
	* Getter/Setter of attackType
	* @return 
	* FR:
	*	Retourne une string pour le getter et void pour le setter
	* EN:
	*	Return a string for the getter and void for the setter
	* @version 1.0
	**/
	public string AttackType {
		get {
			return this.attackType;
		}
		set {
			attackType = value;
		}
	}

	/**
	* FR:
	* Getter/Setter de name
	* EN:
	* Getter/Setter of name
	* @return 
	* FR:
	*	Retourne une string pour le getter et void pour le setter
	* EN:
	*	Return a string for the getter and void for the setter
	* @version 1.0
	**/
	public string Name {
		get {
			return this.name;
		}
		set {
			name = value;
		}
	}

	/**
	* Not used for the moment
	*
	**/
	/*void Attack(float x, float y, float z){

		//Cooldown(0.0);
	}*/

	/**
	* FR:
	* Permet de retirer des points de vie à l'unité
	* EN:
	* Reduce health point of the Unit
	* @return 
	* FR:
	*	Retourne un float
	* EN:
	*	Return an float
	* @version 1.0
	**/
	public float LostHP(float hpLost){
		//Debug.LogWarning ("LOST HP : " + this.GetType().ToString() + " -- " + hpLost);
		hp = hp - hpLost;
		return hp;
	}

	/**
	* FR:
	* 	Permet de redéfinir la position d'une Unité
	* EN:
	* 	Set the position of a Unit
	* @return Return void
	* @version 1.0
	**/
	public void SetPosition(float x,float y, float z)
	{
		this.transform.position = new Vector3 (x, y, z);
	}

	public void WakeUp(float deltaTime)
	{
		float maxHeight = 2f; //edit 1.5 BV
		float speed = 3;
		float height = this.transform.position.y + speed * deltaTime;
		if(maxHeight < height)
		{
			height = maxHeight;
		}
		this.transform.position = new Vector3 (this.transform.position.x, height, this.transform.position.z);
	}

	/**
	* FR:
	* 	Permet de récupérer la position d'une Unité
	* EN:
	* 	Get the position of a Unit
	* @return Return a Vector3 with the x,y and z position
	* @version 1.0
	**/
	public Vector3 GetPosition()
	{
		float x = this.transform.position.x;
		float y = this.transform.position.y;
		float z = this.transform.position.z;

		return new Vector3(x,y,z);
	}
	
	/**
	* FR:
	* 	Fait courir l'unité en fonction du deltaTime
	* EN:
	* 	Do running the Unit according to the deltaTime
	* @return Return void
	* @version 1.0
	**/
	public abstract void Run (float deltaTime);

	/**
	* FR:
	* 	Vérifie si l'unité possède plus de 0 points de vie
	* EN:
	* 	Check if the unit get more than 0 Health point
	* @return Return bool
	* @version 1.0
	**/
	public bool IsDead(){
		if (hp <= 0) {
			return true;
		}
		return false;
	}

	/**
	* FR:
	* 	Tue l'unité en détruisant le gameObject
	* EN:
	* 	Kill the unit by destroying the gameObject
	* @return Return void
	* @version 1.0
	**/
	public void Die()
	{
		Destroy(this.gameObject);
	}

	/**
	* FR:
	* 	Tue l'unité en détruisant le gameObject
	* EN:
	* 	Kill the unit by destroying the gameObject
	* @return Return void
	* @version 1.0
	**/
	public void Die(float timer)
	{
		Destroy(this.gameObject,timer);
	}
}
