using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class Spaceship : MonoBehaviour
{
    public float height = 10f;
    private float defaultSpeed = 20f;
    public float calcForce;
    public float calcSpeed;
    public Rigidbody rigidBody;
    public float proportionalHeight;
    public Vector3 proportionalDestiny;
    public Vector3 finalDestination;
    public Vector3 initialDestination;
    public bool existDestiny;
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

	            proportionalDestiny = existDestiny ? (finalDestination - initialDestination) :  Vector3.zero;
	            proportionalDestiny.y = 0;

                calcForce = height - rigidBody.velocity.y;
	            calcSpeed = defaultSpeed - rigidBody.velocity.magnitude;
	            
                Vector3 appliedHoverForce = (Vector3.up * proportionalHeight * (calcForce)) +  (Vector3.Normalize(proportionalDestiny) * calcSpeed);



	            rigidBody.AddForce(appliedHoverForce,ForceMode.Acceleration);
            }
	    }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.collider.gameObject.name +  " | " +  hit.point);
                finalDestination = hit.point;
                initialDestination = transform.position;
                existDestiny = true;
            }
        }
    }
    

}
