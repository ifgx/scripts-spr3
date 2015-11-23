/******************************************************************************\
* Copyright (C) Leap Motion, Inc. 2011-2014.                                   *
* Leap Motion proprietary. Licensed under Apache 2.0                           *
* Available at http://www.apache.org/licenses/LICENSE-2.0.html                 *
\******************************************************************************/
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Leap;

using Game;

/**
* The Controller object that instantiates hands and tools to represent the hands and tools tracked
* by the Leap Motion device.
*
* HandController is a Unity MonoBehavior instance that serves as the interface between your Unity application
* and the Leap Motion service.
*
* The HandController script is attached to the HandController prefab. Drop a HandController prefab 
* into a scene to add 3D, motion-controlled hands. The hands are placed above the prefab at their 
* real-world relationship to the physical Leap device. You can change the transform of the prefab to 
* adjust the orientation and the size of the hands in the scene. You can change the 
* HandController.handMovementScale property to change the range
* of motion of the hands without changing the apparent model size.
*
* When the HandController is active in a scene, it adds the specified 3D models for the hands to the
* scene whenever physical hands are tracked by the Leap Motion hardware. By default, these objects are
* destroyed when the physical hands are lost and recreated when tracking resumes. The asset package
* provides a variety of hands that you can use in conjunction with the hand controller. 
* 
* @author LeapMotion edited by Baptiste Valthier
*/
public class HandController : MonoBehaviour
{

  
	public HandModel leftPhysicsModel_gtwo;

	// Reference distance from thumb base to pinky base in mm.
	protected const float GIZMO_SCALE = 5.0f;
	/** Conversion factor for millimeters to meters. */
	protected const float MM_TO_M = 0.001f;

	/** Whether to use a separate model for left and right hands (true); or mirror the same model for both hands (false). */ 
	public bool separateLeftRight = false;
	/** The GameObject containing graphics to use for the left hand or both hands if separateLeftRight is false. */
	public HandModel leftGraphicsModel;
	/** The GameObject containing colliders to use for the left hand or both hands if separateLeftRight is false. */
	public HandModel leftPhysicsModel;
	/** The graphics hand model to use for the right hand. */
	public HandModel rightGraphicsModel;
	/** The physics hand model to use for the right hand. */
	public HandModel rightPhysicsModel;
	// If this is null hands will have no parent
	public Transform handParent = null;
	private GameObject fireball = null;
	private GameObject fireballGo = null;
	private GameObject vortex = null;
	private GameObject vortexGo = null;
	private GameController gameController = null;
	private RawImage pointerImage;
	private bool pointerMode = false;



	/** If hands are in charge of Destroying themselves, make this false. */
	public bool destroyHands = true;

	/** The scale factors for hand movement. Set greater than 1 to give the hands a greater range of motion. */
	public Vector3 handMovementScale = Vector3.one;


  
	/** The underlying Leap Motion Controller object.*/
	protected Controller leap_controller_;

	/** The list of all hand graphic objects owned by this HandController.*/
	protected Dictionary<int, HandModel> hand_graphics_;
	/** The list of all hand physics objects owned by this HandController.*/
	protected Dictionary<int, HandModel> hand_physics_;
	private bool flag_initialized_ = false;
	private long prev_graphics_id_ = 0;
	private long prev_physics_id_ = 0;
	private string heroClass = null;
	private HandSide handSide;
	private Hero hero = null;
	public const float TIME_ABOVE_PAUSE = 1.5f;
	private float timeLeftBeforePause = TIME_ABOVE_PAUSE;
	private HandModel lefthand = null;
	private HandModel righthand = null;

