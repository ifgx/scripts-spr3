/******************************************************************************\
* Copyright (C) Leap Motion, Inc. 2011-2014.                                   *
* Leap Motion proprietary. Licensed under Apache 2.0                           *
* Available at http://www.apache.org/licenses/LICENSE-2.0.html                 *
\******************************************************************************/
using UnityEngine;
using System.Collections;
using Leap;

// Class to setup a rigged hand based on a model.
public class RiggedHandClamp : RiggedHandBV
{
	
	/*public Vector3 modelFingerPointing = Vector3.up;
	public Vector3 modelPalmFacing = Vector3.forward;*/




	protected override void LeapPositionPalm (Transform palm)
	{
		palm.position = GetPalmPosition ();
		//palm.rotation = GetPalmRotation() * Reorientation();
	}
}
