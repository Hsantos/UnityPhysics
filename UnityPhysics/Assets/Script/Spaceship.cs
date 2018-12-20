using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class Spaceship : MonoBehaviour
{
    public float height = 10f;
    public float defaultSpeed = 2f;
    public float calcSpeed;
    public Vector3 proportionalDestiny;
    public Vector3 currentDirection;
    public float initialDistance;

    public Vector3 currentDistance;
    private Vector3 initialDestination;
    private Vector3 finalDestination;
    private float proportionalHeight;
    private float calcForce;
    private bool existDestiny;
    private Rigidbody rigidBody;

    void Start()
    {
        float distance = Vector3.Distance(new Vector3(9.7f,0,8.81f), new Vector3(21.9f, 0, 57.3f));//49
        Debug.Log("SHOW DISTANCE: " +  distance);
    }


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
                //propulsão de altura
	            proportionalHeight = (height - ht.distance);
	            calcForce = height - rigidBody.velocity.y;
	            Vector3 appliedHoverForce = (Vector3.up * proportionalHeight * (calcForce));

	            currentDistance = finalDestination - transform.position;
                proportionalDestiny = existDestiny ? currentDistance / initialDistance :  Vector3.zero;
	            calcSpeed = defaultSpeed * currentDistance.magnitude;

	            appliedHoverForce += (proportionalDestiny*calcSpeed);

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

                finalDestination.y = initialDestination.y = 0;

                initialDistance = Vector3.Distance(finalDestination,initialDestination);

                existDestiny = true;

//                GameObject gb = (GameObject)Instantiate(Resources.Load("Capsule"));
            }
        }
    }
    

}