	/**
	 * @author Baptiste Valthier
	 * defines whether the user is left handed or not and choose the appropriate graphics and features according to the class.
	 **/
	public void setModel(HandSide hs, Hero _hero)
	{

		hero = _hero;
		//saves the value locally
		heroClass = hero.GetType().ToString();
		handSide = hs;

		string prefab = heroClass + "_";
		//puts the right-handed or left-handed attribute to the prefab name
		prefab += (hs == HandSide.RIGHT_HAND ? "RH" : "LH");

		//sets Left hand model
		GameObject leftGO = Resources.Load("prefabs/leapmotion/" + prefab + "_left") as GameObject;
		//GameObject leftGO = Resources.Load("prefabs/leapmotion/PepperLightFullLeftHand") as GameObject;

		if (leftGO == null) {
			Debug.LogError("Baptiste says : Can't find GameObject " + "prefabs/leapmotion/" + prefab + "_left" + ". Does it exists?");
		}
		leftGraphicsModel = leftGO.GetComponent<RiggedHandBV>();

		GameObject rightGO = Resources.Load("prefabs/leapmotion/" + prefab + "_right") as GameObject;
		if (rightGO == null) {
			Debug.LogError("Baptiste says : Can't find GameObject " + "prefabs/leapmotion/" + prefab + "_right" + ". Does it exists?");
		}
		rightGraphicsModel = rightGO.GetComponent<RiggedHandBV>();


		//load extra prefabs if needed
		if (heroClass == "Wizard") {
			fireballGo = Resources.Load("prefabs/leapmotion/Fireball") as GameObject;
			vortexGo = Resources.Load("prefabs/leapmotion/Vortex") as GameObject;

		}

	}
	
	public void setGameController(GameController gc)
	{
		gameController = gc;
	}

	/** Draws the Leap Motion gizmo when in the Unity editor. */
	void OnDrawGizmos()
	{
		// Draws the little Leap Motion Controller in the Editor view.
		Gizmos.matrix = Matrix4x4.Scale(GIZMO_SCALE * Vector3.one);
		Gizmos.DrawIcon(transform.position, "leap_motion.png");
	}

	/** 
  * Initializes the Leap Motion policy flags.
  * The POLICY_OPTIMIZE_HMD flag improves tracking for head-mounted devices.
  */
	void InitializeFlags()
	{
		// Optimize for top-down tracking if on head mounted display.
		Controller.PolicyFlag policy_flags = leap_controller_.PolicyFlags;
 
		policy_flags &= ~Controller.PolicyFlag.POLICY_OPTIMIZE_HMD;

		leap_controller_.SetPolicyFlags(policy_flags);
	}

	/** Creates a new Leap Controller object. */
	void Awake()
	{
		leap_controller_ = new Controller();
	}



	/** Initalizes the hand and tool lists and recording, if enabled.*/
	void Start()
	{
		// Initialize hand lookup tables.
		hand_graphics_ = new Dictionary<int, HandModel>();
		hand_physics_ = new Dictionary<int, HandModel>();
		
	
		
		GameObject rawImageObject = GameObject.Find("PointerImage");
		pointerImage = rawImageObject.GetComponent<RawImage>();



		if (leap_controller_ == null) {
			Debug.LogWarning(
          "Cannot connect to controller. Make sure you have Leap Motion v2.0+ installed");
		}
			

	}

	/**
  * Turns off collisions between the specified GameObject and all hands.
  * Subject to the limitations of Unity Physics.IgnoreCollisions(). 
  * See http://docs.unity3d.com/ScriptReference/Physics.IgnoreCollision.html.
  */
	public void IgnoreCollisionsWithHands(GameObject to_ignore, bool ignore = true)
	{
		foreach (HandModel hand in hand_physics_.Values)
			Leap.Utils.IgnoreCollisions(hand.gameObject, to_ignore, ignore);
	}


