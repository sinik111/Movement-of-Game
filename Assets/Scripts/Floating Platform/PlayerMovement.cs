using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerMovement : MonoBehaviour
{
    private readonly float acceleration = 30.0f;
    private Vector3 velocity;

    private bool isGround;

    private float allowedWidth;

    private List<PlatformCollider> platforms;

    private void Start()
    {
        isGround = false;
        velocity = Vector3.zero;
        allowedWidth = Camera.main.orthographicSize * Camera.main.aspect;

        platforms = new List<PlatformCollider>();

        foreach (var item in GameObject.FindGameObjectsWithTag("Platform"))
        {
            platforms.Add(item.GetComponent<PlatformCollider>());
        }
    }

    private void Update()
    {
        InputProcess();

        Gravity();

        Vector3 nextPosition = transform.position + velocity * Time.deltaTime;

        isGround = false;

        foreach (var item in platforms)
        {
            if (item.IsCollide(nextPosition, transform.position))
            {
                if (Vector3.Dot(velocity.normalized, Vector3.up) <= 0)
                {
                    nextPosition.y = item.transform.position.y + 0.5f;
                    nextPosition = nextPosition + item.DeltaMovement;
                    velocity.y = 0.0f;
                    isGround = true;
                }
            }
        }


        if (nextPosition.x < -allowedWidth + 0.25f)
        {
            nextPosition.x = -allowedWidth + 0.25f;
        }
        else if (nextPosition.x > allowedWidth - 0.25f)
        {
            nextPosition.x = allowedWidth - 0.25f;
        }

        transform.position = nextPosition;
    }

    private void InputProcess()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            velocity.y += 12.0f;
            isGround = false;
        }

        velocity.x = Input.GetAxis("Horizontal") * 5.0f;
    }

    private void Gravity()
    {
        velocity.y -= acceleration * Time.deltaTime;
        if (velocity.y < -50.0f)
        {
            velocity.y = -50.0f;
        }
    }
}
