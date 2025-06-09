using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Ads.Unity_ads
{
    public class UnityAdsService : IAds
    {
        
        private InterstitialAd _interstitialAd;
        private RewardedAds _rewardedAds;
        
        public void Initialize()
        {
            _interstitialAd = new InterstitialAd();
            _rewardedAds = new RewardedAds();
            
            _interstitialAd.Initialize();
            _rewardedAds.Initialize();
        }
        
        public void LoadInterstitialAd()
        {
            _interstitialAd.LoadAd();
        }
        
        public async UniTask ShowInterstitialAd()
        {
            await _interstitialAd.ShowAd();
        }
        
        public void LoadRewardedAd()
        {
            _rewardedAds.LoadAd();
        }
        
        public async UniTask<bool> ShowRewardedAds()
        {
            return await _rewardedAds.ShowAd();
        }
    }
}