	/** Creates a new HandModel instance. */
	/** BV : passing leap_hand because we need to know if it's a right or left and detected by LMC before instantiate*/
	protected HandModel CreateHand(HandModel model, Hand leap_hand)
	{

		if (righthand != null && lefthand == null) {
			//create left hand only
			model = leftGraphicsModel;
			lefthand = model;
			//Debug.Log("CreateHand: create lefthand");

		} else if (lefthand != null && righthand == null) {
			//create righthand only
			model = rightGraphicsModel;
			righthand = model;
			//Debug.Log("CreateHand: create righthand");
		} else if (lefthand != null && righthand != null) { 
			//Debug.Log("CreateHand: hands slots busy. Creating zob");
			//already both hands set !
			return null;
		} else if (lefthand == null && righthand == null) {			
			//do nothing, keep the parameter model
			if (leap_hand.IsLeft) {
				lefthand = model;
				//Debug.Log("CreateHand: both slots free, choosing left");
			} else {
				righthand = model;
				//Debug.Log("CreateHand: both slots free choosing right");
			}
		}



		HandModel hand_model = Instantiate(model, transform.position, transform.rotation)
                           as HandModel;
		hand_model.gameObject.SetActive(true);
		Leap.Utils.IgnoreCollisions(hand_model.gameObject, gameObject);
		if (handParent != null) {
			hand_model.transform.SetParent(handParent.transform);
		}
	
		//We attach the hero to transmit damages
		if (heroClass == "Warrior") {
			if (hand_model.GetComponentInChildren<HeroLinkWeapon>() != null) {
				hand_model.GetComponentInChildren<HeroLinkWeapon>().Hero = hero;
			}
		}
		return hand_model;
	}

	/** 
  * Destroys a HandModel instance if HandController.destroyHands is true (the default).
  * If you set destroyHands to false, you must destroy the hand instances elsewhere in your code.
  * EDIT BV : put back the hands in the initial position
  */
	protected void DestroyHand(HandModel hand_model)
	{
    
		if (hand_model.GetLeapHand().IsLeft) {
			lefthand = null;
			//Debug.Log("DestroyHand: minus left hand");
		} else {
			righthand = null;
			//Debug.Log("DestroyHand: minus right hand");
		}

		if (destroyHands)
			Destroy(hand_model.gameObject);
		else
			hand_model.SetLeapHand(null);
		//Debug.Log("DestroyHand " + hand_model);
	}

