using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{

    private Vector2 fingerDownPos;
    private Vector2 fingerUpPos;

    private float fingerHeldTime;
    [SerializeField]
    private float minHoldLength;

    [SerializeField]
    private float deadZone;


    [SerializeField]
    private bool detectSwipeBeforeRelease = false;

    public static event Action<SwipeData> OnSwipe = delegate {};
    public static event Action<HoldData> OnHold = delegate { };

    private void Update()
    {
        if(Input.touchCount > 0)
        {
            foreach(Touch touch in Input.touches)
            {
                if(touch.phase == TouchPhase.Began)
                {
                    fingerDownPos = touch.position;
                    fingerUpPos = touch.position;
                    fingerHeldTime = 0;
                }

                //if(touch.phase == TouchPhase.Stationary)
                //{
                    fingerHeldTime += Time.deltaTime;
                    DetectHold();
                //}

                if(detectSwipeBeforeRelease && touch.phase == TouchPhase.Moved)
                {
                    fingerUpPos = touch.position;
                    DetectSwipe();
                }

                if(touch.phase == TouchPhase.Ended)
                {
                    fingerUpPos = touch.position;
                    DetectSwipe();
                }

            }
        }
    }

    private void DetectSwipe()
    {
        if (SwipeWasLongEnough())
        {
            GameManager.Direction dir;
            if (IsVerticalSwipe())
            {
                if(fingerUpPos.y > fingerDownPos.y)
                {
                    dir = GameManager.Direction.Up;
                }
                else
                {
                    dir = GameManager.Direction.Down;
                }

            }
            else
            {
                if (fingerUpPos.x > fingerDownPos.x)
                {
                    dir = GameManager.Direction.Right;
                }
                else
                {
                    dir = GameManager.Direction.Left;
                }
            }
            RegisterSwipe(dir);
        }
    }

    private void DetectHold()
    {
        if(fingerHeldTime >= minHoldLength)
        {
            RegisterHold(fingerHeldTime);
        }
    }

    private void RegisterHold(float time)
    {
        HoldData obj = new HoldData
        {
            Pos = fingerDownPos,
            TimeHeld = time
        };
        OnHold(obj);
    }

    private bool IsVerticalSwipe()
    {
        return VerticalSwipeDistance() > HorizontalSwipeDistance();
    }

    private bool SwipeWasLongEnough()
    {
        return Vector2.Distance(fingerUpPos, fingerDownPos) > deadZone;
    }

    private float VerticalSwipeDistance()
    {
        return Mathf.Abs(fingerUpPos.y - fingerDownPos.y);
    }
    private float HorizontalSwipeDistance()
    {
        return Mathf.Abs(fingerUpPos.x - fingerDownPos.x);
    }

    public void RegisterSwipe(GameManager.Direction dir)
    {
        SwipeData obj = new SwipeData
        {
            startPos = fingerDownPos,
            endPos = fingerUpPos,
            direction = dir
        };
        //Debug.Log(obj.direction);
        OnSwipe(obj);
    }

    public struct SwipeData
    {
        public Vector2 startPos;
        public Vector2 endPos;
        public GameManager.Direction direction;
    }

    public struct HoldData
    {
        public Vector2 Pos;
        public float TimeHeld;
    }

    //public enum SwipeDirection
    //{
    //    Up,
    //    Down,
    //    Left,
    //    Right
    //}
}
