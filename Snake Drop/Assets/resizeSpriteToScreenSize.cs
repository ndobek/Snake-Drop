using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class resizeSpriteToScreenSize : MonoBehaviour
{
    SpriteRenderer sr;
    public bool keepAspect;

    private void Update()
    {
        Resize();
    }

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Resize()
    {

        if (sr == null) return;

        transform.localScale = new Vector3(1, 1, 1);

        float width = sr.sprite.bounds.size.x;
        float height = sr.sprite.bounds.size.y;


        float worldScreenHeight = Camera.main.orthographicSize * 2f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        Vector3 newScale = transform.localScale;

        if(keepAspect)
        {
            newScale.x = (worldScreenWidth + Mathf.Abs(transform.localPosition.x) * 2) / width;
            newScale.y = (worldScreenHeight + Mathf.Abs(transform.localPosition.y) * 2) / height;

            newScale.x = Mathf.Max(newScale.y, newScale.x);
            newScale.y = newScale.x;
        }
        else
        {
            newScale.x = worldScreenWidth / width;
            newScale.y = worldScreenHeight / height;
        }

        transform.localScale = newScale;

    }
}
