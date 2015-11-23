using UnityEngine;
using System.Collections;

public class TriggerAggro : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider hit)
	{
		if(hit.gameObject.tag == "Player")
		{
			NPC parent = this.gameObject.GetComponentInParent<NPC>();
			parent.DistanceUnderAggroDist = true;
		}
	}
}
