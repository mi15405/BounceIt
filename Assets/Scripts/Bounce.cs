using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
	[SerializeField]
	private float bounceForce;

	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.CompareTag("Ball"))
		{
			var bounceDirection = -other.contacts[0].normal;
			other.rigidbody.AddForce(bounceDirection * bounceForce, ForceMode.Impulse);
		}
	}

}
