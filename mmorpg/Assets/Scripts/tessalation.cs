using UnityEngine;
using System.Collections;

public class tessalation : MonoBehaviour 
{
  Terrain terrain1, terrain2;
  void Start()
  {
    
    terrain1 = GameObject.Find("Terrain1").GetComponent<Terrain>();
    terrain2 = GameObject.Find("Terrain2").GetComponent<Terrain>();
    terrain1.heightmapMaximumLOD = 0;
    terrain2.heightmapMaximumLOD = 0;
    terrain1.detailObjectDistance = 10000;
    terrain2.detailObjectDistance = 10000;
  }
	
	void Update () 
  {
    if (Input.GetKeyDown(KeyCode.L))
    {
      if (terrain1.heightmapMaximumLOD > 0)
      {
        Debug.Log("terrain1 heightmap0");
        terrain1.heightmapMaximumLOD = 0;
      }
      else
      {
        Debug.Log("terrain1 heightmap 4");
        terrain1.heightmapMaximumLOD = 4;
      }

      if (terrain2.heightmapMaximumLOD > 0)
      {
        Debug.Log("terrain2 heightmap0");
        terrain2.heightmapMaximumLOD = 0;
      }
      else
      {
        Debug.Log("terrain2 heightmap 4");
        terrain2.heightmapMaximumLOD = 4;
      }

      
    }
    if (Input.GetKeyDown(KeyCode.H))
    {
      if (Camera.main.hdr)
      {
        Camera.main.hdr = false;
        Debug.Log("falseHdr");
      }
      else
      {
        Camera.main.hdr = true;
        Debug.Log("trueHdr");
      }
    }
	}
}
