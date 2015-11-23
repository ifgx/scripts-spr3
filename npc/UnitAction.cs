using UnityEngine;
using System.Collections;

public class UnitAction {

	public Vector3 coords;
	float damage;
	bool isAttack = false;
	bool isDistant = false;
	bool isRun = false;
	bool isDisappear = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public UnitAction(float x,float y,float z)
	{
		coords = new Vector3(x,y,z);
	}

	public void SetActionAsAttack(float damage)
	{
		Damage = damage;
		isAttack = true;
	}

	public void SetActionAsDistant()
	{
		isDistant = true;
	}

	public void SetActionAsDisappear()
	{
		isDisappear = true;
	}

	public void SetActionAsRun()
	{
		isRun = true;
	}

	public float Damage {
		get {
			return this.damage;
		}
		set {
			damage = value;
		}
	}

	public bool IsAttack {
		get {
			return this.isAttack;
		}
		set {
			isAttack = value;
		}
	}

	public bool IsRun {
		get {
			return this.isRun;
		}
		set {
			isRun = value;
		}
	}

	public bool IsDisappear {
		get {
			return this.isDisappear;
		}
		set {
			isDisappear = value;
		}
	}
}