	/** 
  * Updates hands based on tracking data in the specified Leap HandList object.
  * Active HandModel instances are updated if the hand they represent is still
  * present in the Leap HandList; otherwise, the HandModel is removed. If new
  * Leap Hand objects are present in the Leap HandList, new HandModels are 
  * created and added to the HandController hand list. 
  * @param all_hands The dictionary containing the HandModels to update.
  * @param leap_hands The list of hands from the a Leap Frame instance.
  * @param left_model The HandModel instance to use for new left hands.
  * @param right_model The HandModel instance to use for new right hands.
  */
	protected void UpdateHandModels(Dictionary<int, HandModel> all_hands,
                                  HandList leap_hands,
                                  HandModel left_model, HandModel right_model)
	{
		List<int> ids_to_check = new List<int>(all_hands.Keys);

		// Go through all the active hands and update them.
		int num_hands = leap_hands.Count;
		//Debug.Log("Active hands: "+num_hands +" / " +all_hands.Count);
		

		
		for (int h = 0; h < num_hands; ++h) {
			Hand leap_hand = leap_hands [h];
      
			HandModel model = (leap_hand.IsLeft) ? left_model : right_model;

		

			// If we've mirrored since this hand was updated, destroy it.
			/*if (all_hands.ContainsKey(leap_hand.Id) &&
          all_hands[leap_hand.Id].IsMirrored() != mirrorYAxis) {
        DestroyHand(all_hands[leap_hand.Id]);
        all_hands.Remove(leap_hand.Id);
      }*/

			// Only create or update if the hand is enabled.
			if (model != null) {
				ids_to_check.Remove(leap_hand.Id);

				// Create the hand and initialized it if it doesn't exist yet.
				//EDIT BV and if not both hands sets
				if (!all_hands.ContainsKey(leap_hand.Id)) {
					//Debug.Log("prepare to call CreateHand");
					HandModel new_hand = CreateHand(model, leap_hand);
					//if new_hand is null, it means we already have both hands set. Do not add the new hand.
					if (new_hand != null) {
						new_hand.SetLeapHand(leap_hand);
						//new_hand.MirrorYAxis(mirrorYAxis);
						new_hand.SetController(this);

						// Set scaling based on reference hand.
						float hand_scale = MM_TO_M * leap_hand.PalmWidth / new_hand.handModelPalmWidth;
						new_hand.transform.localScale = hand_scale * transform.lossyScale;

						new_hand.InitHand();
						new_hand.UpdateHand();
						all_hands [leap_hand.Id] = new_hand;
					} //else
						//Debug.Log("can't create hand. States: left " + lefthand + " | right " + righthand);
				} else {
					// Make sure we update the Leap Hand reference.
					HandModel hand_model = all_hands [leap_hand.Id];
					hand_model.SetLeapHand(leap_hand);
					//hand_model.MirrorYAxis(mirrorYAxis);

					// Set scaling based on reference hand.
					float hand_scale = MM_TO_M * leap_hand.PalmWidth / hand_model.handModelPalmWidth;
					hand_model.transform.localScale = hand_scale * transform.lossyScale;
					hand_model.UpdateHand();
				}
			}
		}

		// Destroy all hands with defunct IDs. (hands out)
		for (int i = 0; i < ids_to_check.Count; ++i) {
			//Debug.LogWarning("Destroying hand with defunct ID : " + ((HandModel)all_hands [ids_to_check [i]]));
			//Debug.LogWarning("States: lefthand:"+lefthand +" );

			//try to guess the right slot to free
			if (all_hands [ids_to_check [i]].GetLeapHand().IsLeft)
			{
				if (lefthand == leftGraphicsModel)
					lefthand = null;
				else
					righthand = null;
			}
			else
			{
				if (righthand == rightGraphicsModel)
					righthand = null;
				else
					lefthand = null;
			}

			DestroyHand(all_hands [ids_to_check [i]]);
			all_hands.Remove(ids_to_check [i]);

		}
	}

	/** Returns the Leap Controller instance. */
	public Controller GetLeapController()
	{
		return leap_controller_;
	}

	/**
  * Returns the latest frame object.
  *
  * If the recorder object is playing a recording, then the frame is taken from the recording.
  * Otherwise, the frame comes from the Leap Motion Controller itself.
  */
	public Frame GetFrame()
	{
   

		return leap_controller_.Frame();
	}

