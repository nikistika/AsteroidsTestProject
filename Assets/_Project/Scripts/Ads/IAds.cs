using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Ads
{
    public interface IAds
    {
        
        public void Initialize();

        public void LoadInterstitialAd();

        public UniTask ShowInterstitialAd();

        public void LoadRewardedAd();
        public UniTask<bool> ShowRewardedAds();
    }
}