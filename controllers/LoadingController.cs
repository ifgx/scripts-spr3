using UnityEngine;
using System.Collections;

public class LoadingController : MonoBehaviour {
	

	// Update is called once per frame
	void Start () {
		GameModel.Init ();
		Application.LoadLevel ("Main_menu");
	}
}
