using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchManager : MonoBehaviour
{

    private Vector2 fingerDownPos;
    private Vector2 fingerUpPos;

    private float fingerHeldTime;
    private GameManager.Direction mostRecentSwipedDirection;
    [SerializeField]
    private float minHoldLength;

    [SerializeField]
    private float deadZone;


    [SerializeField]
    private bool detectSwipeBeforeRelease = false;

    public static event Action<TouchData> OnSwipe = delegate {};
    public static event Action<TouchData> OnHold = delegate { };
    public static event Action<TouchData> OnTap = delegate { };

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (InValidArea())
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        fingerDownPos = touch.position;
                        fingerUpPos = touch.position;
                        fingerHeldTime = 0;
                    }

                    fingerHeldTime += Time.deltaTime;
                    DetectHold();
                    fingerUpPos = touch.position;

                    if (detectSwipeBeforeRelease && touch.phase == TouchPhase.Moved)
                    {
                        DetectSwipe();
                    }

                    if (touch.phase == TouchPhase.Ended)
                    {
                        DetectSwipe();
                        DetectTap();
                    }
                }
            }
        }
    }

    private void DetectSwipe()
    {
        if (LongEnoughForSwipe())
        {
            GameManager.Direction dir;
            if (IsVerticalSwipe())
            {
                if(fingerUpPos.y > fingerDownPos.y)
                {
                    dir = GameManager.Direction.UP;
                }
                else
                {
                    dir = GameManager.Direction.DOWN;
                }

            }
            else
            {
                if (fingerUpPos.x > fingerDownPos.x)
                {
                    dir = GameManager.Direction.RIGHT;
                }
                else
                {
                    dir = GameManager.Direction.LEFT;
                }
            }
            mostRecentSwipedDirection = dir;
            OnSwipe(RegisterTouch());
        }
    }

    private void DetectHold()
    {
        if(LongEnoughForHold())
        {
            OnHold(RegisterTouch());
        }
    }

    private void DetectTap()
    {
        if(!LongEnoughForHold() && !LongEnoughForSwipe())
        {
            OnTap(RegisterTouch());
        }
    }

    private bool IsVerticalSwipe()
    {
        return VerticalSwipeDistance() > HorizontalSwipeDistance();
    }

    private bool LongEnoughForSwipe()
    {
        return Vector2.Distance(fingerUpPos, fingerDownPos) > deadZone;
    }

    private bool LongEnoughForHold()
    {
        return fingerHeldTime >= minHoldLength;
    }

    private float VerticalSwipeDistance()
    {
        return Mathf.Abs(fingerUpPos.y - fingerDownPos.y);
    }
    private float HorizontalSwipeDistance()
    {
        return Mathf.Abs(fingerUpPos.x - fingerDownPos.x);
    }

    private bool InValidArea()
    {
        return true;
    }

    public TouchData RegisterTouch()
    {
        return new TouchData
        {
            startPos = fingerDownPos,
            endPos = fingerUpPos,
            direction = mostRecentSwipedDirection
        };
    }

    public struct TouchData
    {
        public Vector2 startPos;
        public Vector2 endPos;
        public GameManager.Direction direction;
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
