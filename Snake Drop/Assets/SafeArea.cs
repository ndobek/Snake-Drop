//source: https://www.youtube.com/watch?v=VprqsEsFb5w

using UnityEngine;
using UnityEngine.UI;

public class SafeArea : MonoBehaviour
{
    private RectTransform t;
    private Rect safeArea;
    private Vector2 minAnchor;
    private Vector2 maxAnchor;

    private void OnRectTransformDimensionsChange()
    {
        ApplySafeArea();
    }

    public void ApplySafeArea()
    {
        t = GetComponent<RectTransform>();
        safeArea = Screen.safeArea;
        minAnchor = safeArea.position;
        maxAnchor = minAnchor + safeArea.size;

        minAnchor.x /= Screen.width;
        minAnchor.y /= Screen.height;
        maxAnchor.x /= Screen.width;
        maxAnchor.y /= Screen.height;

        t.anchorMin = minAnchor;
        t.anchorMax = maxAnchor;
    }
}