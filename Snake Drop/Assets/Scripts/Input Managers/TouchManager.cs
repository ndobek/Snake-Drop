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
                if (InValidArea(touch))
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        fingerDownPos = touch.position;
                        fingerUpPos = touch.position;
                        fingerHeldTime = 0;
                    }

                    fingerHeldTime += Time.deltaTime;
                    fingerUpPos = touch.position;
                    DetectHold();

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
    private Directions.Direction GetDirection()
    {
        Directions.Direction dir;
        if (IsVerticalSwipe())
        {
            if (fingerUpPos.y > fingerDownPos.y)
            {
                dir = Directions.Direction.UP;
            }
            else
            {
                dir = Directions.Direction.DOWN;
            }

        }
        else
        {
            if (fingerUpPos.x > fingerDownPos.x)
            {
                dir = Directions.Direction.RIGHT;
            }
            else
            {
                dir = Directions.Direction.LEFT;
            }
        }
        return dir;
    }

    private bool IsVerticalSwipe()
    {
        return VerticalSwipeDistance() > HorizontalSwipeDistance();
    }

    private bool LongEnoughForSwipe()
    {
        return Distance() > deadZone;
    }

    private float Distance()
    {
        return Vector2.Distance(fingerUpPos, fingerDownPos);
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

    private bool InValidArea(Touch touch)
    {
        return true;
        //return !EventSystem.current.IsPointerOverGameObject(touch.fingerId);
    }

    public TouchData RegisterTouch()
    {
        return new TouchData
        {
            startPos = fingerDownPos,
            endPos = fingerUpPos,
            direction = GetDirection(),
            distance = Distance(),
            timeHeld = fingerHeldTime
            
        };
    }

    public struct TouchData
    {
        public Vector2 startPos;
        public Vector2 endPos;
        public Directions.Direction direction;
        public float timeHeld;
        public float distance;
    }

    //public enum SwipeDirection
    //{
    //    Up,
    //    Down,
    //    Left,
    //    Right
    //}
}
