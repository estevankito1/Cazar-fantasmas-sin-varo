using UnityEngine;

public class IsometricCamera : MonoBehaviour
{
    public Transform target;
    public float distance = 20f;
    public float smoothTime = 0.15f;
    public Vector3 angles = new Vector3(35.264f, 45f, 0f); // ángulo isométrico clásico

    private Vector3 m_Velocity;

    void Awake()
    {
        transform.rotation = Quaternion.Euler(angles);
    }

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desired = target.position - transform.forward * distance;
        transform.position = Vector3.SmoothDamp(transform.position, desired,
            ref m_Velocity, smoothTime);
    }
}
