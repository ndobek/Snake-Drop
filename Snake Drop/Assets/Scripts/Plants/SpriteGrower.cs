using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteGrower : MonoBehaviour, IGrowable
{
    public SpriteRenderer Renderer;
    [HideInInspector]
    public int growthStage = 0;
    public int GrowthStage { get; set; }

    public Sprite[] GrowthStates;

    private void Awake()
    {
        Renderer = gameObject.GetComponent<SpriteRenderer>();
        SetSprite();
    }

    public void Grow()
    {
        if (GrowthStates.Length > growthStage + 1) growthStage += 1;
        SetSprite();
    }

    private void SetSprite()
    {
        Renderer.sprite = GrowthStates[growthStage];
    }

}
