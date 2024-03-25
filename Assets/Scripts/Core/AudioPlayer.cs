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
        [SerializeField] private AudioClip[] handWeaponImpactClips = new AudioClip[3];
        [SerializeField][Range(0f, 1f)] private float handWeaponImpactVolume = 1.0f;

        [Header("Effects")]
        [SerializeField] private AudioClip explosionOneClip;
        [SerializeField][Range(0f, 1f)] private float explosionOneVolume = 1.0f;
        [SerializeField] private AudioClip pickupClip;
        [SerializeField][Range(0f, 1f)] private float pickupClipVolume = 1.0f;
        [SerializeField] private AudioClip slimeDeathClip;
        [SerializeField][Range(0f, 1f)] private float slimeDeathVolume = 1.0f;

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

        public void PlayHandWeaponImpactClip(Vector3 position)
        {
            var clipToPlay = handWeaponImpactClips[Random.Range(0, handWeaponImpactClips.Length)];
            if (clipToPlay != null)
            {
                AudioSource.PlayClipAtPoint(clipToPlay, position, handWeaponImpactVolume);
            }
        }

        public void PlayExplosionOneClip(Vector3 position)
        {
            if (explosionOneClip != null)
            {
                AudioSource.PlayClipAtPoint(explosionOneClip, position, explosionOneVolume);
            }
        }

        public void PlayPickupClip()
        {
            if (pickupClip != null)
            {
                AudioSource.PlayClipAtPoint(pickupClip, Camera.main.transform.position, pickupClipVolume);
            }
        }

        public void PlaySlimeDeathEffect(Vector3 position)
        {
            if (slimeDeathClip != null)
            {
                AudioSource.PlayClipAtPoint(slimeDeathClip, position, slimeDeathVolume);
            }
        }
    }
}

