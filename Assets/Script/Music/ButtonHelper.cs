using UnityEngine;
using SaveSystemCore;

namespace Audio
{
    public class ButtonHelper : MonoBehaviour
    {
        private AudioManager audioManagerInstance;
        
        private void Start()
        {
            audioManagerInstance = FindObjectOfType<AudioManager>();

        }

        public void MusicSoundOnOffSwitch(string name)
        {
            audioManagerInstance.AudioToggle(name);
            if(name == "piano")
            {
                StartCoroutine(audioManagerInstance.Play(name,0));
            }
        }


    }
}
