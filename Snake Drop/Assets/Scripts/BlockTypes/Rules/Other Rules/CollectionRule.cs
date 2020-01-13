using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Blocks/BlockTypes/Rules/CollectionRule")]
public class CollectionRule : Rule
{
    public Rule Rule;

    protected override void Action(Block block)
    {
        Block[] blocks = block.BlockCollection.Blocks;

        foreach (Block obj in blocks)
        {
            Rule.Invoke(obj);
        }
    }
}
