using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaeMovement : MonoBehaviour
{
    #region Fields

    #region References
    [Header("References")]
    public GameManager gameManager;
    public CharacterController controller;
    #endregion

    #region Gameplay and spec
    [Header("Gameplay and spec")]

    // Move speed and gravity
    public float daeMoveSpeed;
    public float gravity = 9.81f;
    public float jumpSpeed = 3.5f;
    public float doubleJumpMultiplier = 0.5f;

    private float directionY;

    // Decide if player can move
    public bool canMove;

    // Decide if player is running
    public bool daeIsRunning;

    // Decide if player can double jump
    public bool hasDoubleJump;
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
        // Only move Dae if time of day is day
        if (gameManager.timeOfDay == GameManager.TimeOfDay.Day)
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
                if (Input.GetButtonDown("Jump") && canDoubleJump && hasDoubleJump)
                {
                    directionY = jumpSpeed * doubleJumpMultiplier;
                    canDoubleJump = false;
                }
            }

            directionY -= gravity * Time.deltaTime;
            direction.y = directionY;

            controller.Move(direction * daeMoveSpeed * Time.deltaTime);

            if (Input.GetKey(KeyCode.A))
            {
                transform.rotation = new Quaternion(0, 180, 0, 1);
                daeIsRunning = true;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.rotation = new Quaternion(0, 0, 0, 1);
                daeIsRunning = true;
            }
            else
            {
                daeIsRunning = false;
            }
            #endregion
        }
    }
}
