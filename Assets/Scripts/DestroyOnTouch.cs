using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTouch : MonoBehaviour {

    [SerializeField]
    private ParticleSystem explosionPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            if (explosionPrefab != null)
            {
                var explosion = Instantiate(explosionPrefab);
                explosion.Play();
                explosion.transform.position = transform.position;
            } 

            Destroy(gameObject);
        }
    }

}
