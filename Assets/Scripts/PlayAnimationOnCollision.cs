using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimationOnCollision : MonoBehaviour {

    [SerializeField]
    private ParticleSystem animationPrefab;
    private ParticleSystem animationOnCollision;

    private void Awake()
    {
        SetAnimation();
    }

    private void SetAnimation()
    {
        animationOnCollision = Instantiate(animationPrefab, transform);
        animationOnCollision.transform.position = transform.position;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            if (animationOnCollision == null)
                SetAnimation();

            animationOnCollision.Play();   
        }
    }

}
