using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 direction;
    public float forwardSpeed;
    private int desiredLane = 1; // 0 left | 1 middle | 2 right
    public float laneDistance = 1.5f; // distance between the lanes
    public float jumpForce;
    public float gravity = -30;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();    
    }

    // Update is called once per frame
    void Update()
    {

        float accelerationX = Input.acceleration.x;

        if (!PlayerManager.isGameStarted) return;

        if (PlayerManager.haveWin && Input.touchCount > 0)
        {
            transform.position = transform.position;
            return;
        }

        direction.z = forwardSpeed;

        //Gather the inputs 
        if (characterController.isGrounded)
        {
            direction.y = -1;
            if (SwipeManager.swipeUp)
            {
              //  Jump();
            }
        }
        else
        {
           // direction.y += gravity * Time.fixedDeltaTime;
        }

        if ( accelerationX > 0.5f)
        {
            desiredLane++;
            if (desiredLane == 3)
                desiredLane = 2;
        }
        else if ( accelerationX < -0.5f)
        {
            desiredLane--;
            if (desiredLane == -1)
                desiredLane = 0;
        }
        else {
            desiredLane = 1;
        }

        // Calculate where we should be in the guture

        Vector3 targetPosition  = transform.position.z * transform.forward + 0 * transform.up ;

        if(desiredLane == 0)
        {
            targetPosition += Vector3.left/2 * laneDistance  ;
        }else if(desiredLane == 2)
        {
            targetPosition += Vector3.right/2 * laneDistance;
        }

        //transform.position = Vector3.Lerp( transform.position, targetPosition, 60 * Time.fixedDeltaTime);
        
        if (transform.position == targetPosition) return;
        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.fixedDeltaTime;
        if(moveDir.sqrMagnitude < diff.sqrMagnitude) characterController.Move(moveDir);
        else characterController.Move(diff);
       
        
    }

    private void FixedUpdate()
    {
        if (!PlayerManager.isGameStarted) return;
        characterController.Move(direction * Time.fixedDeltaTime);
    }

    private void Jump()
    {
        direction.y = jumpForce;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
     if(hit.transform.tag == "Obstacle")
        {
            PlayerManager.gameOver = true;
        }   
    }
}
