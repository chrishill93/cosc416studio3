using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float rotationSpeed = 100f;

    private float currentRotationX = 0f;
    private float currentRotationY = 0f;

    private void LateUpdate()
    {
        if (!target) return;

        currentRotationX += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        currentRotationY += Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
        currentRotationY += Mathf.Clamp(currentRotationY, -40f, 60f);

        Quaternion rotation = Quaternion.Euler(currentRotationY, currentRotationX, 0f);
        transform.position = target.position - (rotation * offset);
        transform.LookAt(target.position);
    }
}