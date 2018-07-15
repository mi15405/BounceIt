using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour {
    
	[SerializeField]
    private Transform player;

    // MOVEMENT
    [SerializeField]
    private bool canMove;

    [SerializeField] 
    private float moveSpeed;
    private float moveHorizontal;
    private float moveVertical;
    private Vector3 moveDirection = Vector3.zero;
    
    // Current position is calculated relative to player's position
    private Vector3 currentPosition;
    
    // DISTANCE
    [SerializeField]
    private float startingDistance;
    
    [SerializeField]
    private float distanceMax;
    
    // ROTATION
    [SerializeField]
    private float localRotateSpeed;
    
    // Rotating arround local X axis
    private bool isRotatingLeft;
    private bool isRotatingRight;
    
    // Allign rotation with axis
    private bool alignHorizontalPressed;
    private bool alignVerticalPressed;
	
    private void Start()
    {
        ResetPosition();
    }

    public void ResetPosition()
    {
        currentPosition = Vector3.down * startingDistance;
        AllignHorizontal();
    }

    private void Update()
    {
	    GetInput();
        SetRotation();
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
    }

    void Move()
    {
        if (canMove)
        {
            moveDirection.y = moveVertical;
            moveDirection.z = moveHorizontal;
            
            var targetPosition = currentPosition + moveDirection * moveSpeed;

            currentPosition = Vector3.MoveTowards(currentPosition, targetPosition, moveSpeed * Time.deltaTime);
            
            // Limits platform position
            currentPosition = Vector3.ClampMagnitude(currentPosition, distanceMax);
        }
       
        // Position calculated relative to player
        transform.position = player.position + currentPosition;
   }

    void SetRotation()
    {
        SetLocalRotation();

        if (alignHorizontalPressed)
            AllignHorizontal();
        else if (alignVerticalPressed)
            AllignVertical();
    }

    private void AllignHorizontal()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, 0f); // Horizontal 
    }

    private void AllignVertical()
    {
        transform.rotation = Quaternion.Euler(90f, 0f, 0f); // Vertical
    }

    void SetLocalRotation()
    {
        if (isRotatingRight)
        {
            transform.Rotate(Vector3.right * localRotateSpeed * Time.deltaTime);
        }
        else if (isRotatingLeft)
        {
            transform.Rotate(-Vector3.right * localRotateSpeed * Time.deltaTime);
        }
    }

}
