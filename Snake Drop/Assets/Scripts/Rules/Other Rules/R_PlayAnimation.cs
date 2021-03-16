using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/General/PlayAnimation")]
[System.Serializable]
public class R_PlayAnimation : Rule
{
    public BlockAnimator animation;
    protected override void Action(Block block, PlayerManager player = null)
    {
        block.AnimationManager.AddAnimation(new BlockAnimation(block, animation));
    }
}
