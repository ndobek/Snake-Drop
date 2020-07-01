using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceAnimationManager : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    private EntranceSlot slot;
    public PlayGrid grid;

    public Sprite Open;
    public Sprite Closed;


    private void Awake()
    {
        slot = gameObject.GetComponent<EntranceSlot>();
    }
    void Update()
    {
        if (slot.CheckIfEntranceHasOpeningToGrid(grid)) spriteRenderer.sprite = Open;
        else spriteRenderer.sprite = Closed;
    }
}
