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
    private float chargeThreshold;    // charge needed for second platorm 

    [SerializeField]
    private float chargeThresholdIncrease;   // charge increase for other platforms
	
    [SerializeField] 
    private GameObject[] platformsToSpawn;
    private int currentPlatform = 0;

    private float bouncePower;
    private bool isCharging = false;

    private bool chargeReleased = false;
    //private bool nextPlatformPressed = false;
    //private bool previousPlatformPressed = false;

    [SerializeField]
    private LayerMask blockingLayers;

    [SerializeField]
    private Color unspawnableColor;
    [SerializeField]
    private Color spawnableColor;

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
                material.color = spawnableColor;
        }
    }

    private Material material;

    private void Awake()
    {
        material = GetComponentInChildren<Renderer>().material;
    }

    private void Update()
    {
	    GetInput();
        SpawnPlatform();
    }

    private void GetInput()
    {
        isCharging = Input.GetButton("Jump");
        chargeReleased = Input.GetButtonUp("Jump");

        /*
        nextPlatformPressed = Input.GetButtonDown("NextPlatform");
        previousPlatformPressed = Input.GetButtonDown("PreviousPlatform");
        */
    }

    private void SpawnPlatform()
    {
        /*
        if (nextPlatformPressed)
            NextPlatform();
        else if (previousPlatformPressed)
            PreviousPlatform();
        */

        if (isCharging)
            bouncePower += Time.deltaTime;
        
        if (chargeReleased )
        {
            if (isSpawnable)
            {
                var platform = Instantiate(SelectPlatformToSpawn());

                platform.transform.position = transform.position;
                platform.transform.rotation = transform.rotation;
            }

            bouncePower = 0f;
        }
    }

    private GameObject SelectPlatformToSpawn()
    {
        var threshold = chargeThreshold;

        foreach (var platform in platformsToSpawn)
        {
            if (bouncePower < threshold)
                return platform;
            else
                threshold += chargeThresholdIncrease;
        }

        // Returns last platofrm, if charged for too long
        return platformsToSpawn[platformsToSpawn.Length - 1];
    }

    private void NextPlatform()
    {
        currentPlatform++;
        if (currentPlatform == platformsToSpawn.Length)
            currentPlatform = 0;

        //UpdateColor();
    }

    private void PreviousPlatform()
    {
        currentPlatform--;
        if (currentPlatform < 0)
            currentPlatform = platformsToSpawn.Length - 1;

        //UpdateColor();
    }


    private void UpdateColor()
    {
        material.color = platformsToSpawn[currentPlatform].GetComponentInChildren<Renderer>().sharedMaterial.color;
    }
    
    private void OnTriggerStay(Collider other)
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
