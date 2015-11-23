using UnityEngine;
using System.Collections;

/**
* @author HugoLS
* @version 1.0
**/
public class ChangeCamera : MonoBehaviour {

	public GameObject cameraSubjective;
	public GameObject cameraHaute;


	/**
	* La fonction start permet d'activer la caméra subjective et désactiver la caméra vue d'en haut
	* @version 1.0
	**/
	void Start () {
		cameraSubjective.active = true;
		cameraHaute.active = false;
	}
	
	/**
	* La fonction update permet de changer de caméra en appuyant sur la touche C.
	* Elle permet également de mettre à jour la position de la caméra haute en fonction de la position du joueur.
	* @version 1.0
	**/
	void Update () {
		if (Input.GetKeyDown(KeyCode.C)) {
     			cameraHaute.active = !cameraHaute.active;
 		}
 		cameraHaute.transform.position = new Vector3(0, 80.0f, cameraSubjective.transform.position.z);
	}


}
