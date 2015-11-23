using UnityEngine;
using System.Collections;
using System.Collections.Generic; // Lib for List<GameObject>

/**
* @author HugoLS
* @version 1.0
**/
public class EnnemyWeaponCollider : MonoBehaviour {


	void OnTriggerEnter(Collider hit)
	{
		if(hit.gameObject.tag == "hero_defense")
		{
			Hero hero = hit.gameObject.GetComponentInParent<Hero>();
			hero.DefenseMode("on");
			NPC parent = this.gameObject.GetComponentInParent<NPC>();
			parent.Blocked();

			if (hit.gameObject.name == "Vortex(Clone)")
			{
				if (this.gameObject.name == "FireballDragonnet(Clone)" || this.gameObject.name == "Ball(Clone)" )
				{
					Destroy(this.gameObject);//useless to exit trigger
					hero.DefenseMode("off"); 
				}
			}

		}
	}

	void OnTriggerExit(Collider hit)
	{

		if(hit.gameObject.tag == "hero_defense")
		{
			Hero hero = hit.gameObject.GetComponentInParent<Hero>();
			hero.DefenseMode("off");
		}
	}
}