using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour {
    
	[SerializeField]
    private Transform player;

    // MOVEMENT
    [SerializeField] 
    private float moveSpeed;
    private float moveHorizontal;
    private float moveVertical;
    
    // Current position is calculated relative to player's position
    private Vector3 currentPosition;
    
    // DISTANCE
    [SerializeField]
    private float distance;
    
    [SerializeField]
    private float distanceMax;
    
    // ROTATION
    [SerializeField]
    private float localRotateSpeed;
    
    // Rotates transform towards player
    private bool globalRotationPressed;
    
    // Rotating arround local axis, instead of player
    private bool isLocalRotating;
    
    // Rotating arround local X axis
    private bool isRotatingLeft;
    private bool isRotatingRight;
    
    // Allign rotation with axis
    private bool alignHorizontalPressed;
    private bool alignVerticalPressed;
	
    private void Start()
    {
        // Starting position, below player
        currentPosition = Vector3.down * distance;
    }

    private void Update()
    {
	    GetInput();
        SetRotation();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void GetInput()
    {
        // Horizontal and Vertical input
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");
        
        // Local rotation input
        isRotatingRight = Input.GetButton("LocalRotRight");
        isRotatingLeft = Input.GetButton("LocalRotLeft");
        
        // Allign rotation
        alignHorizontalPressed = Input.GetButton("AlignHorizontal");
        alignVerticalPressed = Input.GetButton("AlignVertical");
        
        // Platform is rotating localy if it already was rotating localy or has just started
        isLocalRotating |=  isRotatingLeft || isRotatingRight;
        
        // Turns local rotation off, and global (arround player) on
        globalRotationPressed = Input.GetButtonDown("Fire1");
    }

    void Move()
    {
        var moveDirection = new Vector3(0f, moveVertical, moveHorizontal);
        var targetPosition = currentPosition + moveDirection * moveSpeed;

        currentPosition = Vector3.MoveTowards(currentPosition, targetPosition, moveSpeed * Time.deltaTime);
        
        // Limits platform position
        currentPosition = Vector3.ClampMagnitude(currentPosition, distanceMax);
       
        // Position calculated relative to player
        transform.position = player.position + currentPosition;
   }

    void SetRotation()
    {
        if (globalRotationPressed)
            isLocalRotating = false;
        
        SetLocalRotation();

        if (alignHorizontalPressed)
            transform.rotation = Quaternion.Euler(-90f, 0f, -90f); // Horizontal 
        else if (alignVerticalPressed)
            transform.rotation = Quaternion.Euler(0f, 0f, -90f); // Vertical
        else if (!isLocalRotating)
            transform.rotation = Quaternion.LookRotation(player.position - transform.position, Vector3.right);

    }

    void SetLocalRotation()
    {
        if (isRotatingRight)
        {
            transform.Rotate(Vector3.up * localRotateSpeed * Time.deltaTime);
        }
        else if (isRotatingLeft)
        {
            transform.Rotate(-Vector3.up * localRotateSpeed * Time.deltaTime);
        }
    }

}
