using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat<T>
{
    public T value;
    public float probability = 1;
    public SnakeStatType StatType;

    public static Y GetRandomFromStats<Y>(Stat<Y>[] stats)
    {
        float total = 0;
        foreach (Stat<Y> stat in stats)
        {
            total += stat.probability;
        }

        float random = Random.Range(0.0f, 1.0f);
        float cumulative = 0;

        for (int i = 0; i < stats.Length; i++)
        {
            float lowerBound = cumulative;
            float upperBound = (stats[i].probability / total) + lowerBound;


            if (lowerBound <= random && random < upperBound)
            {
                return stats[i].value;
            }
            cumulative = upperBound;
        }

        throw new System.Exception("Finding a random value diddn't work");
    }
}
