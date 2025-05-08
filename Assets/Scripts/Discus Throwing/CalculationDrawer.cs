using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalculationDrawer : MonoBehaviour
{
    [SerializeField]
    private Text resultText;
    [SerializeField]
    private Discus discus;

    private LineRenderer lineRenderer = null;
    private Vector3[] linePositions = new Vector3[3];
    private Vector3 textPosition;
    private float score;

    private void OnEnable()
    {
        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();

            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            lineRenderer.startColor = Color.white;
            lineRenderer.endColor = Color.white;
            lineRenderer.startWidth = 0.02f;
            lineRenderer.endWidth = 0.02f;
            lineRenderer.loop = false;
            lineRenderer.useWorldSpace = true;
            lineRenderer.positionCount = 3;
        }

        for (int i = 0; i < 3; ++i)
        {
            lineRenderer.SetPosition(i, linePositions[i]);
        }

        resultText.text = string.Format("{0:F2}", score);
        resultText.gameObject.GetComponent<RectTransform>().position = textPosition;
        resultText.gameObject.SetActive(true);

        StartCoroutine(DisableAfterDelay());
    }

    private void OnDisable()
    {
        resultText.gameObject.SetActive(false);
    }

    private IEnumerator DisableAfterDelay()
    {
        yield return new WaitForSeconds(2.0f);

        discus.gameObject.SetActive(true);
        discus.ResetDiscus();

        gameObject.SetActive(false);
    }

    public void SetInfo(Vector3[] positions, float score)
    {
        linePositions = positions;
        this.score = score;

        Vector3 dir = (linePositions[2] - linePositions[1]).normalized;

        textPosition = Camera.main.WorldToScreenPoint(linePositions[2] + dir * 0.25f);
    }
}
