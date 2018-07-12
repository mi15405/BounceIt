using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour {
    
    private CinemachineVirtualCamera cam;
    private CinemachineTransposer trans;

    [SerializeField]
    private float offsetMin;
    
    [SerializeField]
    private float offsetMax;

    private void Awake()
    {
        cam = GetComponent<CinemachineVirtualCamera>();

        if (cam != null)
        {
            trans = cam.GetCinemachineComponent<CinemachineTransposer>();
            
            if (trans == null)
                Debug.LogError("Can't find CinemachineTransposer in CameraController!");
        }
        else 
            Debug.LogError("Can't find CinemachineVirtualCamera on Camera Controller!");

    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.O))
        {
            if (trans.m_FollowOffset.x < offsetMax)
                trans.m_FollowOffset.x++;
        }
        else if (Input.GetKey(KeyCode.I))
        {
            if (trans.m_FollowOffset.x > offsetMin)
                trans.m_FollowOffset.x--;
        }
    }

}
