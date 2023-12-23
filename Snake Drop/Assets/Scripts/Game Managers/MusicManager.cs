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
    public float lerpSpeed;
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
            AddSource(chord.bass);
            InitLoop(chord.noise);
            InitLoop(chord.celloLegato);
            InitLoop(chord.celloTremello);
        }



    }

    private void AddSource(AudioClip clip)
    {
        sources.Add(clip, gameObject.AddComponent<AudioSource>());
        clipVolume.Add(clip, 1);
        sources[clip].clip = clip;
    }

    private void InitLoop(AudioClip clip)
    {
        AddSource(clip);
        sources[clip].loop = true;
        sources[clip].volume = 0;
        clipVolume[clip] = 0;
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

        foreach( AudioClip clip in sources.Keys)
        {
            AudioSource src = sources[clip];
            src.volume += (clipVolume[clip]- src.volume) * lerpSpeed * Time.deltaTime;
            if (src.loop)
            {
                if (src.volume < .01f && src.isPlaying)
                {
                    src.Stop();
                }
                if (src.volume > .01f && !src.isPlaying)
                {
                    src.Play();
                }
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

        }
    }

    public void AddCrash()
    {
        AddNoteOnNextBeat(currentChord.bass);
    }

    public void StopChord()
    {
        clipVolume[currentChord.noise] = 0;
        clipVolume[currentChord.celloLegato] = 0;
        clipVolume[currentChord.celloTremello] = 0;
    }

    public void StartChord(Chord chord)
    {
        currentChord = chord;
        FadeIn(currentChord.noise);
        FadeIn(currentChord.celloLegato, 0);
        FadeIn(currentChord.celloTremello, 0);

        clipVolume[currentChord.noise] = 1;
        clipVolume[currentChord.celloLegato] = 0;
        clipVolume[currentChord.celloTremello] = 0;
    }

    private void FadeIn(AudioClip clip, float volume = 1)
    {
        sources[clip].Play();
        sources[clip].volume = 0;
        clipVolume[clip] = volume;
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
            clipVolume[currentChord.celloLegato] = 1 - ((float)blocks[0].BlockCollection.FillAmount / (float)maxVolumeBlockCollectionSize);
            clipVolume[currentChord.celloTremello] = (float)blocks[0].BlockCollection.FillAmount / (float)maxVolumeBlockCollectionSize;
        }
        else
        {
            clipVolume[currentChord.celloLegato] = 0;
            clipVolume[currentChord.celloTremello] = 0;
        }
    }

    public void Florish()
    {
        AddNoteOnNextBeat(currentChord.pianoNotes[0], humanization: 0);
        AddNoteOnNextBeat(currentChord.pianoNotes[1], humanization: 1);
        AddNoteOnNextBeat(currentChord.pianoNotes[2], humanization: 2);
        AddNoteOnNextBeat(currentChord.pianoNotes[3], humanization: 3);
        AddNoteOnNextBeat(currentChord.pianoNotes[4], humanization: 4);
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
