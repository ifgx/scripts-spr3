using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Leap;

/**
 * @author Baptiste Valthier
 * Gesture Detection class using LeapController
 **/
public class LeapControl : MonoBehaviour {

	/**
	 * La derniere action répérée par le mouvement LeapMotion */
	public enum ActionState
	{
		ATTACK,
		DEFENSE,
		CHEST,
		REST
	}
	
	

	
	public ActionState actionState = ActionState.REST;

	//TODO merge in gameController
	public Vector3 initShieldPosition; //a mettre dans game controller
	public Vector3 initSwordPosition; //a mettre dans game controller
	
	/** The underlying Leap Motion Controller object.*/
	protected Controller leap_controller_;
	
	public Canvas debugLeapCanvasPrefab;
	private Canvas debugLeapCanvas;
	
	protected UnityEngine.UI.Text leapDebugLabel;
	protected UnityEngine.UI.Text movementLabel;
	
	
	public GameObject  attackProjectionPrefab ;
	private GameObject attackProjection;
	public GameObject  defenseProjectionPrefab ;
	private GameObject defenseProjection;
	
	public GameObject pointerAttackHandPrefab;
	private GameObject pointerAttackHand;
	public GameObject pointerDefenseHandPrefab;
	private GameObject pointerDefenseHand;
	
//	Hand rightHand = null;
	//Hand leftHand = null;
	
	float movementScale = 15f;
	
	GameObject heroAsParent;
	
	
	//private int nAction = 0;

	//TODO overwritte with game settigns
/*	private GameController.HandSide attackHand = GameController.HandSide.RIGHT_HAND; //default value
	private GameController.HandSide defenseHand = GameController.HandSide.LEFT_HAND;
*/
	/**
	 * Sets which hand (right or left) is the attack hand.
	 * At the same time, defines which defense hand it is (by inverse logic).
	 * It means you don't have to call both setAttackHand or setDefenseHand
	 * @param Right or Left handside
	 **/
	/*public void setAttackHand(GameController.HandSide attackSide)
	{
		attackHand = attackSide;
		defenseHand = (attackSide == GameController.HandSide.RIGHT_HAND ? GameController.HandSide.LEFT_HAND : GameController.HandSide.RIGHT_HAND);

		setInitPosition();
	}*/

	/**
	 * Sets which hand (right or left) is the attack hand.
	 * At the same time, defines which defense hand it is (by inverse logic)
	 * It means you don't have to call both setAttackHand or setDefenseHand
	 * @param defenseSide La main utilisée par la défense (Gauche ou Droite)
	 **/ 
	/*public void setDefenseHand(GameController.HandSide defenseSide)
	{
		attackHand = (defenseSide == GameController.HandSide.RIGHT_HAND ? GameController.HandSide.LEFT_HAND : GameController.HandSide.RIGHT_HAND);
		defenseHand = defenseSide;

		setInitPosition();

	}
*/
	/**
	 * Defines the base position of defense and attackhand. Called at init
	 **/
/*	private void setInitPosition()
	{
		//switch init pos if lefthanded
		if (attackHand == GameController.HandSide.RIGHT_HAND)
		{
			pointerDefenseHand.transform.localPosition = new Vector3 (-1.65f, -0.25f, 0.1f);
			pointerAttackHand.transform.localPosition = new Vector3(1.65f, -0.25f, 0.1f);
		}
		else
		{
			pointerDefenseHand.transform.localPosition = new Vector3 (1.65f, -0.25f, 0.1f);
			pointerAttackHand.transform.localPosition = new Vector3(-1.65f, -0.25f, 0.1f);
		}
		
		initShieldPosition = pointerDefenseHand.transform.localPosition;
		initSwordPosition = pointerAttackHand.transform.localPosition;
		
		//same init pos
		defenseProjection.transform.position = pointerDefenseHand.transform.position;
		attackProjection.transform.position = pointerAttackHand.transform.position;
	}*/

	/**
	 * Returns the Vector 3 Position of the LeapMotion right or left hand (according to
	 * setAttackHand)
	 * internals pointerDefenseHand/pointerAttackHand must be already initialized
	 * internal attackHand must be set with setAttackHand() first
	 **/
	public Vector3 getAttackPointer()
	{
		return pointerAttackHand.transform.position;
	}

