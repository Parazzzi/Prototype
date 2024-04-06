using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.IO;
using System.Runtime.CompilerServices;
using System;


[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    public float volume = 1.0f;
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public VolumeData volumeData;
    private string savePath;

    [SerializeField]
    private List<Sound> sounds = new List<Sound>();

    private Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();
    private List<AudioSource> audioSources = new List<AudioSource>();

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        savePath = Path.Combine(Application.persistentDataPath, "volume.json");
        volumeData = new VolumeData();
        LoadVolume();

        foreach (var sound in sounds)
        {
            if (!audioClips.ContainsKey(sound.name))
                audioClips.Add(sound.name, sound.clip);
        }
        SetVolume(volumeData.volume);
    }

    public void SetVolume(float volume)
    {
        volumeData.volume = volume;
        foreach (var sound in sounds)
        {
            sound.volume = volumeData.volume;
        }

        foreach (var source in audioSources)
        {
            source.volume = volumeData.volume;
        }
        SaveVolume();
    }

    private void SaveVolume()
    {
        string json = JsonUtility.ToJson(volumeData);
        File.WriteAllText(savePath, json);
    }

    private void LoadVolume()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            volumeData = JsonUtility.FromJson<VolumeData>(json);
        }
    }

    public void PlaySound(string name)
    {
        if (audioClips.ContainsKey(name))
        {
            AudioSource freeSource = null;
            foreach (var source in audioSources)
            {
                if (!source.isPlaying)
                {
                    freeSource = source;
                    break;
                }
            }

            if (freeSource == null)
            {
                freeSource = gameObject.AddComponent<AudioSource>();
                audioSources.Add(freeSource);
            }
            Sound sound = sounds.Find(s => s.name == name);
            if (sound != null)
            {
                freeSource.clip = audioClips[name];
                freeSource.volume = sound.volume;
                freeSource.Play();
                StartCoroutine(DisableSource(freeSource));
            }
        }
        else
        {
            Debug.LogWarning("AudioClip with name " + name + " not found.");
        }
    }

    private System.Collections.IEnumerator DisableSource(AudioSource source)
    {
        yield return new WaitForSeconds(source.clip.length);
        source.clip = null;
    }

}
