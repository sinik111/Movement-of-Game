using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryRing : MonoBehaviour
{
    [SerializeField]
    private int segments;
    [SerializeField]
    private float radius;
    [SerializeField]
    private float width;

    private LineRenderer lineRenderer;
    private List<Vector3> vertices;

    public float Radius { get { return radius; } }

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;
        lineRenderer.loop = true;
        lineRenderer.useWorldSpace = true;
        lineRenderer.positionCount = segments;

        vertices = new List<Vector3>();

        float angle;

        for (int i = 0; i < segments; ++i)
        {
            angle = Mathf.Deg2Rad * i / segments * 360;

            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;

            vertices.Add(new Vector3(x, y, 0.0f));
        }

        for (int i = 0; i < segments; ++i)
        {
            lineRenderer.SetPosition(i, vertices[i]);
        }
    }
}
