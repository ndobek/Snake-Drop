using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    private List<SavedPlanetViewer> planets = new List<SavedPlanetViewer>();

    public float xDistance;
    public float LerpSpeed;

    public void Add(SavedPlanetViewer viewer)
    {
        planets.Add(viewer);
    }

    public void Remove(SavedPlanetViewer viewer)
    {
        planets.Remove(viewer);
    }

    public void Update()
    {
        SetPositions();
    }

    public void SetPositions()
    {
        for(int i = 0; i < planets.Count; i++)
        {
            if (planets[i] != null)
            {
                planets[i].transform.Lerp(new Vector3(transform.position.x + (i * xDistance), transform.position.y), LerpSpeed);
            }
        }
    }

}
