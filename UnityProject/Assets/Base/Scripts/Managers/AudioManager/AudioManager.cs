using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase
{
    public class AudioManager : ManagerBase<AudioManager>
    {
        private AudioSource BGMSource { get; set; }
        private AudioSource AESource { get; set; }
        private AudioListener Listener { get; set; }

        public float BGMVolume 
        {
            get
            {
                return BGMSource.volume;
            }
            set
            {
                BGMSource.volume = value;
            }
        }

        public float AEVolume
        {
            get
            {
                return AESource.volume;
            }
            set
            {
                AESource.volume = value;
            }
        }

        public bool IsBGMPlaying
        {
            get
            {
                return BGMSource.isPlaying;
            }
            set
            {
                if (value)
                    BGMSource.UnPause();
                else
                    BGMSource.Pause();
            }

        }
        
        public float Pitch
        {
            get
            {
                return BGMSource.pitch;
            }
            set
            {
                BGMSource.pitch = value;
                AESource.pitch = value;
            }
        }
        protected void Awake()
        {
            Listener = GameObject.FindObjectOfType<AudioListener>();

            BGMSource = gameObject.AddComponent<AudioSource>();
            BGMSource.ignoreListenerPause = true;
            BGMSource.spatialize = false;
            BGMSource.loop = true;

            AESource = gameObject.AddComponent<AudioSource>();
        }

        public void PlayBGM(AudioClip audio)
        {
            BGMSource.clip = audio;
            BGMSource.Play();
        }

        public void PlayAE(AudioClip audio)
        {
            AESource.PlayOneShot(audio);
        }

        public void PlayAt(AudioClip audio, Vector3 pos)
        {
            AudioSource.PlayClipAtPoint(audio,pos,AEVolume);
        }

        public void FadePitch(float pitch, float speed)
        {

            if (Pitch == pitch)
                return;

            if (speed == 0)
                return;

            if ((Pitch > pitch && speed > 0) || (Pitch < pitch && speed < 0))
                speed = -speed;

            StopAllCoroutines();
            StartCoroutine(_FadePitch(pitch,speed));
        }

        private IEnumerator _FadePitch(float _pitch,float speed)
        {
            float min = Pitch > _pitch ? _pitch : Pitch;
            float max = Pitch > _pitch ? Pitch : _pitch;
            while (Pitch != _pitch)
            {
                Pitch = Mathf.Clamp(Pitch + speed * Time.deltaTime, min, max);
                yield return null;
            }
            yield return null;
        }

    }
    
}
