using UnityEngine;
using System.Collections;

public class agent : MonoBehaviour 
{

  public GameObject character;
	
	void Start () 
  {
	  //character = GameObject.Find("Character");

	}
	
	
	void Update () 
  {
    if ((character.transform.position - gameObject.transform.position).sqrMagnitude < 100)
      gameObject.transform.forward = character.transform.position - gameObject.transform.position;
	}
}
