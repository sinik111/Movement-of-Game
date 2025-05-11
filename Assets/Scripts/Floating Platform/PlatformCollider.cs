using System.IO.Pipes;
using UnityEngine;

public class PlatformCollider : MonoBehaviour
{
    private LineRenderer lineRenderer;

    private Vector3 leftOffset;
    private Vector3 rightOffset;

    private Vector3 startPoint;
    private Vector3 endPoint;

    private Vector3 deltaMovement;

    public Vector3 DeltaMovement { get => deltaMovement; set => deltaMovement = value; }

    private void Start()
    {
        leftOffset = new Vector3(-transform.localScale.x / 2, 0.5f, 0.0f);
        rightOffset = new Vector3(transform.localScale.x / 2, 0.5f, 0.0f);

        deltaMovement = Vector3.zero;

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.material.color = Color.white;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.loop = false;
        lineRenderer.useWorldSpace = true;
        lineRenderer.positionCount = 2;
    }

    private void Update()
    {
        startPoint = transform.position + leftOffset;
        endPoint = transform.position + rightOffset;

        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, endPoint);
    }

    public bool IsCollide(Vector3 nextPosition, Vector3 prevPosition)
    {
        float leftDist;
        float rightDist;

        bool isLeftOut = IsPointOut(nextPosition + Vector3.left * 0.25f, out leftDist);
        bool isRightOut = IsPointOut(nextPosition + Vector3.right * 0.25f, out rightDist);

        if (isLeftOut && isRightOut)
        {
            lineRenderer.material.color = Color.white;
            return false;
        }

        if ((leftDist >= 0 && leftDist < 0.05f ) || (rightDist >= 0 && rightDist < 0.05f))
        {
            lineRenderer.material.color = Color.red;
            return true;
        }

        Vector3 relativePrev = (prevPosition - startPoint) + deltaMovement;
        Vector3 relativeNext = nextPosition - startPoint;

        float prev = Vector3.Dot(Vector3.up, relativePrev.normalized);
        float next = Vector3.Dot(Vector3.up, relativeNext.normalized);

        if (prev >= 0 && next < 0)
        {
            lineRenderer.material.color = Color.red;
            return true;
        }

        lineRenderer.material.color = Color.white;
        return false;
    }

    private bool IsPointOut(Vector3 point, out float distance)
    {
        Vector3 lineVector = endPoint - startPoint;
        float lineSqrMagnitude = lineVector.sqrMagnitude;

        float t = Vector3.Dot(point - startPoint, lineVector) / lineSqrMagnitude;

        Vector3 perpendicularPoint = startPoint + t * lineVector;
        distance = Vector3.Distance(point, perpendicularPoint);

        if (t < 0 || t > 1)
        {
            return true;
        }

        return false;
    }
}
