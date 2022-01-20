using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    #region Boilerplate
    public LineRenderer lr;

    public LayerMask whatIsGrappable;

    // Point to grapple to 
    public Vector3 grapplePoint;

    // Grapple origin
    public Transform grappleOrigin, player;

    public SpringJoint joint;
    #endregion

    #region Gameplay and spec
    // Max length of rope
    public float ropeLength;

    public bool grappled;

    public float minDistance;
    public float maxDistance;
    public float spring;
    public float damper;
    public float massScale;

    #endregion
    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            StartGrapple();
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            StopGrapple();
        }
    }

    private void LateUpdate()
    {
        DrawRope();
    }

    private void StartGrapple()
    {
        RaycastHit hit;

        if (Physics.Raycast(grappleOrigin.position, grappleOrigin.up, out hit, ropeLength, whatIsGrappable))
        {
            // Get where grapple hits
            grapplePoint = hit.point;

            // Create a new joint
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;

            // Get grappled rigid body
            joint.connectedBody = hit.collider.attachedRigidbody;

            joint.minDistance = minDistance;
            joint.maxDistance = maxDistance;

            lr.positionCount = 2;

            print(hit.collider.gameObject.name);
        }
    }

    private void DrawRope()
    {
        // If not grappling, don't draw line
        if (!joint) return;

        lr.SetPosition(0, grappleOrigin.position);
        lr.SetPosition(1, grapplePoint);
    }

    private void StopGrapple()
    {
        lr.positionCount = 0;
        Destroy(joint);
    }

    private void ChangePlayerGrapple(bool x)
    {
        FindObjectOfType<PlayerMovement>().isGrappling = x;
    }
}
