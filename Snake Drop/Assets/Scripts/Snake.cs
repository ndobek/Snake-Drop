using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : Block
{
    private void Update()
    {
        if (Input.GetButtonDown("Left"))
        {
            MoveLeft();
        }
        if (Input.GetButtonDown("Right"))
        {
            MoveRight();
        }
        if (Input.GetButtonDown("Up"))
        {
            MoveUp();
        }
        if (Input.GetButtonDown("Down"))
        {
            MoveDown();
        }
    }


    public override void MoveTo(int x, int y)
    {
        //playGrid.SetBlock(x, y);
        base.MoveTo(x, y);
    }

}
