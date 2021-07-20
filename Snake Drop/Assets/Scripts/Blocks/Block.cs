using System;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    #region Variables

    #region Block Info

    [HideInInspector]
    public BlockType blockType;
    [HideInInspector]
    public BlockColor blockColor;
    public BlockCollection BlockCollection;

    public BlockAnimationManager AnimationManager;
    public SpriteRenderer BlockSprite;
    public LineRenderer Highlight;
    public SpriteMask FillMask;
    public Transform FillMaskTransform;
    public SpriteRenderer FillSpriteRenderer;

    public Directions.Direction mostRecentDirectionMoved = Directions.Direction.DOWN;

    private BlockSlot slot;
    public BlockSlot Slot
    {
        get { return slot; }
    }
    private PlayerManager owner;
    public PlayerManager Owner
    {
        get { return owner; }
        set { SetOwner(value); }
    }

    #endregion

    #region Coordinates

    public Vector2 Coords()
    {
        return new Vector2(Slot.x, Slot.y);
    }
    public int X
    {
        get { return Slot.x; }
    }
    public int Y
    {
        get { return Slot.y; }
    }
    public BlockSlot Neighbor(Directions.Direction direction)
    {
        return Slot.GetNeighbor(direction);
    }

    #endregion

    #region Snake Data

    public bool isPartOfSnake()
    {
        return isPartOfSnake(this);
    }
    public static bool isPartOfSnake(Block obj)
    {
        return obj.blockType.isPartOfSnake;
    }
    public static bool isNotPartOfSnake(Block obj)
    {
        return !isPartOfSnake(obj);
    }
    private Block head;
    public Block Head { get => head; set { head = value; } }

    private Block tail;
    public Block Tail
    {
        get { return tail; }
        set { SetTail(value);}
    }

    #endregion

    #endregion

    #region Methods to Update Block Status

    public void SetBlockType(BlockColor color, BlockType type)
    {
        blockType = type;
        blockColor = color;
        UpdateSprite();
    }
    private void AddAnimations()
    {
        foreach (BlockAnimator obj in blockType.EveryFrameAnimations)
        {
            AnimationManager.AddAnimation(new BlockAnimation(this, obj));
        }
    }
    private void UpdateSprite()
    {
        AnimationManager.AddAnimation(new BlockAnimation(this, blockColor.GetAnimator(blockType)));
      
    }
    private void LateUpdate()
    {
        AddAnimations();
        
    }

    //Tells the grid that it needs to check for fall movement and update
    private void SetGridDirty()
    {
        if (Slot && Slot.playGrid) Slot.playGrid.SetDirty();
    }

    #endregion

    #region Movement Methods

    //Base for all movement code, doesn't follow game rules.
    #region Raw Movement

    public void RawMove(Directions.Direction neighbor)
    {
        RawMoveTo(Neighbor(neighbor));
    }
    public virtual void RawMoveTo(BlockSlot obj, BlockAnimator animation = null)
    {
        SetGridDirty();
        UpdateSprite();
        BlockSlot Old = Slot;
        if (Old)
        {

            if (Old.playGrid == obj.playGrid) { mostRecentDirectionMoved = Directions.DirectionTo(Old, obj); }
            else
            {
                mostRecentDirectionMoved = GameManager.instance.playerManagers[0].enterSlot.GetEntranceDirection(obj.playGrid);                
            }
            Old.OnUnassignment(this);
            
        }
        else
        {
            mostRecentDirectionMoved = Directions.Direction.DOWN;
        }
        if (obj)
        {
            if (animation == null) animation = blockType.defaultMoveAnimator;
            AnimationManager.AddAnimation(new BlockAnimation(this, animation, Old == null ? obj.transform : Old.transform, obj.transform));
            slot = obj;
            obj.OnAssignment(this);
            if (Tail != null) Tail.RawMoveTo(Old);
        }
        else
        {
            throw new System.Exception("No Slot to move to");
        }
        UpdateSprite();
    }
    public void RawBreak()
    {
        SetGridDirty();
        Slot.OnUnassignment(this);
        GameObject.Destroy(this.gameObject);
    }

    #endregion

    public void SpecialAction(PlayerManager player = null)
    {
        blockType.SpecialAction(this, player);
    }
    public void Move(Directions.Direction neighbor, PlayerManager player = null)
    {
        MoveTo(Neighbor(neighbor), player);
    }
    public virtual void MoveTo(BlockSlot obj, PlayerManager player = null)
    {
        SetGridDirty();
        if (player && obj) obj.SetOwner(player);
        blockType.OnMove(this, obj, player);
    }
    public bool CanMoveToWithoutCrashing(BlockSlot obj, PlayerManager player = null)
    {
        return blockType.CanMoveToWithoutCrashing(this, obj, player);
    }

    public void Break(PlayerManager player = null)
    {
        SetGridDirty();
        blockType.OnBreak(this, player);
    }


    #endregion

    #region Snake Controls
    public void SetOwner(PlayerManager NewOwner = null)
    {
        if (owner == null)
        {
            owner = GameManager.instance.playerManagers[0];
            return;
        }
        if (Tail != null)
        {
            Tail.SetOwner(NewOwner);
        }
        owner = NewOwner;
    }
    public void ApplyRuleToSnake(Rule rule, PlayerManager player = null)
    {
        if(Tail != null)
        {
            Tail.ApplyRuleToSnake(rule);
        }
        rule.Invoke(this, player);
    }

    

        public void SetTail(Directions.Direction neighbor)
    {
        SetTail(Neighbor(neighbor).Block);
    }
    public void SetTail(Block obj)
    {
        tail = obj;
        if (tail)
        {
            tail.head = this;
        }
        
    }
    public Block GetLastTail()
    {
        if (tail) return tail.GetLastTail();
        else return this;
    }

    public void Kill(PlayerManager player = null)
    {
        blockType.OnKill(this, player);
    }
    public void KillSnake(PlayerManager player = null)
    {

        if (Tail != null)
        {
/*            if (Tail.Slot.playGrid == Slot.playGrid) */
            Tail.KillSnake(player);
            SetTail(null);
        }
        Kill(player);
    }

    public int SnakeLength()
    {
        if (Tail != null)
        {
            return Tail.SnakeLength() + 1;
        }
        else return 1;
    }

    public int SnakeLengthInPlayGrid()
    {
        PlayGrid playGrid = GameManager.instance.playerManagers[0].playGrid;
        if (Tail != null && Tail.slot.playGrid == playGrid)
        {
            return Tail.SnakeLengthInPlayGrid() + 1;
        }
        else return slot.playGrid == playGrid ? 1 : 0;
    }


    public int FindSnakeMaxY()
    {
        int obj1 = (int)Coords().y;
        int obj2 = 0;
        if (tail != null)
        {
            obj2 = tail.FindSnakeMaxY();
        }
        int result = (obj1 > obj2) ? obj1 : obj2;
        return result;
    }

    #endregion


    //public BlockSaveData Save(SaveData save)
    //{
    //    return Save(save, slot.Blocks.IndexOf(tail));
    //}
    //public BlockSaveData Save(SaveData save, int i)
    //{
    //    BlockSaveData result = new BlockSaveData()
    //    {
    //        blockColor = blockColor.Name,
    //        blockType = blockType.Name,
    //        index = i,
    //        tail = tail != null,
    //    };

    //    if(tail != null)
    //    {
    //        result.tailOnLoadGrid = tail.slot.playGrid == GameManager.instance.playerManagers[0].previewGrid;
    //        result.tailX = tail.X;
    //        result.tailY = tail.Y;
    //        result.tailI = tail.slot.Blocks.IndexOf(tail);
    //    }
    //    if(BlockCollection != null)
    //    {
    //        save.blockCollections.Add(BlockCollection.Save());
    //    }

    //    return result;
    //}
}
