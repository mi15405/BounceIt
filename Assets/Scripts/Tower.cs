using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Tower : MonoBehaviour {

    [SerializeField]
    private float range;

    [SerializeField] 
    private float fireForce;

    [SerializeField] private float reloadTime;
    private float timeToFire = 0f;
    
    [SerializeField]
    private Transform fireTransform;

    [SerializeField] 
    private GameObject projectilePrefab;

    [SerializeField] 
    private LayerMask targetLayer;
    
    [SerializeField] 
    private LayerMask blockingLayers;

    private Transform target;

    private void Update()
    {
        FindTarget();
        Shoot();
    }

    private void FindTarget()
    {
        var targetsInRange = Physics.OverlapSphere(fireTransform.position, range, targetLayer);

        if (targetsInRange.Length > 0)
            target = targetsInRange[0].gameObject.transform;
        else 
            target = null;
    }

    private void Shoot()
    {
        // Reload
        if (timeToFire > 0f)
            timeToFire -= Time.deltaTime;
        
        // Check target and if is ready to fire
        if (target == null || timeToFire > 0f)
            return;

        var fireDirection = (target.position - fireTransform.position).normalized;
        
        // Shoot only if target is in sight
        RaycastHit hit;
        
        if (Physics.Raycast(transform.position, fireDirection, out hit, range, targetLayer | blockingLayers))
        {
            // Return if blocking layer vas hit
            if (blockingLayers == (blockingLayers | (1 << hit.collider.gameObject.layer)))
                return;
        }

        var projectile = Instantiate(projectilePrefab);
        projectile.transform.position = fireTransform.position;

        var rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
            rb.AddForce(fireDirection * fireForce, ForceMode.Impulse); 
        else
            Debug.LogError("Projectile that Tower is shooting doesn't have a rigidbody!");

        timeToFire = reloadTime;
    }

}
