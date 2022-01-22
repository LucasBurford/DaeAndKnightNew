using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    public Camera cam;
    public LineRenderer lr;
    public LayerMask grappleLayer;
    public Transform rayOrigin;
    public Material lrMaterial;
    public Gradient lrGradient;

    public Color lrCol1;
    public Color lrCol2;

    Vector3 clickedPoint;
    Vector3 startPos;

    public float distanceToWorld;
    public float maxRayDistance;
    public float lerpSpeed;
    public float stoppingDistance;
    public float removeLineTime;
    public float lrStartWidth;
    public float lrEndWidth;

    public bool hasGrapple;
    public bool shouldLerp;

    private void Update()
    {
        // Store clicked positon in world
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = distanceToWorld;

        // Check if player clicks mouse, store clicked position in clickedPoint
        if (Input.GetButtonDown("Fire1") && hasGrapple)
        {
            clickedPoint = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceToWorld));

            // Cast a line between player and clicked point and check if it collides with any grapple objects
            if (Physics.Linecast(rayOrigin.position, clickedPoint, grappleLayer))
            {
                lr = gameObject.AddComponent<LineRenderer>();
                lr.startWidth = lrStartWidth;
                lr.endWidth = lrEndWidth;
                lr.material = lrMaterial;

                float alpha = 1f;

                lr.startColor = lrCol1;
                lr.endColor = lrCol2;

                lrGradient.SetKeys(
                    new GradientColorKey[] { new GradientColorKey(lrCol1, 1f), new GradientColorKey(lrCol2, 1f) },
                    new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1f)}
                );
                                    

                Vector3[] lrVectors = new Vector3[]
                {   
                    rayOrigin.position,
                    clickedPoint
                };

                lr.SetPositions(lrVectors);

                // Teleport player to clicked point but keep x the same
                transform.position = new Vector3(transform.position.x, clickedPoint.y, clickedPoint.z);

                FindObjectOfType<AudioManager>().Play("Portal");

                StartCoroutine(WaitToRemoveLine());

                //startPos = transform.position;
                //shouldLerp = true;
            }
        }

        if (shouldLerp)
        {
            transform.position = Vector3.Lerp(startPos, new Vector3(transform.position.x, clickedPoint.y, clickedPoint.z), lerpSpeed);

            if (Vector3.Distance(transform.position, clickedPoint) < stoppingDistance)
            {
                shouldLerp = false;
            }

            print(Vector3.Distance(transform.position, clickedPoint));
        }
    }

    IEnumerator WaitToRemoveLine()
    {
        yield return new WaitForSeconds(removeLineTime);
        Destroy(lr);
    }
}
