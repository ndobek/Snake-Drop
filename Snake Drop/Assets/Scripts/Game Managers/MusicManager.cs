using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class MusicManager : MonoBehaviour
{
    private Stopwatch timer = new Stopwatch();
    private System.Random rng = new System.Random();

    public float musicVolume = 3;

    public Sprite mutedIcon;
    public Sprite unMutedIcon;
    public Image muteButtonImage;
    public bool Muted
    {
        get
        {
            return CloudOnce.CloudVariables.Muted;
        }
        set
        {
            CloudOnce.CloudVariables.Muted = value;
            CloudOnce.Cloud.Storage.Save();

            if (value)
            {
                muteButtonImage.sprite = mutedIcon;
                AudioListener.volume = 0;
            }
            else
            {
                muteButtonImage.sprite = unMutedIcon;
                AudioListener.volume = musicVolume;
            }
        }
    }

    public double BPS;
    private long onBeatMS;
    public float fadeTime;
    public long minHumanization = 0;
    public long maxHumination = 0;

    public long maxVolumeBlockCollectionSize = 64;


    public List<Chord> chords = new List<Chord>();
    private int lastPlayedChord = -1;

    private long startTime;
    private long currentTime;

    private List<ScheduledNote> notes = new List<ScheduledNote>();
    public Dictionary<AudioClip, AudioSource> sources = new Dictionary<AudioClip, AudioSource>();
    public Dictionary<AudioClip, float> clipVolume = new Dictionary<AudioClip, float>();
    private Chord currentChord;
    private static List<AudioSource> currentlyFading = new List<AudioSource>();
    //private AudioSource currentBass;

    private void Awake()
    {
        timer.Start();
        CreateSources();
        onBeatMS = (long)(1000 / BPS);
        StartChord(chords[0]);

        Muted = Muted; //initialize muted state
        startTime = timer.ElapsedMilliseconds;
        currentTime = timer.ElapsedMilliseconds;
    }

    private void CreateSources()
    {
        
        foreach(Chord chord in chords)
        {
            foreach(AudioClip clip in chord.pianoNotes)
            {
                AddSource(clip);
            }

            AddSource(chord.noise);
            AddSource(chord.celloLegato);
            AddSource(chord.celloTremello);
            sources[chord.noise].loop = true;
            sources[chord.celloLegato].loop = true;
            sources[chord.celloTremello].loop = true;
            //AddSource(chord.bass);
        }



    }

    public void AddSource(AudioClip clip)
    {
        sources.Add(clip, gameObject.AddComponent<AudioSource>());
        clipVolume.Add(clip, 1);
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
                StopChord();
                StartChord(chord);
            }

            lastPlayedChord = chords.IndexOf(chord);
            AudioClip note = chord.pianoNotes[rng.Next(0, chord.pianoNotes.Count)];

            AddNoteOnNextBeat(note, humanization: rng.NextDouble());
            ParseBlockCollection();
            //if(block.blockType == GameManager.instance.GameModeManager.GameMode.TypeBank.collectionType) { AddNoteOnNextBeat(chord.bass); }

        }
    }

    public void StopChord()
    {
        StartCoroutine(FadeOut(sources[currentChord.noise], fadeTime));
        StartCoroutine(FadeOut(sources[currentChord.celloLegato], fadeTime));
        StartCoroutine(FadeOut(sources[currentChord.celloTremello], fadeTime));
    }

    public void StartChord(Chord chord)
    {
        currentChord = chord;
        StartCoroutine(FadeIn(sources[currentChord.noise], fadeTime));
        sources[currentChord.celloLegato].volume = 0;
        sources[currentChord.celloTremello].volume = 0;
    }

    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        while (currentlyFading.Contains(audioSource)) yield return null;

        currentlyFading.Add(audioSource);

        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;

        currentlyFading.Remove(audioSource);
    }

    public static IEnumerator FadeIn(AudioSource audioSource, float FadeTime)
    {
        while (currentlyFading.Contains(audioSource)) yield return null;

        currentlyFading.Add(audioSource);

        float endVolume = audioSource.volume;
        audioSource.volume = 0;
        audioSource.Play();

        while (audioSource.volume < endVolume)
        {
            audioSource.volume += endVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.volume = endVolume;

        currentlyFading.Remove(audioSource);
    }
    public static IEnumerator FadeTo(AudioSource audioSource, float FadeTime, float endVolume)
    {
        while (currentlyFading.Contains(audioSource)) yield return null;

        currentlyFading.Add(audioSource);

        float startVolume = audioSource.volume;
        float elapsedTime = 0;

        while (elapsedTime < FadeTime)
        {
            elapsedTime += Time.deltaTime;
            audioSource.volume = ((endVolume - startVolume) * (elapsedTime/FadeTime)) + startVolume;

            yield return null;
        }

        audioSource.volume = endVolume;

        currentlyFading.Remove(audioSource);
    }


    public void ToggleMute()
    {
        Muted = !Muted;
    }

    public void ParseBlockCollection()
    {

        PlayerManager p = GameManager.instance.playerManagers[0];

        List<Block> blocks = p.SnakeHead.Slot.Blocks.Where((b) => !b.isPartOfSnake()).ToList();
        if (p.RoundInProgress && blocks.Count > 0 && blocks[0].BlockCollection != null)
        {
            sources[currentChord.celloLegato].volume = (float)blocks[0].BlockCollection.Area() / (float)maxVolumeBlockCollectionSize;
            sources[currentChord.celloTremello].volume = (float)blocks[0].BlockCollection.FillAmount / (float)maxVolumeBlockCollectionSize;
        }
        else
        {
            StartCoroutine(FadeTo(sources[currentChord.celloLegato], fadeTime, 0));
            StartCoroutine(FadeTo(sources[currentChord.celloTremello], fadeTime, 0));
        }
    }

}


[System.Serializable]
public struct Chord
{
    public string name;
    public List<AudioClip> pianoNotes;
    public AudioClip noise;
    public AudioClip celloLegato;
    public AudioClip celloTremello;
    public AudioClip bass;
    public BlockColor color;
}

[System.Serializable]
public struct ScheduledNote
{
    public AudioClip clip;
    public long time;
}
