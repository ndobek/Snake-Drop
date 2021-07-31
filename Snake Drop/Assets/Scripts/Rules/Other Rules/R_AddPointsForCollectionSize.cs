using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/General/Add Points For CollectionSize")]
public class R_AddPointsForCollectionSize : Rule
{
    public enum SizeType
    {
        Area,
        SmallestDimension,
        LargestDimension
    }

    public SizeType sizeType;
    public int ScoreIncreasePerBlockInCollection;
    [SerializeField]
    public int MultiplierIncreasePerBlockInCollection;
    [SerializeField]
    private bool ApplyMultiplier;
    protected override void Action(Block block, PlayerManager player = null)
    {
        if (player && block.BlockCollection != null)
        {
            int SizeMultiplier = 1;
            int area = block.BlockCollection.Area();
            int x = block.BlockCollection.XSize();
            int y = block.BlockCollection.YSize();

            switch (sizeType)
            {
                case SizeType.Area:
                    SizeMultiplier = area;
                    break;

                case SizeType.SmallestDimension:
                    SizeMultiplier = x <= y ? x : y;
                    break;

                case SizeType.LargestDimension:
                    SizeMultiplier = x >= y ? x : y;
                    break;
            }
            int ScoreIncrease = SizeMultiplier * ScoreIncreasePerBlockInCollection;
            int MIncrease = SizeMultiplier * MultiplierIncreasePerBlockInCollection;

            player.Score.Multiplier += MIncrease;
            player.Score.IncreaseScore(ScoreIncrease, ApplyMultiplier);
        }
    }
}
