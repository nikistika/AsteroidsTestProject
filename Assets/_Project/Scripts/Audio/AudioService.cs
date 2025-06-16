using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Audio
{
    public class AudioService : IAudioService
    {
        private readonly AudioControllerSpawner _audioControllerSpawner;

        public AudioService(
            AudioControllerSpawner audioControllerSpawner)
        {
            _audioControllerSpawner = audioControllerSpawner;
        }

        public async UniTask Initialize()
        {
            await _audioControllerSpawner.StartWork();
        }

        public void PlayLaserShotAudio()
        {
            _audioControllerSpawner.AudioControllerObject.PlayLaserShotAudio();
        }

        public void PlayMissileShotAudio()
        {
            _audioControllerSpawner.AudioControllerObject.PlayMissileShotAudio();
        }

        public void PlayExplosionAudio()
        {
            _audioControllerSpawner.AudioControllerObject.PlayExplosionAudio();
        }
    }
}