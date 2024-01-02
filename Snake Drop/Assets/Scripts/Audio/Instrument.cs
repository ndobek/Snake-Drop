using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
[CreateAssetMenu(menuName = "Audio/Instrument")]
public class Instrument : ScriptableObject
{
    public bool loop = false;
    public List<AudioClip> notes;

    public List<AudioClip> GetNote(string name)
    {
        return notes.Where(n=>n.name == name).ToList();
    }
}

