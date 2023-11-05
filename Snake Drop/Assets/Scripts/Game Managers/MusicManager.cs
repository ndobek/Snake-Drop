using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private Stopwatch timer = new Stopwatch();
    private System.Random rng = new System.Random();

    public double BPS;
    private long onBeatMS;
    public float fadeTime;
    public long minHumanization = 0;
    public long maxHumination = 0;


    public List<Chord> chords = new List<Chord>();
    private int lastPlayedChord = -1;

    private long startTime;
    private long currentTime;

    private List<ScheduledNote> notes = new List<ScheduledNote>();
    public Dictionary<AudioClip, AudioSource> sources = new Dictionary<AudioClip, AudioSource>();
    private AudioClip currentNoise;
    private AudioSource currentBass;

    private void Awake()
    {
        timer.Start();
        CreateSources();

        onBeatMS = (long)(1000 / BPS);


        startTime = timer.ElapsedMilliseconds;
        currentTime = timer.ElapsedMilliseconds;
    }

    private void CreateSources()
    {
        
        foreach(Chord chord in chords)
        {
            foreach(AudioClip clip in chord.clips)
            {
                AddSource(clip);
            }

            AddSource(chord.noise);
            sources[chord.noise].loop = true;
            //AddSource(chord.bass);
        }



    }

    public void AddSource(AudioClip clip)
    {
        sources.Add(clip, gameObject.AddComponent<AudioSource>());
        sources[clip].clip = clip;
    }

    public void AddNoteOnNextBeat(AudioClip clipToPlay, int beatDivider = 1, double humanization = 0)
    {
        long beatTime = onBeatMS / beatDivider;
        long timeToNextBeat = beatTime - (currentTime % beatTime);
        long humanFactor = (long)(humanization * (double)(maxHumination - minHumanization)) + minHumanization;
        long timeToPlay = currentTime + timeToNextBeat + humanFactor;

        notes.Add(new ScheduledNote { clip = clipToPlay, time = timeToPlay });
    }

    void Update()
    {
        currentTime = timer.ElapsedMilliseconds;

        for (int i = notes.Count - 1; i >= 0; --i)
        {
            if(currentTime >= notes[i].time)
            {
                sources[notes[i].clip].Play();
                notes.RemoveAt(i);
            }
        }


    }
    public void ParseBlock(Block block)
    {
 
        foreach (Chord chord in chords)
        {
            if (chord.color != block.blockColor) continue;
            if (lastPlayedChord == -1 || chords[lastPlayedChord].name != chord.name)
            {
                if(currentNoise != null) StartCoroutine(FadeOut(sources[currentNoise], fadeTime));
                currentNoise = chord.noise;
                sources[currentNoise].Play();
            }
            lastPlayedChord = chords.IndexOf(chord);
            AudioClip note = chord.clips[rng.Next(0, chord.clips.Count)];
            AddNoteOnNextBeat(note, 2);



        }
    }

    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }
}


[System.Serializable]
public struct Chord
{
    public string name;
    public List<AudioClip> clips;
    public AudioClip noise;
    //public AudioClip bass;
    public BlockColor color;
}

[System.Serializable]
public struct ScheduledNote
{
    public AudioClip clip;
    public long time;
}
