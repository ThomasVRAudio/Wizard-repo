using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private float smoothRotation = 0.125f;

    private void LateUpdate()
    {
        Vector3 targetPosition = target.position;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        Quaternion targetRotation = target.rotation;
        Quaternion smoothedRotation = Quaternion.Lerp(transform.rotation, targetRotation, smoothRotation * Time.deltaTime);
        transform.rotation = smoothedRotation;
    }
}
