using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Environment Settings/Wind")]
public class Wind : ScriptableObject
{
    public float xDistanceModifier;
    public float yDistanceModifier;
    public float periodLength;

    public float GetWindSpeed(Transform location)
    {
        //result from -1 to 1
        float result = Mathf.Sin((Time.time + (xDistanceModifier * location.position.x) + (yDistanceModifier * location.position.y)) / periodLength);
        //result from 0 to 1
        return (result + 1f) / 2;
    }

}