	/**
	 * @author Baptiste Valthier
	 * According to the Hero class, recognizes the pattern and adapt the view
	 * And look for Root pattern like "pause/play" movement (hand is near LM)
	 **/
	void DetectSpecialMovements()
	{
		if (heroClass == null)
			return;

		if (heroClass == "Wizard") {
			Frame frame = GetFrame();
			HandList handsInFrame = frame.Hands;

			foreach (Hand hand in handsInFrame) {				
				//if we are going through the attack hand
				if (hand.IsValid && (handSide == HandSide.RIGHT_HAND ? hand.IsRight : hand.IsLeft)) 
				{

					//if we grab and we don't have a fireball in  the hand yet
					if (fireball == null && hand.GrabStrength >= 0.88 && hero.PowerQuantity > HeroConfigurator.wizardAttackCost) 
					{


						Transform handPrefab = transform.parent.FindChild("Wizard_" + (handSide == HandSide.RIGHT_HAND ? "RH" : "LH") + "_right(Clone)");
						//if the LM finds the grab before constructing the hand, we wait to instantiate it
						if (handPrefab != null) {
							//loading fireball in the hand
							fireball = Instantiate(fireballGo);
							fireball.GetComponentInChildren<HeroLinkWeapon>().Hero = hero;

							fireball.transform.parent = handPrefab.FindChild("HandContainer").transform;
							fireball.transform.localPosition = new Vector3(0f, 0f, 0f);
						}
					}
					//if we throw and we have a fireball ready in the hand
					else if (fireball != null && hand.GrabStrength <= 0.2) {

						//reduces mana
						if (fireball.transform.parent != null)
							hero.PowerQuantity -= HeroConfigurator.wizardAttackCost;

						fireball.transform.parent = null;
						fireball.GetComponentInChildren<CapsuleCollider>().enabled = true;
						fireball.GetComponent<Rigidbody>().isKinematic = false;


					}
				} else
				if (hand.IsValid && (handSide == HandSide.RIGHT_HAND ? hand.IsLeft : hand.IsRight)) 
				{
					//defense hand
					if (vortex == null && hand.GrabStrength >= 0.88 && hero.PowerQuantity > HeroConfigurator.wizardDefenseCost) 
					{
						//prepare to create vortex

						Transform handPrefab = transform.parent.FindChild("Wizard_" + (handSide == HandSide.RIGHT_HAND ? "RH" : "LH") + "_left(Clone)");
						//if the LM finds the grab before constructing the hand, we wait to instantiate it
						if (handPrefab != null) {
							//loading vortex
							vortex = Instantiate(vortexGo);
							
							vortex.transform.parent = handPrefab.FindChild("HandContainer").transform;
							vortex.transform.localPosition = new Vector3(0f, 0f, 0f);
						}
					}
					//if we throw 
					else if (vortex != null && hand.GrabStrength <= 0.2) 
					{

						//reduces mana
						if (vortex.transform.parent != transform.parent)
							hero.PowerQuantity -= HeroConfigurator.wizardDefenseCost;

						vortex.transform.parent = transform.parent; //attach to camera
						//vortex.transform.localPosition = new Vector3(0f, 0.3f, 1.5f);

						vortex.GetComponent<VortexController>().isDropped();

						vortex.GetComponentInChildren<CapsuleCollider>().enabled = true;
						//it is dropped so the size is bigger
						vortex.GetComponent<ParticleSystem>().startSize = 0.8f;

						/*SerializedObject so = new SerializedObject(vortex.GetComponent<ParticleSystem>());
						so.FindProperty("ShapeModule.radius").floatValue = 0.5f;
						so.ApplyModifiedProperties();*/

					}
				}
			}
		}
	}

	void Pointer(Frame _frame)
	{
		float appWidth = pointerImage.canvas.pixelRect.width;
		float appHeight = pointerImage.canvas.pixelRect.height;
		
		InteractionBox iBox = _frame.InteractionBox;
		Pointable pointable = _frame.Pointables.Frontmost;
		
		Leap.Vector leapPoint = pointable.StabilizedTipPosition;
		Leap.Vector normalizedPoint = iBox.NormalizePoint(leapPoint, false);
		
		float appX = normalizedPoint.x * appWidth;
		float appY = (1 - normalizedPoint.y) * appHeight;
		//The z-coordinate is not used
		
		pointerImage.rectTransform.position = new Vector2(appX - 25, appHeight - (appY - 25)); 
	}
	
	public void setPointerMode(bool _pointerMode)
	{
		pointerMode = _pointerMode;
		if (pointerMode)
			pointerImage.enabled = true;
		else
			pointerImage.enabled = false;
	}
	
