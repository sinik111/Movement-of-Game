using UnityEngine;

public class ThrowerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private BoundaryRing boundaryRing;

    private Vector3 ringPosition;
    private float ringRadius;
    private float throwerRadius;

    private void Start()
    {
        ringPosition = boundaryRing.transform.position;
        ringRadius = boundaryRing.Radius;
        throwerRadius = transform.GetChild(0).localScale.x * 0.5f;
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, vertical, 0.0f);

        if (direction.magnitude > 1.0f)
        {
            direction.Normalize();
        }

        Vector3 nextPosition = transform.position + direction* moveSpeed *Time.deltaTime;

        if (ringRadius - throwerRadius < (nextPosition - ringPosition).magnitude)
        {
            nextPosition = (nextPosition - ringPosition).normalized * (ringRadius - throwerRadius);
        }

        transform.position = nextPosition;
    }
}
