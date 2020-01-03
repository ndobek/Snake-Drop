using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightLimitIndicator : MonoBehaviour
{
    public float offset;
    public PlayGrid affectedGrid;

    [HideInInspector]
    public int HeightLimit;

    public void LowerHeightLimit(int temp)
    {
        if (temp < HeightLimit) HeightLimit = temp;
    }

    public void ResetHeightLimit()
    {
        HeightLimit = GameManager.instance.playGrid.YSize + 1;
    }
    void Update()
    {
        Vector2 obj = GameManager.instance.playGrid.CoordsPosition(0, HeightLimit + offset);
        this.transform.position = new Vector2(0, obj.y);
    }
    public bool CheckHeightLimit(BlockSlot slot)
    {
        if (!affectedGrid) throw new System.ArgumentNullException("Affected Grid");
        return slot.y <= HeightLimit | slot.playGrid != affectedGrid;
    }
}
