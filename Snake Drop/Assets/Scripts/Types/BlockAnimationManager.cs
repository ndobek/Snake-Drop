using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockAnimationManager : MonoBehaviour
{

    private Queue<BlockAnimation> UpcomingAnimations = new Queue<BlockAnimation>();
    [SerializeField]
    private List<BlockAnimation> ActiveAnimations = new List<BlockAnimation>();

    public void AddAnimation(BlockAnimation animation)
    {
        if (animation.Concurrent)
        {
            BeginAnimation(animation);
        } else
        {
            UpcomingAnimations.Enqueue(animation);
        }
    }

    private bool NonConcurrentInProgress()
    {
        bool result = false;
        foreach(BlockAnimation obj in ActiveAnimations)
        {
            if (!obj.Concurrent) result = true;
        }
        return result;
    }
    private void AddNonConcurrent()
    {
        if (!NonConcurrentInProgress() && UpcomingAnimations.Count > 0)
        {
            BeginAnimation(UpcomingAnimations.Dequeue());
        }
    }

    public void BeginAnimation(BlockAnimation animation)
    {
        if (!animation.begun)
        {
            ActiveAnimations.Add(animation);
            StartCoroutine(animation.Animate());
        }
    }

    public void RemoveComplete()
    {
        for(int i = ActiveAnimations.Count - 1; i == 0; i--)
        {
            if (ActiveAnimations[i].AnimationIsComplete()) ActiveAnimations.RemoveAt(i);
        }
    }

    public void Update()
    {
        AddNonConcurrent();
        RemoveComplete();
    }
}
