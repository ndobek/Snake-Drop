using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockAnimationManager : MonoBehaviour
{

    public Queue<BlockAnimation> UpcomingAnimations = new Queue<BlockAnimation>();
    [SerializeField]
    public List<BlockAnimation> ActiveAnimations = new List<BlockAnimation>();

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
    private bool CanStartNewNonConcurrent()
    {
        RemoveComplete();
        return (!NonConcurrentInProgress()) && UpcomingAnimations.Count > 0;
    }
    private void AddNonConcurrent()
    {
        if (CanStartNewNonConcurrent())
        {
            BeginAnimation(UpcomingAnimations.Dequeue());
        }
    }

    public float GetLengthOfUpcomingAnimations()
    {
        float result = 0;
        foreach(BlockAnimation obj in UpcomingAnimations)
        {
            result += obj.Animator.AnimationTotalTime;
        }
        return result;
    }

    private void BeginAnimation(BlockAnimation animation)
    {
        //if (!animation.begun)
        //{
            ActiveAnimations.Add(animation);
            StartCoroutine(animation.Animate());
        //}
    }

    public void RemoveComplete()
    {
        for(int i = ActiveAnimations.Count - 1; i >= 0; i--)
        {
            if (ActiveAnimations[i].AnimationIsComplete()) ActiveAnimations.RemoveAt(i);
        }
    }

    public void LateUpdate()
    {
        //Debug.Log(ActiveAnimations.Count);
        while (CanStartNewNonConcurrent())
        {
            AddNonConcurrent();
        }

    }
}
