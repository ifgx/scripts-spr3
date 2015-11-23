using UnityEngine;
using System.Collections;

/**
 * @author Adrien D
 * @version 1.0
 */

 /**
  * The Loading Controller
  * It is the entry point of the game to initialize the Game Model
  */
public class LoadingController : MonoBehaviour {
	

	// Update is called once per frame
	/**
	 * Initialization of Game Model
	 */
	void Start () {
		GameModel.Init ();
		Application.LoadLevel ("Main_menu");
	}
}
