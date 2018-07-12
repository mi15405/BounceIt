using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Cinemachine;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.Timeline;

public class PlatformSpawner : MonoBehaviour {
	
    [SerializeField] 
    private GameObject[] platformsToSpawn;
    
    private int currentPlatform = 0;
    private bool spawnPressed = false;
    private bool nextPlatformPressed = false;
    private bool previousPlatformPressed = false;

    [SerializeField]
    private LayerMask blockingLayers;

    [SerializeField]
    private Color unspawnableColor;

    private bool isSpawnable = true;
    public bool IsSpawnable
    {
        get { return isSpawnable;}
        set
        {
            isSpawnable = value;

            if (!isSpawnable)
                material.color = unspawnableColor;
            else
                UpdateColor();
                
        }
    }

    private Material material;

    private void Awake()
    {
        material = GetComponentInChildren<Renderer>().material;
    }

    private void Start()
    {
        UpdateColor();
    }

    private void Update()
    {
	    GetInput();
        SpawnPlatform();
    }

    private void GetInput()
    {
        spawnPressed = Input.GetButtonDown("Jump");
        nextPlatformPressed = Input.GetButtonDown("NextPlatform");
        previousPlatformPressed = Input.GetButtonDown("PreviousPlatform");
    }

    private void SpawnPlatform()
    {
        if (nextPlatformPressed)
            NextPlatform();
        else if (previousPlatformPressed)
            PreviousPlatform();
        
        if (spawnPressed && isSpawnable)
        {
            var platform = Instantiate(platformsToSpawn[currentPlatform]);
            platform.transform.position = transform.position;
            platform.transform.rotation = transform.rotation;
        }
    }

    private void NextPlatform()
    {
        currentPlatform++;
        if (currentPlatform == platformsToSpawn.Length)
            currentPlatform = 0;

        UpdateColor();
    }

    private void PreviousPlatform()
    {
        currentPlatform--;
        if (currentPlatform < 0)
            currentPlatform = platformsToSpawn.Length - 1;

        UpdateColor();
    }


    private void UpdateColor()
    {
        material.color = platformsToSpawn[currentPlatform].GetComponentInChildren<Renderer>().sharedMaterial.color;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (IsLayerInMask(other.gameObject.layer, blockingLayers))
        {
            IsSpawnable = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsLayerInMask(other.gameObject.layer, blockingLayers))
        {
            IsSpawnable = true;
        }
    }

    private bool IsLayerInMask(int layer, LayerMask mask)
    {
        return mask == (mask | (1 << layer));
    }

}
