using UnityEngine;

public class SideBlocks : MonoBehaviour
{
    private void Start()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();

        Vector2 sizeDelta = rectTransform.sizeDelta;
        rectTransform.sizeDelta = sizeDelta;
    }
}
