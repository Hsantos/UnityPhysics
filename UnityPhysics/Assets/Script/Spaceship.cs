using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class Spaceship : MonoBehaviour
{
    public float height = 10f;
    public float calcForce;
    public Rigidbody rigidBody;
    public float proportionalHeight;
    public Vector3 destination;
    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

	void FixedUpdate () {

	    Debug.DrawRay(transform.position, Vector3.down * height, Color.red);

	    RaycastHit ht;
	    Ray landingRay = new Ray(transform.position, Vector3.down);

	    if (Physics.Raycast(landingRay, out ht, height))
	    {
	        if (ht.transform.gameObject.GetComponent<TerrainControl>())
	        {
	            proportionalHeight = (height - ht.distance);
	            calcForce = height - rigidBody.velocity.y;
                Vector3 appliedHoverForce = Vector3.up * proportionalHeight * (calcForce);
                rigidBody.AddForce(appliedHoverForce,ForceMode.Acceleration);
            }

	    }

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(destination);
        }
    }
    

}
