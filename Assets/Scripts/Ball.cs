using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

	[SerializeField] 
	private float gravity;

	[SerializeField] 
	private Transform startingPosition;
	

	public Transform StartingPosition
	{
		get { return startingPosition; }
		set { startingPosition = value; }
	}

	private Rigidbody rb;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		MoveToStart();
		
	}

	private void FixedUpdate () 
	{
		if (gravity != 0f)
            rb.AddForce(Vector3.down * gravity * Time.deltaTime);
		
	}

	public void MoveToStart()
	{
		transform.position = startingPosition.position;
		rb.velocity = Vector3.zero;
		
		var platform = FindObjectOfType<PlatformMovement>();
		if (platform != null)
			platform.ResetPosition();
	}

}

