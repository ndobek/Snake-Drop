using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : Block
{



    public override void MoveTo(int x, int y)
    {
        //playGrid.SetBlock(x, y);
        base.MoveTo(x, y);
    }

}
