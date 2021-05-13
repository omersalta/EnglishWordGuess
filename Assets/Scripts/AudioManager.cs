﻿using System;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager instance;
    
    public Sound[] Sounds;
    
    private void Awake() {

        if (instance == null) {
            instance = this;
        }else {
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);
        
        foreach(var s in Sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
            s.source.volume = s.volume;
        }
    }
    
    
    public void Play(string name) {
        name = name.ToLower();
        Sound s = Array.Find(Sounds, Sound => Sound.name == name);
        
        if (s == null) {
            Debug.LogWarning(name +" sound cannot find");
        }else {
            s.source.Play();
        }
        
    }
    
    public void Stop(string name) {
        name = name.ToLower();
        Sound s = Array.Find(Sounds, Sound => Sound.name == name);
        
        if (s == null) {
            Debug.LogWarning(name +" sound cannot find");
        }else {
            s.source.Stop();
        }
        
    }
    
    public void StopAll() {
        foreach (var s in Sounds) {
            s.source.Stop();
        }
    }
    
}
