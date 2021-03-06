﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOnCollision : MonoBehaviour
{
    [SerializeField]
    private bool turnedOn = true;
    public bool TurnedOn
    {
        get { return turnedOn; }
        set
        {
            turnedOn = value;
            UpdateColor();
        }
    }

    [SerializeField]
    private bool switchEnabled = false;

    [SerializeField]
    private float switchInterval;
    private float timeToSwitch;

    [SerializeField]
    private Color onColor;
    
    [SerializeField]
    private Color offColor;

    private Material material;

    private void Awake()
    {
        material = GetComponentInChildren<Renderer>().material;
    }

    private void Start()
    {
        if (switchEnabled)
            timeToSwitch = Random.Range(0, switchInterval);
    }

    private void Update()
    {
        if (!switchEnabled)
            return;
        
        timeToSwitch -= Time.deltaTime;

        if (timeToSwitch < 0f)
        {
            // Assign with property, so color can change too
            TurnedOn = !TurnedOn;
            timeToSwitch = switchInterval;
        }   
    }
    
    private void OnCollisionEnter(Collision other)
    {
        KillBall(other.collider);
    }

    private void OnTriggerEnter(Collider other)
    {
        KillBall(other);
    }

    private void KillBall(Collider other)
    {
        if (!turnedOn)
            return;
        
        if (other.gameObject.CompareTag("Ball"))
        {
            var ball = other.gameObject.GetComponent<Ball>();
            
            if (ball != null)
                ball.MoveToStart();
            else
                Debug.LogError("Can't find Ball component in KillOnCollision!");
        }
    }

    private void UpdateColor()
    {
        material.color = turnedOn ? onColor : offColor;
    }

}
