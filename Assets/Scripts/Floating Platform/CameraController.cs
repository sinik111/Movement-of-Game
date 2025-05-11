using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject target;

    private float limitY;

    private void Start()
    {
        limitY = Camera.main.orthographicSize * 0.8f;
    }

    private void Update()
    {
        Vector3 newPosition = transform.position;

        if (target.transform.position.y >= limitY)
        {
            newPosition.y = target.transform.position.y;
        }
        else
        {
            newPosition.y = limitY;
        }

        Vector3 lerpPosition = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * 5.0f);

        transform.position = lerpPosition;
    }
}
