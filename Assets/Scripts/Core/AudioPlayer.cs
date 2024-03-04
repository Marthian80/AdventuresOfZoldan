using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureOfZoldan.Core
{
    public class AudioPlayer : Singleton<AudioPlayer>
    {
        [Header("Weapons")]
        [SerializeField] private AudioClip shootLaserPistolClip;
        [SerializeField][Range(0f, 1f)] private float shootLaserPistolVolume = 1.0f;
        [SerializeField] private AudioClip shootRaygunClip;
        [SerializeField][Range(0f, 1f)] private float shootRaygunVolume = 1.0f;
        [SerializeField] private AudioClip[] crowbarSwingClip = new AudioClip[3];
        [SerializeField][Range(0f, 1f)] private float crowbarSwingVolume = 1.0f;        

        private AudioSource audioSource;

        protected override void Awake()
        {
            base.Awake();
            audioSource = FindObjectOfType<AudioSource>();
        }

        public void PlayLaserPistolClip()
        {
            if (shootLaserPistolClip != null)
            {
                AudioSource.PlayClipAtPoint(shootLaserPistolClip, Camera.main.transform.position, shootLaserPistolVolume);
            }
        }

        public void PlayRailgunClip()
        {
            if(shootRaygunClip != null)
            {
                AudioSource.PlayClipAtPoint(shootRaygunClip, Camera.main.transform.position, shootRaygunVolume);
            }
        }

        public void PlayCrowbarClip()
        {
            var clipToPlay = crowbarSwingClip[Random.Range(0,crowbarSwingClip.Length)];            
            if (clipToPlay != null)
            {
                AudioSource.PlayClipAtPoint(clipToPlay, Camera.main.transform.position, crowbarSwingVolume);
            }
        }
    }
}

