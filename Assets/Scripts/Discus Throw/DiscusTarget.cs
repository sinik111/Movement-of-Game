using UnityEngine;

public class DiscusTarget : MonoBehaviour
{
    [SerializeField]
    private BoundaryRing boundaryRing;
    [SerializeField]
    private float exceptionWidth;

    private float targetRadius;

    private float screenHeight;
    private float screenWidth;

    private Vector3 ringPosition;
    private float ringRadius;

    public float Radius { get { return targetRadius; } }

    private void Start()
    {
        targetRadius = transform.GetChild(0).localScale.x * 0.5f;

        ringPosition = boundaryRing.transform.position;
        ringRadius = boundaryRing.Radius;

        screenHeight = Camera.main.orthographicSize;
        screenWidth = screenHeight * Camera.main.aspect;

        SetNewPosition();
    }

    public void SetNewPosition()
    {
        Vector3 newPosition;

        while (true)
        {
            float x = Random.Range(-screenWidth + targetRadius, screenWidth - targetRadius);
            float y = Random.Range(-screenHeight + targetRadius, screenHeight - targetRadius);

            newPosition = new Vector3(x, y, 0.0f);

            if (ringRadius + exceptionWidth + targetRadius < (newPosition - ringPosition).magnitude)
            {
                break;
            }
        }

        transform.position = newPosition;
    }
}
