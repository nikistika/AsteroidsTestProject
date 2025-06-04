using Cysharp.Threading.Tasks;

namespace GameLogic.Ads
{
    public interface IAdsService
    {
        public void Initialize();
        public void LoadInterstitialAd();
        public UniTask ShowInterstitialAd();
        public void LoadRewardedAd();
        public UniTask<bool> ShowRewardedAds();
    }
}