	/** Updates the graphics objects. */
	void Update()
	{
		if (leap_controller_ == null)
			return;
    
		Frame frame = GetFrame();
    
		//if poitnerMode enabled, just check this mode
		if (pointerMode) {
			Pointer(frame);
			return;
		}
	
		/*Hand closestHand = null;
	for (int i=0; i < frame.Hands.Count; i++)
	{
		if (closestHand != null)
			closestHand = (closestHand.PalmPosition.y < frame.Hands[i].PalmPosition.y ? closestHand : frame.Hands[i]);
		else
			closestHand = frame.Hands[i];
	}

	//detect long pose over LM which means Pause
	//Pause will not be implemented in the game. (Movement not suitable)
	if (closestHand != null && gameController != null)
	{
		//((UnityEngine.UI.Text)infoLabel).text = closestHand.PalmVelocity.y.ToString();
		
		
		if (closestHand.PalmPosition.y < 100 && closestHand.PalmVelocity.y < 60)
		{
			if (timeLeftBeforePause <= 0)
			{
				timeLeftBeforePause = TIME_ABOVE_PAUSE;
				gameController.Pause();
			}
			else
				timeLeftBeforePause -= Time.deltaTime;
		}
		else
			//if me out-criterion hte Pause mvt, set the timer back
			timeLeftBeforePause = TIME_ABOVE_PAUSE;
	}*/
		/*else
		((UnityEngine.UI.Text)infoLabel).text = "No hands detected";*/



		if (frame != null && !flag_initialized_) {
			InitializeFlags();
		}
		if (frame.Id != prev_graphics_id_) {
			DetectSpecialMovements();
			UpdateHandModels(hand_graphics_, frame.Hands, leftGraphicsModel, rightGraphicsModel);
			prev_graphics_id_ = frame.Id;
		}
	}

	/** Updates the physics objects */
	void FixedUpdate()
	{
		if (leap_controller_ == null)
			return;

		Frame frame = GetFrame();

		if (frame.Id != prev_physics_id_) {
			UpdateHandModels(hand_physics_, frame.Hands, leftPhysicsModel, rightPhysicsModel);
			prev_physics_id_ = frame.Id;
		}
	}

	/** True, if the Leap Motion hardware is plugged in and this application is connected to the Leap Motion service. */
	public bool IsConnected()
	{
		return leap_controller_.IsConnected;
	}

	/** Returns information describing the device hardware. */
	public LeapDeviceInfo GetDeviceInfo()
	{
		LeapDeviceInfo info = new LeapDeviceInfo(LeapDeviceType.Peripheral);
		DeviceList devices = leap_controller_.Devices;
		if (devices.Count != 1) {
			return info;
		}
		// TODO: Add baseline & offset when included in API
		// NOTE: Alternative is to use device type since all parameters are invariant
		info.isEmbedded = devices [0].IsEmbedded;
		info.horizontalViewAngle = devices [0].HorizontalViewAngle * Mathf.Rad2Deg;
		info.verticalViewAngle = devices [0].VerticalViewAngle * Mathf.Rad2Deg;
		info.trackingRange = devices [0].Range / 1000f;
		info.serialID = devices [0].SerialNumber;
		return info;
	}

	/** Returns a copy of the hand model list. */
	public HandModel[] GetAllGraphicsHands()
	{
		if (hand_graphics_ == null)
			return new HandModel[0];

		HandModel[] models = new HandModel[hand_graphics_.Count];
		hand_graphics_.Values.CopyTo(models, 0);
		return models;
	}

	/** Returns a copy of the physics model list. */
	public HandModel[] GetAllPhysicsHands()
	{
		if (hand_physics_ == null)
			return new HandModel[0];

		HandModel[] models = new HandModel[hand_physics_.Count];
		hand_physics_.Values.CopyTo(models, 0);
		return models;
	}

	/** Destroys all hands owned by this HandController instance. */
	/** Edit BV : hands stay in scene */
	public void DestroyAllHands()
	{
		//Debug.Log("DestroyAllHands() called");
		lefthand = null;
		righthand = null;
		/*if (hand_graphics_ != null) {
      foreach (HandModel model in hand_graphics_.Values)
        Destroy(model.gameObject);

      hand_graphics_.Clear();
    }
    if (hand_physics_ != null) {
      foreach (HandModel model in hand_physics_.Values)
        Destroy(model.gameObject);

      hand_physics_.Clear();
    }*/
	}
  
}
