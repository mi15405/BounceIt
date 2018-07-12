using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour
{
    [SerializeField]
    private float timeToDestroy = 1f;

    private void Start()
    {
        Destroy(gameObject, timeToDestroy);
    }
}
