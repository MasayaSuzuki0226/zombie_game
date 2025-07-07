using UnityEngine;

public class FixedCamera : MonoBehaviour
{
    public Vector3 targetPosition;
    public Vector3 targetRotation;

    void Update()
    {
        transform.position = targetPosition;
        transform.rotation = Quaternion.Euler(targetRotation);
    }
}

