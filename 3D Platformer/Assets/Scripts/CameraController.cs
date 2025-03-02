using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 4, -8);
    public float rotationSpeed = 500f;

    private float currentRotationX = 0f;
    private float currentRotationY = 0f;

    private void LateUpdate()
    {
        if (!target) return;

        currentRotationX += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        currentRotationY += Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
        currentRotationY = Mathf.Clamp(currentRotationY, -80f, -30f);

        Quaternion rotation = Quaternion.Euler(currentRotationY, currentRotationX, 0f);
        Vector3 desiredPosition = target.position - (rotation * offset);

        transform.position = desiredPosition;
        transform.LookAt(target.position);
    }
}