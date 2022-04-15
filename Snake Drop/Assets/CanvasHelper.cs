//source: https://stackoverflow.com/questions/63113590/unity-how-to-get-the-coordinates-of-the-safearea-rect

using UnityEngine;
using UnityEngine.UI;

public class CanvasHelper : MonoBehaviour
{
    void Start()
    {
        ApplyVerticalSafeArea();
    }


    public void ApplyVerticalSafeArea()
    {

        var rectTransform = GetComponent<RectTransform>();
        float x = Screen.width - Screen.safeArea.x;
        float y = Screen.height - Screen.safeArea.y;
        rectTransform.sizeDelta = new Vector2(x, y);


        //var bottomPixels = Screen.safeArea.y;
        //var topPixel = Screen.currentResolution.height - (Screen.safeArea.y + Screen.safeArea.height);

        //var bottomRatio = bottomPixels / Screen.currentResolution.height;
        //var topRatio = topPixel / Screen.currentResolution.height;

        //var referenceResolution = canvasScaler.referenceResolution;
        //bottomUnits = referenceResolution.y * bottomRatio;
        //topUnits = referenceResolution.y * topRatio;

        //var rectTransform = GetComponent<RectTransform>();
        //rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x, bottomUnits);
        //rectTransform.offsetMax = new Vector2(rectTransform.offsetMax.x, -topUnits);
    }
}