	/**
	 * Returns the Vector 3 Position of the LeapMotion right or left hand (according to
	 * setAttackHand)
	 * internals pointerDefenseHand/pointerAttackHand must be already initialized
	 * internal attackHand must be set with setAttackHand() first
	 **/
	public Vector3 getDefensePointer()
	{
		return pointerDefenseHand.transform.position;
	}

	// Use this for initialization
	/**
	 * Called before GameController calls LeapControl.setParent()
	 **/
	void OnEnable () 
	{
		debugLeapCanvas = Instantiate (debugLeapCanvasPrefab);
		attackProjection = Instantiate (attackProjectionPrefab);
		defenseProjection = Instantiate (defenseProjectionPrefab);
		
		pointerDefenseHand = Instantiate (pointerDefenseHandPrefab);
		pointerAttackHand = Instantiate (pointerAttackHandPrefab);

		leapDebugLabel = debugLeapCanvas.transform.Find("InfoLabel").GetComponent<UnityEngine.UI.Text>();
		movementLabel = debugLeapCanvas.transform.Find("MovementLabel").GetComponent<UnityEngine.UI.Text>();


		//setAttackHand(GameController.HandSide.RIGHT_HAND);
	
		//Debug.Log ("Fin de Start LeapControl.cs ");
		
		
	}

	/**
	 * Called by GameControler to link with parent so the LeapController
	 * follows the camera
	 **/
	public void addParent(GameObject go)
	{
		heroAsParent = go;
		//Debug.Log("add parent: "+go);
		pointerDefenseHand.transform.parent = go.transform;
		pointerAttackHand.transform.parent = go.transform;
		attackProjection.transform.parent = go.transform;
		defenseProjection.transform.parent = go.transform;
	}
	
	/** Creates a new Leap Controller object. */
	void Awake()
	{
		leap_controller_ = new Controller();
		//Debug.Log("Awake, new Controller:"+leap_controller_);
	}
	
	/**
	 * Called in update FPS from Leap Controller
	 * recognizes movement patterns
	 **/
	/*void GestureDetection(Hand hand)
	{

		//detect gestures
		//detect hand forward (attack / defense)
		if (hand != null && hand.IsValid && (hand.PalmPosition.z <= -50) && (hand.PalmVelocity.z <= -500))
		{
			if ((attackHand == GameController.HandSide.LEFT_HAND && hand.IsLeft) || 
			    (attackHand == GameController.HandSide.RIGHT_HAND && hand.IsRight))
				nAction = attack(nAction, hand);
			else
				nAction = defense(nAction);
		}*/
		/*else
		 * //desactivation pour sprint 1 car bug
			//si on a une acceleration vers le haut rapide : chest open
			if ((leftHand != null && leftHand.IsValid && leftHand.PalmVelocity.y > 700 && leftHand.PalmVelocity.z < 300 && leftHand.PalmVelocity.z < 300) || (rightHand != null && rightHand.IsValid && rightHand.PalmVelocity.y > 700  && rightHand.PalmVelocity.z < 300 && rightHand.PalmVelocity.z < 300))
		{
			nAction++;
			
			actionState = ActionState.CHEST;
			timeAction = Time.time;
		}*/
	//}

	/**
	 * Visual clue of attacking
	 * @param nAction action counter (displayed for debug purposes)
	 * @param hand LeapMotion object
	 **/
	public int attack (int nAction, Hand hand)
	{
		nAction++;
		//dbg pps
		
		//attackProjection.GetComponent<Renderer>().enabled = true;
		
		Vector3 pointerPosition = attackProjection.transform.position;
		pointerPosition.x = hand.PalmPosition.ToUnityScaled().x * movementScale;
		pointerPosition.y = hand.PalmPosition.ToUnityScaled().y * movementScale;
		attackProjection.transform.position = pointerPosition;
		
		actionState = ActionState.ATTACK;

		return nAction;
	}

	/**
	 * Visual clue for defense
	 * @param nAction action counter (displayed for debug purposes)
	 **/
	public int defense(int nAction)
	{
		nAction++;
		
		
		defenseProjection.GetComponent<Renderer>().enabled = true;
		
		//reattach shield  to pointer
		defenseProjection.transform.parent = pointerDefenseHand.transform;
		defenseProjection.transform.localPosition = new Vector3(0,0,0);
		
		/*Vector3 pointerPosition = defenseProjection.transform.localPosition;
			pointerPosition.x = leftHand.PalmPosition.ToUnityScaled().x * movementScale;
			pointerPosition.y = leftHand.PalmPosition.ToUnityScaled().y * movementScale;
			defenseProjection.transform.localPosition = pointerPosition;*/
		
		
		
		actionState = ActionState.DEFENSE;


		return nAction;
	}

