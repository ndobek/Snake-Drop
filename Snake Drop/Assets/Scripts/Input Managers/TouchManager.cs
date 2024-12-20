﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchManager : MonoBehaviour
{

    private Vector2 fingerDownPos;
    private Vector2 fingerUpPos;

    private float fingerHeldTime;
    private Directions.Direction mostRecentSwipedDirection;
    [SerializeField]
    private float minHoldLength;

    [SerializeField]
    public float deadZone;
    private bool disallowTouch = false;


    //[SerializeField]
    //private bool detectDrag = false;

    public static event Action<TouchData> OnTouchBegin = delegate { };
    public static event Action<TouchData> OnSwipe = delegate {};
    public static event Action<TouchData> OnHold = delegate { };
    public static event Action<TouchData> OnTap = delegate { };
    public static event Action<TouchData> OnTouchEnd = delegate { };

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (AreaValid(touch))
                {
                    if (disallowTouch) disallowTouch = false;
                    else {

                        if (touch.phase == TouchPhase.Began)
                        {
                            fingerDownPos = touch.position;
                            fingerUpPos = touch.position;
                            fingerHeldTime = 0;
                            OnTouchBegin(RegisterTouch());
                        }

                        fingerHeldTime += Time.deltaTime;
                        fingerUpPos = touch.position;

                        mostRecentSwipedDirection = SwipeDirection();

                        DetectHold();

                        if (touch.phase == TouchPhase.Ended)
                        {

                            DetectSwipe();
                            DetectTap();
                            OnTouchEnd(RegisterTouch());

                        }
                    }
                }
            }
        }
    }

    private void DetectSwipe()
    {
        if (DistanceLongEnoughForSwipe() && !TimeLongEnoughForHold())
        {

            OnSwipe(RegisterTouch());
        }
    }

    //private void OnTouchEnd()
    //{
    //    OnTouchEnd(RegisterTouch());
    //}

    private void DetectHold()
    {
        if(TimeLongEnoughForHold())
        {
            OnHold(RegisterTouch());
        }
    }

    private void DetectTap()
    {
        if(!TimeLongEnoughForHold() && !DistanceLongEnoughForSwipe())
        {
            OnTap(RegisterTouch());
        }
    }

    private bool IsVerticalSwipe()
    {
        return VerticalSwipeDistance() > HorizontalSwipeDistance();
    }

    private bool DistanceLongEnoughForSwipe()
    {
        return Distance() > deadZone;
    }

    private bool TimeLongEnoughForHold()
    {
        return fingerHeldTime >= minHoldLength;
    }

    private float Distance()
    {
        return Vector2.Distance(fingerUpPos, fingerDownPos);
    }
    private Directions.Direction SwipeDirection()
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
    private float VerticalSwipeDistance()
    {
        return Mathf.Abs(fingerUpPos.y - fingerDownPos.y);
    }
    private float HorizontalSwipeDistance()
    {
        return Mathf.Abs(fingerUpPos.x - fingerDownPos.x);
    }

    private bool AreaValid(Touch touch)
    {
        //return true;
        bool result = !EventSystem.current.IsPointerOverGameObject(touch.fingerId);
        if (result == false) disallowTouch = true;
        return result;
    }

    public TouchData RegisterTouch()
    {
        return new TouchData
        {
            startPos = fingerDownPos,
            endPos = fingerUpPos,
            longEnoughForSwipe = DistanceLongEnoughForSwipe(),
            direction = mostRecentSwipedDirection
        };
    }

    public struct TouchData
    {
        public Vector2 startPos;
        public Vector2 endPos;
        public bool longEnoughForSwipe;
        public Directions.Direction direction;
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
