using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/Grid Actions/Spawn Block")]
public class GA_SpawnRandomBlock : GridAction
{
    public BlockColor color;
    public BlockType type;
    //public bool GameOverIfUnable;

    [SerializeField]
    private int minX;
    [SerializeField]
    private int maxX;
    [SerializeField]
    private int minY;
    [SerializeField]
    private int maxY;

    protected override void Action(PlayGrid grid)
    {
        List<BlockSlot> emptyBlockSlots = grid.EmptyBlockSlots(minX, maxX, minY, maxY);

        if (emptyBlockSlots.Count > 0) 
        {
            BlockSlot randomSlot = emptyBlockSlots[Random.Range(0, emptyBlockSlots.Count)];
            randomSlot.CreateBlock(color, type);
        }
        //else if (GameOverIfUnable) GameManager.instance.playerManagers[0].EndGame();
    }
}
