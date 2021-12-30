using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Fields

    #region References
    [Header("References")]
    public GameManager gameManager;
    public CharacterController controller;
    public Animator animator;
    #endregion

    #region Gameplay and spec
    [Header("Gameplay and spec")]

    // Move speed and gravity
    public float knightMoveSpeed;
    public float gravity = 9.81f;
    public float jumpSpeed = 3.5f;
    public float doubleJumpMultiplier = 0.5f;

    private float directionY;

    // Decide if player can move
    public bool canMove;

    // Decide if player is running
    public bool knightIsRunning;

    // Decide if player can double jump
    public bool canDoubleJump;
    #endregion

    #endregion

    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        // Only move knight if time of day is night
        if (gameManager.timeOfDay == GameManager.TimeOfDay.Night)
        {
            #region Movement
            // Get input
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 direction = new Vector3(0, verticalInput, horizontalInput);

            if (controller.isGrounded)
            {
                canDoubleJump = true;

                if (Input.GetButtonDown("Jump"))
                {
                    directionY = jumpSpeed;
                }
            }
            else
            {
                if (Input.GetButtonDown("Jump") && canDoubleJump)
                {
                    directionY = jumpSpeed * doubleJumpMultiplier;
                    canDoubleJump = false;
                }
            }

            directionY -= gravity * Time.deltaTime;
            direction.y = directionY;

            controller.Move(direction * knightMoveSpeed * Time.deltaTime);

            if (Input.GetKey(KeyCode.A))
            {
                transform.rotation = new Quaternion(0, 180, 0, 1);
                knightIsRunning = true;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.rotation = new Quaternion(0, 0, 0, 1);
                knightIsRunning = true;
            }
            else
            {
                knightIsRunning = false;
            }

            animator.SetBool("KnightIsRunning", knightIsRunning);
            #endregion
        }
    }
}
