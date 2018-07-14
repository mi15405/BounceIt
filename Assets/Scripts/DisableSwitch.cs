using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableSwitch : MonoBehaviour {

    [SerializeField]
    private GameObject target;

    private bool turnedOn = false;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            if (target != null)
            {
                target.SetActive(turnedOn);
                turnedOn = !turnedOn;
            }
        }
    }
	
}
