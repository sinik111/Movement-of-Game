using UnityEngine;

public class WayPointMovement : MonoBehaviour
{
    [SerializeField]
    private Transform[] points;
    [SerializeField]
    private float duration;
    [SerializeField]
    private float speed;

    private float elapsed = 0f;
    private int index = 0;

    private PlatformCollider platform;

    private void Start()
    {
        platform = GetComponent<PlatformCollider>();
    }

    private void Update()
    {
        Vector3 position1 = points[index].position;
        Vector3 position2 = points[(index + 1) % points.Length].position;

        float distance = (position1 - position2).magnitude;
        duration = distance / speed;

        float t = elapsed / duration;

        Vector3 nextPosition = NonLinearInterpolate(position1, position2, t);

        platform.DeltaMovement = nextPosition - transform.position;

        transform.position = nextPosition;

        elapsed += Time.deltaTime;
        if (t >= 1f)
        {
            index = (index + 1) % points.Length;
            elapsed = 0;
        }
    }

    private Vector3 NonLinearInterpolate(Vector3 v0, Vector3 v1, float t)
    {
        t = Mathf.Clamp01(t);

        t = -2f * Mathf.Pow(t, 3) + 3f * Mathf.Pow(t, 2);

        return v0 * (1 - t) + v1 * t;
    }
}
