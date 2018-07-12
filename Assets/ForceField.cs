using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour
{

    [SerializeField]
    private float force;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            var rb = other.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * force);
        }
    }

}
