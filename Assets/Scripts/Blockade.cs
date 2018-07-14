using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blockade : MonoBehaviour {

    [SerializeField]
    private float breakPoint;

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            if (other.gameObject.GetComponent<Rigidbody>().velocity.magnitude > breakPoint)
            {
                Destroy(gameObject);
            }
        }
    }
}
