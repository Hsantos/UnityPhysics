using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject target;

    public float offsetX = 0f;
    public float offsetY = 5f;
    public float offsetZ = -15f;
	
    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{

      Vector3 targetPosition = new Vector3(target.transform.localPosition.x+offsetX, target.transform.localPosition.y+offsetY, target.transform.localPosition.z+offsetZ);
	  transform.localPosition = targetPosition;
        
	}
}
