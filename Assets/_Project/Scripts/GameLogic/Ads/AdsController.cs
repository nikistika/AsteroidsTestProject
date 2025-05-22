using Cysharp.Threading.Tasks;
using GameLogic.Ads.Unity_ads;
using Zenject;

namespace GameLogic.Ads
{
    public class AdsController : IInitializable
    {
        private IAds _ads;

        public void Initialize()
        {
            _ads = new UnityAdsService();
            _ads.Initialize();
        }

        public void LoadAd()
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

        public async UniTask<bool> ShowAdGetReward()
        {
            return await _ads.ShowRewardedAds();
        }
    }
}