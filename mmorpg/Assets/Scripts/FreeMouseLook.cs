using UnityEngine;
using System.Collections;

public class FreeMouseLook : MonoBehaviour 
{
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;

	public float minimumX = -360F;
	public float maximumX = 360F;

	public float minimumY = -90F;
	public float maximumY = 90F;
  public float speed = 10;

	float rotationY;

  void Start()
  {
    rotationY = -transform.localEulerAngles.x;
    if (rigidbody)
      rigidbody.freezeRotation = true;
  }

	void Update ()
	{
    if(Input.GetButton("Fire2"))
    {
		  float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
		  rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
		  rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
	    transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
    }

    Vector3 vec = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    Vector3 forward = Vector3.Project(vec, Vector3.forward);
    Vector3 side = Vector3.Project(vec, Vector3.right);

    transform.position += transform.forward * forward.sqrMagnitude * Vector3.Dot(vec, Vector3.forward)*speed + transform.right * side.sqrMagnitude * Vector3.Dot(vec, Vector3.right)*speed;
	}
}