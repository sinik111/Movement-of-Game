using UnityEngine;
using UnityEngine.UI;

public class Discus : MonoBehaviour
{
    [SerializeField]
    private GameObject thrower;
    [SerializeField]
    private DiscusTarget target;
    [SerializeField]
    private float speed;

    [SerializeField]
    private int maxTryCount;
    [SerializeField]
    private Text tryCounterText;
    [SerializeField]
    private Text hitCounterText;
    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private CalculationDrawer calcDrawer;

    private float radius;

    private float currentRadian;
    private float rotateRadius;
    private float rotateSpeed;

    private Vector3 throwedPosition;
    private Vector3 throwDirection;

    private float screenHeight;
    private float screenWidth;

    private int currentTryCount;
    private int hitCount;
    private float scoreSum;

    private bool isThrowed;

    private void Start()
    {
        currentTryCount = maxTryCount;
        tryCounterText.text = string.Format("Tries Left: {0}", currentTryCount);

        radius = transform.GetChild(0).localScale.x * 0.5f;

        currentRadian = 0.0f;
        rotateRadius = 1.0f;
        rotateSpeed = Mathf.PI * 2.0f / (speed / 2.0f);

        screenHeight = Camera.main.orthographicSize;
        screenWidth = screenHeight * Camera.main.aspect;

        scoreSum = 0.0f;

        isThrowed = false;
    }

    private void Update()
    {
        Throw();
        Move();
        CollisionCheck();
    }

    private void Throw()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isThrowed && currentTryCount > 0)
        {
            --currentTryCount;
            tryCounterText.text = string.Format("Tries Left: {0}", currentTryCount);

            isThrowed = true;

            Vector3 dirFromCenter = (transform.position - thrower.transform.position).normalized;
            throwDirection = new Vector3(-dirFromCenter.y, dirFromCenter.x, 0.0f);

            throwedPosition = transform.position;
        }
    }

    private void Move()
    {
        if (!isThrowed)
        {
            currentRadian += rotateSpeed * Time.deltaTime;

            float x = thrower.transform.position.x + rotateRadius * Mathf.Cos(currentRadian);
            float y = thrower.transform.position.y + rotateRadius * Mathf.Sin(currentRadian);

            transform.position = new Vector3(x, y, 0.0f);
        }
        else
        {
            Vector3 nextPosition = transform.position + throwDirection * speed * Time.deltaTime;

            if (nextPosition.x < -screenWidth + radius || nextPosition.x > screenWidth - radius ||
                nextPosition.y < -screenHeight + radius || nextPosition.y > screenHeight - radius)
            {
                if (currentTryCount == 0)
                {
                    Destroy(gameObject);
                    Destroy(target.gameObject);
                }
                else
                {
                    isThrowed = false;
                }
            }
            else
            {
                transform.position = nextPosition;
            }
        }
    }

    private void CollisionCheck()
    {
        if (radius + target.Radius > (transform.position - target.transform.position).magnitude)
        {
            ++hitCount;
            hitCounterText.text = string.Format("Hit: {0}", hitCount);

            CalcScore();
        }
    }

    private void CalcScore()
    {
        Vector3 dirToStart = (throwedPosition - target.transform.position).normalized;
        Vector3 dirToCollide = (transform.position - target.transform.position).normalized;

        float dot = Vector3.Dot(dirToStart, dirToCollide);
        float score = 10.0f * dot;

        scoreSum += score;
        scoreText.text = string.Format("Score: {0:F2}", scoreSum);

        calcDrawer.SetInfo(new Vector3[3] { throwedPosition, target.transform.position, transform.position }, score);
        calcDrawer.gameObject.SetActive(true);

        gameObject.SetActive(false);
    }

    public void ResetDiscus()
    {
        if (currentTryCount == 0)
        {
            Destroy(gameObject);
            Destroy(target.gameObject);
        }
        else
        {
            target.SetNewPosition();
            isThrowed = false;
        }
    }


}
