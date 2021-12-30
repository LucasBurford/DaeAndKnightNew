using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameManager gameManager;

    public Transform knight;
    public Transform dae;

    public float smoothSpeed = 0.125f;

    public Vector3 offset;

    private void LateUpdate()
    {
        Vector3 desiredPosition;

        desiredPosition = knight.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        //if (gameManager.timeOfDay == GameManager.TimeOfDay.Day) 
        //{
        //    desiredPosition = dae.position + offset;
        //    Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        //    transform.position = smoothedPosition;
        //}
        if (gameManager.timeOfDay == GameManager.TimeOfDay.Night)
        {

        }
    }
}
