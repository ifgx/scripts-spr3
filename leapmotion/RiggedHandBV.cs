/******************************************************************************\
* Copyright (C) Leap Motion, Inc. 2011-2014.                                   *
* Leap Motion proprietary. Licensed under Apache 2.0                           *
* Available at http://www.apache.org/licenses/LICENSE-2.0.html                 *
\******************************************************************************/

using UnityEngine;
using System.Collections;
using Leap;

// Class to setup a rigged hand based on a model.
public class RiggedHandBV : HandModel {
	
  public Vector3 modelFingerPointing = Vector3.forward;
  public Vector3 modelPalmFacing = Vector3.down;

	GameObject leftConstraint = null;
	GameObject rightConstraint = null;
	GameObject topConstraint = null;

  public override void InitHand() {
    UpdateHand();
  }

  public Quaternion Reorientation() {
    return Quaternion.Inverse(Quaternion.LookRotation(modelFingerPointing, -modelPalmFacing));
  }

	protected virtual void LeapPositionPalm(Transform palm)
	{
		palm.position = GetPalmPosition();
		palm.rotation = GetPalmRotation() * Reorientation();
	}

  public override void UpdateHand() 
{
	//caching the contsraints objects
	if (leftConstraint == null)
		leftConstraint = GameObject.FindGameObjectsWithTag("LeftConstraint")[0];
	if (rightConstraint == null)
		rightConstraint = GameObject.FindGameObjectsWithTag("RightConstraint")[0];
	if (topConstraint == null)
		topConstraint = GameObject.FindGameObjectsWithTag("TopConstraint")[0];

	
	if (palm != null) {

			LeapPositionPalm(palm);
		
		
		//add constraint for clamping
		
		//test horinzontal clamp
		//doesn't allow to go further than the LMC wall
		if (palm.position.x <= leftConstraint.transform.position.x )
		{
			palm.position = new Vector3(leftConstraint.transform.position.x , palm.position.y, palm.position.z);
			leftConstraint.GetComponent<WarningLimit>().showLimit();
			
		}
		else if (palm.position.x >= rightConstraint.transform.position.x )
		{
			
			rightConstraint.GetComponent<WarningLimit>().showLimit();
			palm.position = new Vector3(rightConstraint.transform.position.x, palm.position.y, palm.position.z);
		}
		
		
		//test vertical clamp
		if (palm.position.y >= topConstraint.transform.position.y )
		{
			palm.position = new Vector3(palm.position.x , topConstraint.transform.position.y, palm.position.z);
			topConstraint.GetComponent<WarningLimit>().showLimit();
			
		}

		
			
	}

	/*	if (palm != null) {
      palm.position = GetPalmPosition();
      palm.rotation = GetPalmRotation() * Reorientation();
    }*/

    if (forearm != null)
      forearm.rotation = GetArmRotation() * Reorientation();

    for (int i = 0; i < fingers.Length; ++i) {
      if (fingers[i] != null) {
				fingers[i].fingerType = (Finger.FingerType)i;
        fingers[i].UpdateFinger();
			}
		}
  }
}
