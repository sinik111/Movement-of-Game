using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class LinearMovement : MonoBehaviour
{
    [SerializeField]
    private PlatformCollider target;
    [SerializeField]
    private Vector3 startPosition;
    [SerializeField]
    private Vector3 endPosition;
    [SerializeField]
    private float duration;
    
    private float elapsed;

    private bool reversed;

    private void Start()
    {
        target.transform.position = startPosition;
        elapsed = 0.0f;
        reversed = false;
    }

    private void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed > duration)
        {
            elapsed = 0.0f;
            reversed = !reversed;
        }

        Vector3 nextPosition = LinearInterpolation(startPosition, endPosition, reversed ? 1.0f - elapsed / duration : elapsed / duration);

        target.DeltaMovement = nextPosition - target.transform.position;

        target.transform.position = nextPosition;
    }

    public Vector3 LinearInterpolation(Vector3 v0, Vector3 v1, float t)
    {
        t = Mathf.Clamp01(t);

        return v0 * (1 - t) + v1 * t;
    }
}
