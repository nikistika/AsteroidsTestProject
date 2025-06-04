using Cysharp.Threading.Tasks;

namespace _Project.Scripts.RemoteConfig
{
    public interface IRemoteConfigService
    {
        public UniTask Initialize();
    }
}