using UnityEngine;
using System.Collections;

/**
 * @author Adrien D
 * @version 1.0
 */

 /**
  * Controller of Next Level Scene
  */
public class NextLevelController : MonoBehaviour {

	/**
	 * The max timer value
	 */
	private float max;

	/**
	 * The timer
	 */
	private float time;

	// Use this for initialization
	/**
	 * Initialize max timer and timer
	 */
	void Start () {
		max = 3.0f;
		time = 0.0f;
	}
	
	// Update is called once per frame
	/**
	 * Wait for the max time and then save the game + load the next level
	 */
	void Update () {
		time += Time.deltaTime;
		if (time > max) {
			GameModel.Score ++;
			SaveParser.addSave(GameModel.Slot, GameModel.Hero, GameModel.Score, GameModel.ActualLevelId);
			Application.LoadLevel("GameScene");
		}
	}
}
