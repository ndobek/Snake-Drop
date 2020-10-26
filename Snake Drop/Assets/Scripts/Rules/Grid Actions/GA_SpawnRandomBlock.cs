using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/Grid Actions/Spawn Block")]
public class GA_SpawnRandomBlock : GridAction
{
    public BlockColor color;
    public BlockType type;
    public bool GameOverIfUnable;

    protected override void Action(PlayGrid grid)
    {
        List<BlockSlot> emptyBlockSlots = grid.EmptyBlockSlots();
        BlockSlot randomSlot = emptyBlockSlots[Random.Range(0, emptyBlockSlots.Count)];
        if (randomSlot) randomSlot.CreateBlock(color, type);
        else if (GameOverIfUnable) GameManager.instance.playerManagers[0].EndGame();
    }
}
