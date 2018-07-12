using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    [SerializeField]
    private ParticleSystem collectAnimation;

    [SerializeField]
    private float animationTime = 1f;

    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            var ball = other.gameObject.GetComponent<Ball>();
            ball.StartingPosition = transform;

            if (collectAnimation != null)
            {
                var animation = Instantiate(collectAnimation, transform);
                Destroy(animation, animationTime);
            }
        }
    }


	
}
