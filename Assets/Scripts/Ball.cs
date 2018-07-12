using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    [SerializeField]
    private float gravity;

    [SerializeField]
	private Transform startingPosition;

    private Rigidbody rb;

	void Start () 
	{
        rb = GetComponent<Rigidbody>();
		MoveToStart();
	}
	
	void FixedUpdate () 
	{
		if (gravity != 0f)
            rb.AddForce(Vector3.down * gravity * Time.deltaTime);
	}

	public void MoveToStart()
	{
		transform.position = startingPosition.position;
		rb.velocity = Vector3.zero;
	}

}

