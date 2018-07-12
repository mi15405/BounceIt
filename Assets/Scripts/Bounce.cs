using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
	[SerializeField]
	private float bounceForce;

	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			var rb = other.gameObject.GetComponent<Rigidbody>();
			if (rb == null)
				Debug.LogError(("Can't find Rigidbody on Player"));

			var bounceDirection = -other.contacts[0].normal;
			//var bounceDirection = transform.forward;
			
			rb.AddForce(bounceDirection * bounceForce, ForceMode.Impulse);
		}
	}
}
