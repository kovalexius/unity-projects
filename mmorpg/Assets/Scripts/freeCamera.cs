using UnityEngine;
using System.Collections;

public class freeCamera : MonoBehaviour 
{
  float rotationY = 0F;
  public float minimumX = -360F;
  public float maximumX = 360F;

  public float minimumY = -60F;
  public float maximumY = 60F;

  public float sensitivityX = 15F;
  public float sensitivityY = 15F;

	void Start () 
  {
	
	}
	
	void Update () 
  {
    float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
    rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
    rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
    transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);

    Vector3 forward = new Vector3(0, 0, Input.GetAxis("Vertical"));
    transform.position += forward;
    
	}
}
