using UnityEngine;
using System;
using System.Collections;
using SaveSystemCore;

namespace Audio
{
    
    public class AudioManager: MonoBehaviour
    {
        
        public Sound[] sounds;
        
        public static AudioManager audioManagerInstance;

                
        private void Awake()
        {

            
            if (audioManagerInstance == null) audioManagerInstance = this;
            else
            {
                Destroy(gameObject);
                return;
            }
            
            DontDestroyOnLoad(gameObject);
            

            foreach (Sound s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;

            }
        }

        private void Start()
        {
            Sound music = Array.Find(sounds, sound => sound.name == "piano");
            
            if (PersistentData.gameMusic) music.source.Play();
            
        }
        
        
        public IEnumerator Play(string name,float delay)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            bool temp;
            if(name == "piano")
            {
                delay = 0f;
                temp = PersistentData.gameMusic;

            }
            else temp = PersistentData.gameSound;

            yield return new WaitForSeconds(delay);
            
            if (temp) s.source.Play();
            else s.source.Stop();
            
        }

        public void AudioToggle(string musicOrsound)
        {
            if (musicOrsound == "piano")
            {
                if (PersistentData.gameMusic) PersistentData.gameMusic = false;
                else PersistentData.gameMusic = true;

            }
            else if (musicOrsound == "crank")
            {
                if (PersistentData.gameSound) PersistentData.gameSound = false;
                else PersistentData.gameSound = true;

            }

        }
    }
}
