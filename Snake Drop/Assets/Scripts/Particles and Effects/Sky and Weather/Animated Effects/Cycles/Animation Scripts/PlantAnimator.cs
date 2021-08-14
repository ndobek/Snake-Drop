using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantAnimator : MonoBehaviour, IEffectAnimator<Animator, GrowthStage, GrowthStage>, IReact 
{
    public EnvironmentalEffects environmentData;
    
    public Animator animator;
    public PlantSpecies species;
    
    private GrowthStage initialState;
    public GrowthStage InitialState { get => initialState; }
    private GrowthStage currentState;
    public GrowthStage CurrentState { get => currentState; set { currentState = value; } }
    private GrowthStage previousState;
    public GrowthStage PreviousState { get => previousState; set { previousState = value; } }
    public List<PlantClip> currentStageClips = new List<PlantClip>();
    
    private void SetStageClips(GrowthStage growthStage)
    {
        currentStageClips.Clear();
        foreach (PlantClip clip in growthStage.clips)
        {
            
            currentStageClips.Add(clip);
            
        }

    }
   
    private void Start()
    {
        initialState = species.stages[0];
        currentState = initialState;
        previousState = initialState;
        environmentData.affectedAnimators.Add(this);
        currentClip = species.stages[0].clips[0];

        animator = GetComponent<Animator>();
    }
    public void SetGrowthStage(int stage)
    {
        Debug.Log("Fix Me!");
        // SetStageClips(species.stages[stage]);
        // if (species.stages.Count > stage)
        // {                         
        //     Animate(CurrentState, species.stages[stage], 1f);
        // }
        // else if(species.stages.Count > 0)
        // {
        //     Animate(CurrentState, species.stages[species.stages.Count-1], 1f);
        // }
        
        
    }
    public void Animate(GrowthStage keyframe1, GrowthStage keyframe2, float t)
    {
        
        UpdateEffect(animator, keyframe2);
    }

    public void TransitionComplete(GrowthStage stateTransitionedTo, GrowthStage stateTransitionedFrom)
    {
        previousState = stateTransitionedFrom;
        currentState = stateTransitionedTo;
        
    }
    public List<PlantClip> SelectClips(GrowthStage growthStage, int intensity )
    {
        List<PlantClip> clips = new List<PlantClip>();
        foreach (PlantClip clip in growthStage.clips)
        {
            if (intensity == clip.intensityLevel)
            {
                clips.Add(clip);
            }
        }
        if (clips.Count > 0)
        {
            return clips;
        }
        clips.Add(growthStage.clips[0]);
        return clips;
    }
    private PlantClip currentClip;
    private bool isTransition = false;
    public void UpdateEffect(Animator effect, GrowthStage nextFrame)
    {
        List<PlantClip> selectedClips = SelectClips(nextFrame, environmentData.IntensityLevel);
        PlantClip nextClip = selectedClips[Random.Range(0, selectedClips.Count - 1)];
        if (effect.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f && nextClip != currentClip)
        {
            
            if (currentClip.transitions.Count > 0 && isTransition == false)
            {
                foreach (TransitionClip t in currentClip.transitions)
                {
                    if (t.to == nextClip)
                    {
                        effect.Play(t.stateName);
                        isTransition = true;
                    }
                }


            }
            currentClip = nextClip;
            if (isTransition == false)
            {
                
                effect.Play(currentClip.stateName);
            }
            else
            {
                isTransition = false;
                effect.Play(currentClip.stateName);
                TransitionComplete(nextFrame, currentState);

            }
            
        }
        
        
    }

    public void React()
    {
        UpdateEffect(animator, currentState);
    }
}
