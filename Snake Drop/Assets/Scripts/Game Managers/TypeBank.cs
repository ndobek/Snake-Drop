using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Type Bank")]
public class TypeBank : ScriptableObject
{
    public Block blockObj;
    public BlockType basicType;
    public BlockType snakeHeadType;
    public BlockType collectionType;
    public BlockType collectionGhostType;
    public BlockType specialType;
}
