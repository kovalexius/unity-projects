using UnityEngine;
using System.Collections;

public class moveCharacter : MonoBehaviour 
{
  //CharacterMotor motor;

  void Awake()
  {
    //motor = gameObject.GetComponent<CharacterMotor>();
  }

	void Start () 
  {
	
	}
	
	void Update () 
  {
		/*
    Vector3 vec = new Vector3(Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal"));


    // Get the input vector from kayboard or analog stick
	  var directionVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
	
	  if (directionVector != Vector3.zero) 
    {
		// Get the length of the directon vector and then normalize it
		// Dividing by the length is cheaper than normalizing when we already have the length anyway
		var directionLength = directionVector.magnitude;
		directionVector = directionVector / directionLength;
		
		// Make sure the length is no bigger than 1
		directionLength = Mathf.Min(1, directionLength);
		
		// Make the input vector more sensitive towards the extremes and less sensitive in the middle
		// This makes it easier to control slow speeds when using analog sticks
		directionLength = directionLength * directionLength;
		
		// Multiply the normalized direction vector by the modified length
		directionVector = directionVector * directionLength;
	}
	
	// Apply the direction to the CharacterMotor
    characterParameters chPar = gameObject.GetComponent<characterParameters>();
	motor.inputMoveDirection = transform.rotation * directionVector *chPar.speed;
	motor.inputJump = Input.GetButton("Jump");
	  */
	}
}