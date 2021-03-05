using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;


public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public List<AudioProfile> audioProfiles;
    public List<AudioPlayers> audioPlayers = new List<AudioPlayers>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
        SetUp();

    }
  
    private void SetUp()
    {
        GameObject audiobase = new GameObject("Audio Base");
        audiobase.transform.SetParent(transform);
        audioProfiles.ForEach(x =>
        {
            GameObject audioobject = new GameObject(x.audioID);
            audioobject.transform.SetParent(audiobase.transform);
           AudioSource audiosource= audioobject.AddComponent<AudioSource>();
            audiosource.playOnAwake = false;
            audiosource.clip = x.audioClip;
            audioPlayers.Add(new AudioPlayers()
            {
                audioID = x.audioID,
                audioSource = audiosource
            });
        });
    }

    public void PlayAudio(string audioID)
    {
        if (!audioPlayers.Any(x => x.audioID == audioID)) return;
        Debug.Log("Play Audio");
      AudioPlayers findedaudio=audioPlayers.Find(x => x.audioID == audioID);
        findedaudio.audioSource.Play();
    }
        

    [System.Serializable]
    public struct AudioProfile
    {
        public string audioID;
        public AudioClip audioClip;
    }
    public struct AudioPlayers
    {
        public string audioID;
        public AudioSource audioSource;
    }
}
