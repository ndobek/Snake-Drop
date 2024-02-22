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

            foreach (AudioSource src in sources.Values)
            {
                src.enabled = !value;
            }
        }
    }

    public double BPS;
    private long onBeatMS;
    public float lerpSpeed;

    public long maxVolumeBlockCollectionSize = 64;

    public AudioClip ClearBoard;
    public List<Chord> chords = new List<Chord>();
    private int lastPlayedChord = -1;

    private long startTime;
    private long currentTime;

    private List<ScheduledNote> notes = new List<ScheduledNote>();
    public Dictionary<AudioClip, AudioSource> sources = new Dictionary<AudioClip, AudioSource>();
    private AudioSource commonSource = null;
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
        if (commonSource == null) commonSource = gameObject.AddComponent<AudioSource>();
        foreach (Chord chord in chords)
        {
            foreach (AudioClip clip in chord.pianoNotes)
            {
                AddSource(clip, true);
            }
            foreach (AudioClip clip in chord.harpNotes)
            {
                AddSource(clip, true);
            }
            AddSource(chord.bass, true);
            InitLoop(chord.noise);
            InitLoop(chord.celloLegato);
            InitLoop(chord.celloTremello);
        }
        AddSource(ClearBoard);
    }

    private void AddSource(AudioClip clip, bool useCommon = false)
    {
        if (useCommon)
        {
            sources.Add(clip, commonSource);
        }
        else
        {
            sources.Add(clip, gameObject.AddComponent<AudioSource>());
        }
        sources[clip].playOnAwake = false;
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

    public void AddNoteOnNextBeat(AudioClip clipToPlay, long delay = 0, int beatDivider = 1)
    {
        long beatTime = onBeatMS / beatDivider;
        long timeToNextBeat = beatTime - (currentTime % beatTime);
        long timeToPlay = currentTime + timeToNextBeat + delay;

        notes.Add(new ScheduledNote { clip = clipToPlay, time = timeToPlay });
    }

    public void OnBoardClear()
    {
        StopChord();
        StartChord(chords.Where(c => c.name == "Cmaj9").First());
        AddNoteOnNextBeat(ClearBoard);
    }

    void Update()
    {

        currentTime = timer.ElapsedMilliseconds;

        for (int i = notes.Count - 1; i >= 0; --i)
        {
            if(currentTime >= notes[i].time)
            {
                sources[notes[i].clip].PlayOneShot(notes[i].clip);
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

        Chord chord = GetChordFromBlock(block);
        if (lastPlayedChord == -1 || chords[lastPlayedChord].name != chord.name)
        {
            StopChord();
            StartChord(chord);
        }

        lastPlayedChord = chords.IndexOf(chord);
        AudioClip note = chord.pianoNotes[rng.Next(0, chord.pianoNotes.Count)];

        AddNoteOnNextBeat(note);
    }
    public void ParseCrash(Block block)
    {
        if (!block) return;
        Chord chord = GetChordFromBlock(block);
        AudioClip note = chord.harpNotes[rng.Next(0, chord.harpNotes.Count)];
        AddNoteOnNextBeat(note);
    }

    public Chord GetChordFromBlock(Block block)
    {
        foreach (Chord chord in chords)
        {
            if (chord.color != block.blockColor) continue;
            else return chord;
        }
        return chords[0];
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
    }

    private void FadeIn(AudioClip clip, float volume = 1)
    {
        //sources[clip].Play();
        //sources[clip].volume = 0;
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
            Chord chord = GetChordFromBlock(blocks[0]);
            float intensity = (float)blocks[0].BlockCollection.FillAmount / (float)maxVolumeBlockCollectionSize;
            float danger = 1- Mathf.Clamp(((float)p.entranceManager.NumberOfOpenEntrances() / (float)p.entranceManager.slots.Length) * 2f, 0,1);


                clipVolume[chord.celloLegato] = intensity * (1-danger);
                clipVolume[chord.celloTremello] = intensity * danger;

        }
        else
        {
            foreach (Chord chord in chords)
            {
                clipVolume[chord.celloLegato] = 0;
                clipVolume[chord.celloTremello] = 0;
            }
        }
    }

    public void Florish(Block block)
    {
        Chord chord = GetChordFromBlock(block);
        AddNoteOnNextBeat(chord.pianoNotes[rng.Next(0, chord.pianoNotes.Count)], 0);
        AddNoteOnNextBeat(chord.pianoNotes[rng.Next(0, chord.pianoNotes.Count)], 30);
        AddNoteOnNextBeat(chord.pianoNotes[rng.Next(0, chord.pianoNotes.Count)], 60);
        AddNoteOnNextBeat(chord.pianoNotes[rng.Next(0, chord.pianoNotes.Count)], 80);
        AddNoteOnNextBeat(chord.pianoNotes[rng.Next(0, chord.pianoNotes.Count)], 100);
    }

}


[System.Serializable]
public struct Chord
{
    public string name;
    public List<AudioClip> pianoNotes;
    public List<AudioClip> harpNotes;
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
