using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class animateCharacter : MonoBehaviour 
{
  public GameObject character;
  Animation animationChar;

  float t0;
  int curIdle = 0;
  public float dt;
  List<string> idleNames;

  void Start () 
  {

    animationChar = character.animation;
    
    addClipsToCharacter("animations/kick");
    addClipsToCharacter("animations/run");
    addClipsToCharacter("animations/jump");
    addClipsToCharacter("animations/cellphone");
    addClipsToCharacter("animations/watch");

    idleNames = new List<string>();
    idleNames.Add("idleWatch");
    idleNames.Add("idleCellphone");

    t0 = Time.time;
    //setClipToCharacter("run");
    
    //animationChar.playAutomatically = true;
    //animationChar.Play();
	}

  void addClipsToCharacter(string prefabName)
  {
    Animation anim = ((GameObject)Resources.Load(prefabName)).animation;
    foreach (AnimationState state in anim)
      animationChar.AddClip(state.clip, state.clip.name);
  }

  void setClipToCharacter(string clipName)
  {
    foreach(AnimationState state in animationChar)
      if (clipName == state.clip.name)
        animationChar.clip = state.clip;
  }

	void Update () 
  {
    if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
      animationChar.CrossFade("run");
    else
    {
      if (Input.GetButton("Fire1"))
        animationChar.CrossFade("kick");
      else
        animationChar.CrossFade(idleNames[curIdle]);
    }

    if ((Time.time - t0) > dt)
    {
      curIdle++;
      if (curIdle >= idleNames.Count)
        curIdle = 0;
      t0 = Time.time;
    }
	}

  
}
