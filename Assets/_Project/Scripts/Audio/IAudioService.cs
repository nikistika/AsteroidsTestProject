using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Audio
{
    public interface IAudioService
    {
        public UniTask Initialize();

        public void PlayLaserShotAudio();

        public void PlayMissileShotAudio();

        public void PlayExplosionAudio();
    }
}