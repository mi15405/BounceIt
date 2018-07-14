using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {
	
    // ROTATE
	[SerializeField] 
	private float rotateSpeed;

	// SHOOT
	[SerializeField] 
	private Transform aimFrom;

	[SerializeField] 
	private float forceChargeTime;
	private float forceCharge;
	
	[SerializeField]
	private float minFireForce;
	
	[SerializeField]
	private float maxFireForce;
	
	private float fireForce;

	private bool shootPressed;
	private bool shootReleased;
	
	private Vector3 fireDirection = Vector3.forward;

	private Rigidbody rb;

	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}

	private void Start()
	{
		fireForce = minFireForce;
		forceCharge = (maxFireForce - minFireForce) / forceChargeTime;
	}

	private void Update()
	{
		GetInput();
		UpdateAim();
		Fire();	
	}

	private void GetInput()
	{
		fireDirection.z = Input.GetAxis("Horizontal");
		fireDirection.y = Input.GetAxis("Vertical");
		shootPressed = Input.GetButton("Jump");
		shootReleased = Input.GetButtonUp("Jump");	
	}
	
	private void UpdateAim()
	{
		// Set position to ball's position
		aimFrom.position = transform.position;
		
		if (fireDirection == Vector3.zero)
			return;

		var targetRotation = Vector3.RotateTowards(aimFrom.forward, fireDirection, rotateSpeed * Time.deltaTime, 0f);
		aimFrom.rotation = Quaternion.LookRotation(targetRotation);
	}

	private void Fire()
	{
        if (shootReleased)
        {
	        // Limit force value
	        fireForce = Mathf.Clamp(fireForce, minFireForce, maxFireForce);
	        
			rb.AddForce(aimFrom.forward * fireForce, ForceMode.Impulse);
	        fireForce = minFireForce;
        }	
		else if (shootPressed)
		{
			fireForce += forceCharge * Time.deltaTime;
		}
		
	}
}
