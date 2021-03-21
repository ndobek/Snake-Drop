using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[CreateAssetMenu(menuName = "Plants/Animation State")]
public class AnimationState : ScriptableObject, ICyclical
{
    [SerializeField]
    private float cycleLength;
    public float CycleLength { get => cycleLength; set { cycleLength = value; } }

    private float cyclePoint;
    public float CyclePoint { get => cyclePoint; set { cyclePoint = value; } }

    public void CycleUpdate()
    {
        throw new System.NotImplementedException();
    }

   
}
