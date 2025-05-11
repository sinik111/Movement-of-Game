using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CircularMovement : MonoBehaviour
{
    [SerializeField]
    private float radius;
    [SerializeField]
    private PlatformCollider[] platforms;
    [SerializeField]
    private float speed;

    private float rotationSpeed = Mathf.PI * 2f;
    private List<float> radians;

    private void Start()
    {
        rotationSpeed *= speed;

        int count = platforms.Length;

        radians = new List<float>();

        for (int i = 0; i < count; ++i)
        {
            radians.Add(Mathf.PI * 2f * i / count);
        }
    }

    private void Update()
    {
        for (int i = 0; i < radians.Count; ++i)
        {
            radians[i] += rotationSpeed * Time.deltaTime;

            if (radians[i] >= Mathf.PI * 2)
            {
                radians[i] -= Mathf.PI * 2f;
            }
        }

        for (int i = 0; i < platforms.Length; ++i)
        {
            float x = transform.position.x + radius * Mathf.Cos(radians[i]);
            float y = transform.position.y + radius * Mathf.Sin(radians[i]);

            Vector3 nextPosition = new Vector3(x, y, 0);

            platforms[i].DeltaMovement = nextPosition - platforms[i].transform.position;

            platforms[i].transform.position = nextPosition;
        }
    }
}
