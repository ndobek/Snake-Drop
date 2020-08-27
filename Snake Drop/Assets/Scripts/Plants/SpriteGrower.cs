using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class SpriteGrower : MonoBehaviour, IGrowable
//{
//    public SpriteRenderer Renderer;
//    [HideInInspector]
//    public int currentState = 0;
//    public int GrowthStage
//    {
//        get { return currentState; }
//        set { currentState = value; }
//    }

//    public Sprite[] GrowthStates;

//    private void Awake()
//    {
//        Renderer = gameObject.GetComponent<SpriteRenderer>();
//        SetSprite();
//    }

//    public void Grow()
//    {
//        if (GrowthStates.Length > currentState + 1) currentState += 1;
//        SetSprite();
//    }

//    private void SetSprite()
//    {
//        Renderer.sprite = GrowthStates[currentState];
//    }
//    public void ResetGrowth()
//    {
//        GrowthStage = 0;
//    }
//}
