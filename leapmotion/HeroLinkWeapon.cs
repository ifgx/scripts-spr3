using UnityEngine;
using System.Collections;

public class HeroLinkWeapon : MonoBehaviour {

	private Hero hero;

	public Hero Hero{
		get{
			return this.hero;
		}
		set{
			this.hero = value;
		}
	}

}
