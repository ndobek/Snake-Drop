using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/General/Containers And Conditions/Do If Part Of Snake")]
public class R_DoIfPartOfSnake : Rule
{
    public Rule DoIfPartOfSnake;
    public Rule DoIfNotPartOfSnake;

    protected override void Action(Block block, PlayerManager player = null)
    {
        if (block.isPartOfSnake() && DoIfPartOfSnake != null) DoIfPartOfSnake.Invoke(block, player);
        if (!block.isPartOfSnake() && DoIfNotPartOfSnake != null) DoIfNotPartOfSnake.Invoke(block, player);
    }
}
