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
    public Rigidbody rb;
    public ParticleSystem ps;
    public AudioSource runningAudio;
    #endregion

    #region Gameplay and spec
    [Header("Gameplay and spec")]

    // Move speed and gravity
    public float knightMoveSpeed;
    public float gravity = 9.81f;
    public float jumpForce;
    public float doubleJumpMultiplier;
    public float dashForce;

    private float directionY;

    // Decide if player can move
    public bool canMove;

    // Decide if player is running
    public bool knightIsRunning;

    // Decide if player can double jump
    public bool hasDoubleJump;
    public bool canDoubleJump;

    // Decide if player can dash
    public bool hasDash;
    public bool canDash;
    public bool hasVerticalDash;
    public bool canVerticalDash;
    public bool isGrappling;

    public bool isPlaying;
    #endregion

    #endregion

    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        #region Movement and Jump
        // Get input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(0, verticalInput, horizontalInput);

        #region Jump
        if (controller.isGrounded)
        {
            canDoubleJump = true;

            directionY = 0;

            if (Input.GetButtonDown("Jump"))
            {
                directionY = jumpForce;
                FindObjectOfType<AudioManager>().Play("KnightJump");
            }
        }
        else
        {
            if (Input.GetButtonDown("Jump") && canDoubleJump && hasDoubleJump)
            {
                directionY = jumpForce * doubleJumpMultiplier;
                FindObjectOfType<AudioManager>().Play("KnightDoubleJump");
                canDoubleJump = false;
            }
        }

        print(controller.isGrounded);

        directionY -= gravity * Time.deltaTime;
        direction.y = directionY;
        #endregion

        if (canMove)
        {
            controller.Move(direction * knightMoveSpeed * Time.deltaTime);
        }

        #region Misc
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
        #endregion

        #region Running audio
        if (knightIsRunning)
        {
            if (!runningAudio.isPlaying && controller.isGrounded)
            {
                runningAudio.Play();
            }
        }
        else
        {
            runningAudio.Stop();
        }
        #endregion

        #region Dash
        if (hasDash)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
            {
                Vector3 dash = new Vector3(0, 0, 0);

                if (direction.z > 0)
                {
                    dash = new Vector3(0, 0, dashForce);
                }
                else if (direction.z < 0)
                {
                    dash = new Vector3(0, 0, -dashForce);
                }
                
                controller.Move(dash);
                FindObjectOfType<AudioManager>().Play("Dash");
                canDash = false;
                ps.Play();
                StartCoroutine(WaitToStopParticleSystem());
                StartCoroutine(WaitToResetDash());
            }
        }

        if (Input.GetKeyDown(KeyCode.W) && hasVerticalDash && canVerticalDash && !controller.isGrounded)
        {
            // Vertical dash
            Vector3 dash = new Vector3(0, 0, 0);

            dash = new Vector3(0, dashForce, 0);

            controller.Move(dash);
            FindObjectOfType<AudioManager>().Play("Dash");
            canDash = false;
            ps.Play();
            StartCoroutine(WaitToStopParticleSystem());
            StartCoroutine(WaitToResetDash());
        }
        #endregion
        animator.SetBool("KnightIsRunning", knightIsRunning);
        #endregion

        // Only move knight if time of day is night
        if (gameManager.timeOfDay == GameManager.TimeOfDay.Night)
        {

        }
    }

    IEnumerator WaitToResetDash()
    {
        yield return new WaitForSeconds(2.5f);
        canDash = true;
    }

    IEnumerator WaitToStopParticleSystem()
    {
        yield return new WaitForSeconds(0.5f);
        ps.Stop();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "platform")
        {
            FindObjectOfType<AudioManager>().Play("WoodPlatformLanding");
        }
    }
}
