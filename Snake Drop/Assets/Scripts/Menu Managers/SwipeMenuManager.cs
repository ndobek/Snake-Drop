using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeMenuManager : MonoBehaviour
{
    public List<Transform> menuItems = new List<Transform>();
    public Transform CameraTransform;
    public Transform AnchorTransform;
    [SerializeField]
    private bool loop;
    [SerializeField]
    private bool MoveCamera;
    public float xDistance;
    public float yDistance;
    public float zDistance;
    public float lerpSpeed;
    [SerializeField]
    private int currentSelection;
    [SerializeField]
    private bool ControlsActive;

    private void Awake()
    {
        TouchManager.OnSwipe += TouchManager_OnSwipe;
    }

    private void Start()
    {
        UpdateCameraLocation(1);
        UpdateMenuItemLocations(1);
    }

    private void TouchManager_OnSwipe(TouchManager.TouchData touch)
    {
        if (ControlsActive)
        {
            if (touch.direction == Directions.Direction.LEFT) MoveLeft();
            if (touch.direction == Directions.Direction.RIGHT) MoveRight();
        }
    }

    public void Add(Transform transform) { menuItems.Add(transform); }
    public void Remove(Transform transform) { menuItems.Remove(transform); }
    public void SetSelection(int selection) { currentSelection = selection; }

    public void EnableControls(bool enabled) { ControlsActive = enabled; }
    public void MoveLeft()
    {
        if (currentSelection == 0)
        {
            if (loop) currentSelection = menuItems.Count - 1;
        }
        else currentSelection -= 1;

    }

    public void MoveRight()
    {
        if (currentSelection == menuItems.Count - 1)
        {
            if (loop) currentSelection = 0;
        }
        else currentSelection += 1;
    }

    private void UpdateMenuItemLocations(float speed = 0)
    {
        for(int i =0; i < menuItems.Count; i++)
        {
            if (menuItems[i] != null)
            {
                SetPosition(menuItems[i], MoveCamera?i:i-currentSelection, speed);
            }
            else menuItems.RemoveAt(i);
        }
    }

    private void SetPosition(Transform transform, float index, float speed = 0)
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(AnchorTransform.position.x + xDistance * index, AnchorTransform.position.y + yDistance * index, AnchorTransform.position.z + zDistance * index), speed == 0? lerpSpeed : speed);
    }

    private void UpdateCameraLocation(float speed = 0)
    {
        SetPosition(CameraTransform, MoveCamera?currentSelection:0, speed);
    }


    private void Update()
    {
        if (ControlsActive)
        {
            if (Input.GetKeyDown("a") || Input.GetKeyDown("left")) { MoveLeft(); }
            if (Input.GetKeyDown("d") || Input.GetKeyDown("right")) { MoveRight(); }
        }

        UpdateMenuItemLocations();
        UpdateCameraLocation();
    }


}
