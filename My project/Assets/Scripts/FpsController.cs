using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FpsController : MonoBehaviour
{
    CharacterController characterController;
    public Camera playerCamera;
    public float walkSpeed = 4f;
    public float runSpeed = 6f;
    public float jumpPower = 3f;
    public float gravity = -9.8f;

    public float mouseTurnSpeed = 2f;
    public float lookLimit = 2f;

    Vector3 movDir = Vector3.zero;
    float rotationX = 2f;

    public bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        #region Player move
        //Vectors
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        //Check if player run/sprint
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        
        //Mouse Speed
        float mouseSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float mouseSpeedy = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        
        //Move direction
        float moveDirY = movDir.y;
        movDir = (forward * mouseSpeedX) + (right * mouseSpeedy);
        #endregion

        #region Payer jump
        //Checking if Space bar is pushed, allowed to move and checking if player is grounded
        if(Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            //Set the vertical component of the movement direction vector to the jump power
            movDir.y = jumpPower;
        }
        else
        {
            //Set the vertical component of the movement direction vector to its original value
            //This ensures that the character maintains any vertical movement not related to jumping
            movDir.y = moveDirY;
        }
        //Check if the character controller is not grounded (i.e., in the air)
        if (!characterController.isGrounded)
        {
            //Apply gravity to the vertical component of the movement direction vector
            //Gravity is subtracted from the vertical velocity to simulate falling
            movDir.y -= gravity * Time.deltaTime;
        }

        #endregion

        #region rotation
        //Move the character controller based on the movement direction vector multiplied by deltaTime
        characterController.Move(movDir * Time.deltaTime);

        if(canMove)
        {
            //Update rotationX based on vertical mouse movement
            rotationX += -Input.GetAxis("Mouse Y") * mouseTurnSpeed;
            //Clamp rotationX within limits to restrict vertical camera rotation
            rotationX = Mathf.Clamp(rotationX, -lookLimit, lookLimit);
            //Apply the vertical rotation to the player's camera
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            //Rotate the player horizontally based on horizontal mouse movement
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * mouseTurnSpeed, 0);
        }
        #endregion
    }
}
