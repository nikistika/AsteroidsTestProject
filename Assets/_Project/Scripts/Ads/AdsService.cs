using _Project.Scripts.Ads.Unity_ads;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Project.Scripts.Ads
{
    public class AdsService : IInitializable, IAdsService
    {
        private IAds _ads;

        public void Initialize()
        {
            _ads = new UnityAdsService();
            _ads.Initialize();
        }

        public void LoadInterstitialAd()
        {
            _ads.LoadInterstitialAd();
        }

        public async UniTask ShowInterstitialAd()
        {
            await _ads.ShowInterstitialAd();
        }

        public void LoadRewardedAd()
        {
            _ads.LoadRewardedAd();
        }

        public async UniTask<bool> ShowRewardedAds()
        {
            return await _ads.ShowRewardedAds();
        }
    }
}