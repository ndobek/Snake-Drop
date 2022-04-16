using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISpacer : MonoBehaviour
{
    public float xSpace;
    public float ySpace;
    public Vector2 origin;
    public UIFade[] elements;
    public UIFade checkIfFadedIn;

    private void Awake()
    {
        spaceElements();
    }
    private void Update()
    {
        if (checkIfFadedIn.FadedIn)
        {
            spaceElements();
        }
    }

    public void spaceElements()
    {
        List<RectTransform> activeElements = new List<RectTransform>();

        foreach (UIFade element in elements)
        {
            RectTransform t = element.GetComponent<RectTransform>();
            if(t != null && element.FadedIn && element.gameObject.activeSelf)
            {
                activeElements.Add(t);
            }
        }
        int count = activeElements.Count;
        float topX = origin.x + (count * xSpace * .5f);
        float topY = origin.y + (count * ySpace * .5f);

        for(int i = 0; i < count; i++)
        {
            float x = topX - (i * xSpace);
            float y = topY - (i * ySpace);
            activeElements[i].anchoredPosition = new Vector3(x, y, activeElements[i].position.z);
        }
    }
}
