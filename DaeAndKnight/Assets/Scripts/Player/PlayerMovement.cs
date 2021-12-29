using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Fields

    #region References
    [Header("References")]
    public CharacterController controller;
    public Animator animator;
    #endregion

    #region Gameplay and spec
    [Header("Gameplay and spec")]

    // Move speed and gravity
    public float knightMoveSpeed;
    public float daeMoveSpeed;
    public float gravity = 9.81f;
    public float jumpSpeed = 3.5f;

    private float directionY;

    // Decide if player can move
    public bool canMove;

    // Decide if player is running
    public bool isRunning;
    #endregion

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        #region Movement
        // Get input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(0, verticalInput, horizontalInput);

        if (controller.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                directionY = jumpSpeed;
            }
        }

        directionY -= gravity * Time.deltaTime;
        direction.y = directionY;

        controller.Move(direction * knightMoveSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = new Quaternion(0, 180, 0, 1);
            isRunning = true;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = new Quaternion(0, 0, 0, 1);
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        animator.SetBool("IsRunning", isRunning);
        #endregion
    }
}
