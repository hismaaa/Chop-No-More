using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private Vector3 offset;
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime;
    private Vector3 currentVelocity = Vector3.zero;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        offset = transform.position - target.position;
    }

    // LateUpdate is called once per frame after Update
    private void LateUpdate()
    {
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
    }
}
