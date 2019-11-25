using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeManager : MonoBehaviour
{

    private Vector2 fingerDownPos;
    private Vector2 fingerUpPos;

    [SerializeField]
    private float deadZone;

    [SerializeField]
    private bool detectSwipeBeforeRelease = false;

    public static event Action<SwipeData> OnSwipe = delegate {};

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
                }

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
            SwipeDirection dir;
            if (IsVerticalSwipe())
            {
                if(fingerUpPos.y > fingerDownPos.y)
                {
                    dir = SwipeDirection.Up;
                }
                else
                {
                    dir = SwipeDirection.Down;
                }

            }
            else
            {
                if (fingerUpPos.x > fingerDownPos.x)
                {
                    dir = SwipeDirection.Right;
                }
                else
                {
                    dir = SwipeDirection.Left;
                }
            }
            RegisterSwipe(dir);
        }
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

    public void RegisterSwipe(SwipeDirection dir)
    {
        SwipeData obj = new SwipeData
        {
            startPos = fingerDownPos,
            endPos = fingerUpPos,
            direction = dir
        };
        Debug.Log(obj.direction);
        OnSwipe(obj);
    }

    public struct SwipeData
    {
        public Vector2 startPos;
        public Vector2 endPos;
        public SwipeDirection direction;
    }

    public enum SwipeDirection
    {
        Up,
        Down,
        Left,
        Right
    }
}
