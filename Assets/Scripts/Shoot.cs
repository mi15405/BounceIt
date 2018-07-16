using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {
	
    // ROTATE
	[SerializeField] 
	private float rotateSpeed;

	// AIM
	[SerializeField] 
	private Transform aimFrom;

	[SerializeField] private string horizontalAxis;
	[SerializeField] private string verticalAxis;

    // FORCE
	[SerializeField] 
	private float forceChargeTime;
	private float forceCharge;
	private float fireForce;

	[SerializeField]
	private float minFireForce;
	
	[SerializeField]
	private float maxFireForce;

    // SHOOT
	private bool shootPressed;
	private bool shootReleased;

    // EFFECTS
    [SerializeField]
    private ParticleSystem shootParticlePrefab;
    private ParticleSystem shootParticle;

    [SerializeField]
    private float shootParticleDuration;
    private float particleLifetime = 0f;
	
	
	private Vector3 fireDirection = Vector3.forward;

	private Rigidbody rb;

	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
        shootParticle = Instantiate(shootParticlePrefab, transform);
        shootParticle.transform.position = transform.position;

        if (shootParticle.isPlaying)
            shootParticle.Stop();
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
        UpdateParticles();        
	}

	private void GetInput()
	{
		fireDirection.z = Input.GetAxis(horizontalAxis);
		fireDirection.y = Input.GetAxis(verticalAxis);
		shootPressed = Input.GetButton("Push");
		shootReleased = Input.GetButtonUp("Push");	
	}
	
	private void UpdateAim()
	{
		// Set position to ball's position
		aimFrom.position = transform.position;

		if (fireDirection == Vector3.zero)
        {
            aimFrom.gameObject.SetActive(false);
			return;
        }
        else if (!aimFrom.gameObject.activeInHierarchy)
        {
            aimFrom.gameObject.SetActive(true);
            aimFrom.rotation = Quaternion.LookRotation(fireDirection);
        }

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
            
            // Play shooting particles
            particleLifetime = shootParticleDuration;
            shootParticle.Play();
        }	
		else if (shootPressed)
		{
			fireForce += forceCharge * Time.deltaTime;
		}
	}

    private void UpdateParticles()
    {
        if (particleLifetime > 0f)
            particleLifetime -= Time.deltaTime;
        
        if (particleLifetime <= 0f && shootParticle.isPlaying)
            shootParticle.Stop();
    }
}
