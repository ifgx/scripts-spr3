using UnityEngine;
using System.Collections;

public class AvanceCamera : MonoBehaviour {

	public GameObject personnage;
	Transform perso;
	// Use this for initialization
	void Start () {
		perso = personnage.transform;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = new Vector3(perso.position.x,this.transform.position.y,perso.position.z) ;
	}
}
