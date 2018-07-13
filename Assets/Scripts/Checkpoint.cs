using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    [SerializeField]
    private ParticleSystem collectAnimationPrefab;
    private ParticleSystem collectAnimation;

    private void SetAnimation()
    {
        collectAnimation = Instantiate(collectAnimationPrefab, transform);
        collectAnimation.transform.position = transform.position;
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            var ball = other.gameObject.GetComponent<Ball>();
            ball.StartingPosition = transform;

            if (collectAnimation == null)
                SetAnimation();

            collectAnimation.Play();
        }
    }


	
}
