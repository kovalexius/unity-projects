using UnityEngine;
using System.Collections;

public class personCamera : MonoBehaviour
{
  public Transform target;
  public float R=15;
  float R1;
  //public int div = 100;
  //int divide = 0;

  float x, y;
  public float sensitivityX = 2;
  public float sensitivityY = 2;
  public float sensitivityWheel = 10;
  public float height = 0.5f;

  void Start () 
  {
    x = 0;
    y = 0;
    R = (transform.position - target.position).magnitude;

    Update();
	}

  void Update()
  {
    x += Input.GetAxis("Mouse X") * sensitivityX;
    y -= Input.GetAxis("Mouse Y") * sensitivityY;
    //R -= Input.GetAxis("Mouse ScrollWheel") * sensitivityWheel;
    //h = Input.GetAxis("Mouse ScrollWheel") * sensitivityWheel;
    if (Input.GetAxis("Mouse ScrollWheel") != 0)
    {
      R1 = R + Input.GetAxis("Mouse ScrollWheel") * sensitivityWheel;
      //divide += div;
    }

    Quaternion rotation = Quaternion.Euler(y, x, 0);
    Vector3 vec = Vector3.zero;
    vec.z = -R;
    vec.y = height;
    Vector3 position = rotation * vec + target.position;

    transform.rotation = rotation;
    transform.position = position;
  }
}