	/**
	 * returns the last actual state of movement
	 */
	public ActionState getActionState() {
		return actionState;
	}
	
	/**
	 * Get the values of LM controller (polling the service)
	 * and may print for debug purposes the status
	 */
	void Update () {
		
		/*if(leap_controller_.IsConnected && leap_controller_.IsServiceConnected())
		{
			
			//polling de LM
			Frame frame = leap_controller_.Frame();
			
			string frameString;
			
			//debug info
			frameString = "frame_id" + frame.Id;
			frameString += "\nnum_hands" +  frame.Hands.Count;
			frameString += "\nnum_fingers" + frame.Fingers.Count;
			frameString += "\num_fingers" + frame.Fingers.Count;
			
			HandList handsInFrame = frame.Hands;
			
			
			foreach (Hand hand in handsInFrame)
			{
				
				
				if (hand.IsValid && (hand.IsLeft && defenseHand == GameController.HandSide.LEFT_HAND ||
				    hand.IsRight && defenseHand == GameController.HandSide.RIGHT_HAND))
				{
					frameString += "\nLeft hand";
					frameString += "\nLeft hand pos.x :  " + hand.PalmPosition.x;
					frameString += "\nLeft hand postounityscale.x :  " + hand.PalmPosition.ToUnityScaled().x;
					frameString += "\nLeft pointer pos :  " + pointerDefenseHand.transform.position.ToString();
					leftHand = hand;
					//pointerDefenseHand.transform.localPosition = leftHand.PalmPosition.ToUnityScaled();
					Vector3 pointerPosition = pointerDefenseHand.transform.position;
					pointerPosition.x = leftHand.PalmPosition.ToUnityScaled().x * movementScale;
					pointerPosition.y = leftHand.PalmPosition.ToUnityScaled().y * movementScale -1.0f;
					pointerDefenseHand.transform.position = pointerPosition;
					
				}
				else if (hand.IsValid && (hand.IsRight && attackHand == GameController.HandSide.RIGHT_HAND ||
				         hand.IsLeft && attackHand == GameController.HandSide.LEFT_HAND))
				{
					
					rightHand = hand;
					//pointerAttackHand.transform.localPosition = rightHand.PalmPosition.ToUnityScaled();
					
					Vector3 pointerPosition = pointerAttackHand.transform.position;
					pointerPosition.x = rightHand.PalmPosition.ToUnityScaled().x * movementScale;
					pointerPosition.y = rightHand.PalmPosition.ToUnityScaled().y * movementScale -1.0f;
					pointerAttackHand.transform.position = pointerPosition;
					
				}

				//ne cherche les actions que si on est pas déjà en mvt
				if(actionState == ActionState.REST)
					GestureDetection (hand);
				
			}
			
			leapDebugLabel.text = frameString;
			
		}
		else
			//leap not available
		{
			string frameString = ("Leap Service: "+leap_controller_.IsServiceConnected());
			frameString += "\nLeap Connected:"+leap_controller_.IsConnected;
			frameString += "\nswitching to keyboard/mouse mode.";
			
			leapDebugLabel.text = frameString;
		}
		
		

		
		
	
		
		movementLabel.text = actionState.ToString() + " ( "+ nAction.ToString()+" )";*/
	}

	/**
	 * Remet les projections mains gauche et main droite dans leur position de repos (vers le bas de l'écran).
	 * En général, a appeler quand on respasse l'actionState à REST
	 **/
	public void backToInitialPosition()
	{
		//remet en état initial la main gauche
		defenseProjection.transform.parent = heroAsParent.transform;
		Vector3 restPositionL = defenseProjection.transform.position;
		restPositionL.x = initShieldPosition.x;
		restPositionL.y = initShieldPosition.y;
		
		defenseProjection.transform.position =  restPositionL;

		Vector3 restPositionR = attackProjection.transform.position;
		restPositionR.x = initSwordPosition.x;
		restPositionR.y = initSwordPosition.y;
		attackProjection.transform.position = restPositionR;
	}
	
}

