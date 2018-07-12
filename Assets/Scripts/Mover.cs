using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private Transform[] destinations;
    private int currentDestination = 0;
    

    private void Update()
    {
        Move();
        UpdateDestination();
    }

    private void Move()
    {
        var target = (CurrentDestination() - transform.position).normalized * speed;
        transform.Translate(target * Time.deltaTime);
    }

    private void UpdateDestination()
    {
        float closeEnough = 0.1f;
        
        if (Vector3.Distance(transform.position, CurrentDestination()) <= closeEnough)
            NextDestination();
    }

    private Vector3 CurrentDestination()
    {
        return destinations[currentDestination].position;
    }

    private void NextDestination()
    {
        currentDestination++;

        if (currentDestination == destinations.Length)
            currentDestination = 0;
    }
    
}
