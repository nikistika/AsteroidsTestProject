using UnityEngine;

namespace _Project.Scripts.Audio
{
    public class AudioController : MonoBehaviour, IAudio
    {
        
        [SerializeField] private AudioSource LaserShotAudio;
        [SerializeField] private AudioSource MissileShotAudio;
        [SerializeField] private AudioSource ExplosionShotAudio;
        
        public void PlayLaserShotAudio()
        {
            LaserShotAudio.Play();
        }
        
        public void PlayMissileShotAudio()
        {
            MissileShotAudio.Play();
        }
        
        public void PlayExplosionAudio()
        {
            ExplosionShotAudio.Play();
        }
        
    }
}